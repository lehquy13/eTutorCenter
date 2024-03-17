﻿using ESCenter.Domain.Aggregates.Tutors;
using MapsterMapper;
using Matt.ResultObject;
using Matt.SharedKernel.Application.Mediators.Queries;
using Matt.SharedKernel.Domain.Interfaces;

namespace ESCenter.Admin.Application.ServiceImpls.Tutors.Queries.GetPopularTutors;

public class GetPopularTutorsQueryHandler(
    ITutorRepository tutorRepository,
    IAppLogger<GetPopularTutorsQueryHandler> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : QueryHandlerBase<GetPopularTutorsQuery, List<PopularTutorListDto>>(unitOfWork, logger, mapper)
{
    public override async Task<Result<List<PopularTutorListDto>>> Handle(GetPopularTutorsQuery request, CancellationToken cancellationToken)
    {
        var popularTutorsAsQueryable = await tutorRepository.GetPopularTutors();
        
        var popularTutorListDtos = Mapper.Map<List<PopularTutorListDto>>(popularTutorsAsQueryable);

        return popularTutorListDtos;
    }
}