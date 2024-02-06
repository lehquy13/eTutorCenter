using ESCenter.Application.Contract.Commons.Primitives.Auditings;

namespace ESCenter.Application.Contract.Users.Learners;

public class LearnerForProfileDto : BasicAuditedEntityDto<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";
    public int BirthYear { get; set; } = 1960;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";

    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
     public string Role { get; set; } = "Learner";
}