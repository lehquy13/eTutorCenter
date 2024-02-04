﻿using ESCenter.Domain.Aggregates.Subjects;
using ESCenter.Domain.Aggregates.Subjects.ValueObjects;
using ESCenter.Persistence.Entity_Framework_Core;
using Matt.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESCenter.Persistence.Persistence.Repositories;

internal class SubjectRepository(
    AppDbContext appDbContext,
    IAppLogger<SubjectRepository> appLogger)
    : RepositoryImpl<Subject, SubjectId>(appDbContext, appLogger), ISubjectRepository
{
    public Task<List<Subject>> GetListByIdsAsync(IEnumerable<SubjectId> subjectIds, CancellationToken cancellationToken)
    {
        return AppDbContext.Subjects
            .Where(s => subjectIds.Contains(s.Id))
            .ToListAsync(cancellationToken);
    }
}