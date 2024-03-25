﻿using ESCenter.Domain.Aggregates.Discoveries;
using ESCenter.Domain.Aggregates.Discoveries.Entities;
using ESCenter.Domain.Aggregates.Discoveries.ValueObjects;
using ESCenter.Domain.Aggregates.DiscoveryUsers;
using ESCenter.Domain.Aggregates.Subjects.ValueObjects;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using ESCenter.Persistence.EntityFrameworkCore;
using Matt.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESCenter.Persistence.Persistence.Repositories;

internal class DiscoveryRepository(AppDbContext appDbContext, IAppLogger<RepositoryImpl<Discovery, DiscoveryId>> logger)
    : RepositoryImpl<Discovery, DiscoveryId>(appDbContext, logger), IDiscoveryRepository
{
    public IQueryable<DiscoverySubject> GetDiscoverySubjectAsQueryable()
    {
        return AppDbContext.Set<DiscoverySubject>();
    }

    public async Task<List<SubjectId>> GetUserDiscoverySubjects(CustomerId userGuid, 
        CancellationToken cancellationToken)
    {
        return await AppDbContext.Database.SqlQuery<SubjectId>(
            $"""
              SELECT SubjectId 
              FROM DiscoverySubject 
              JOIN DISCOVERYUSER ON DISCOVERYUSER.DiscoveryId = DISCOVERYSUBJECT.DiscoveryId 
              WHERE UserId = {userGuid.Value}
              """).ToListAsync(cancellationToken);
    }

    public IQueryable<DiscoveryUser> GetDiscoveryUserAsQueryable()
    {
        return AppDbContext.Set<DiscoveryUser>();
    }
}