﻿using ESCenter.Admin.Application.Contracts.Commons;
using ESCenter.Domain.Aggregates.Tutors;
using ESCenter.Domain.Aggregates.Users;
using Mapster;
using Matt.SharedKernel.Application.Contracts.Primitives;

namespace ESCenter.Admin.Application.Contracts.Users.Tutors;

public class TutorForListDto : BasicAuditedEntityDto<Guid>
{
    //Admin information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = Domain.Shared.Courses.GenderEnum.Male.ToString();

    public int BirthYear { get; set; } = 1960;

    //public string WardId { get; set; } = "00001";
    public string Description { get; set; } = string.Empty;

    public string Avatar { get; set; }  = string.Empty;

    //Account References
    public string PhoneNumber { get; set; } = string.Empty;

    public string AcademicLevel { get; set; } = Domain.Shared.Courses.AcademicLevel.UnderGraduated.ToString();
    public string University { get; set; } = string.Empty;
    public short NumberOfRequests { get; set; } = 0;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;
    public short NumberOfChangeRequests { get; set; } = 0;
}

public class TutorListDto : EntityDto<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public int NumberOfRequests { get; set; } = 0;
    public int NumberOfChangeRequests { get; set; } = 0;
    public bool IsVerified { get; set; } = false;
    public string PhoneNumber { get; set; } = string.Empty;
}

public class TutorForListDtoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Customer, Tutor, int, int), TutorForListDto>()
            .Map(dest => dest.Id, src => src.Item2.Id.Value)
            .Map(dest => dest.FirstName, src => src.Item1.FirstName)
            .Map(dest => dest.LastName, src => src.Item1.LastName)
            .Map(dest => dest, src => src.Item2)
            .Map(dest => dest.NumberOfRequests, src => src.Item3)
            .Map(dest => dest.NumberOfChangeRequests, src => src.Item4);
    }
}