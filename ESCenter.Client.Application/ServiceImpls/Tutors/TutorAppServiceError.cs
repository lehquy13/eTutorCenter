using Matt.ResultObject;

namespace ESCenter.Client.Application.ServiceImpls.Tutors;

public static class TutorAppServiceError
{
    public static Error FailToCreateTutorWhileSavingChanges => new(
        "TutorAppServiceError.FailToCreateTutorWhileSavingChanges", "Fail to create tutor while saving changes");

    public static Error FailToUpdateTutorWhileSavingChanges => new(
        "TutorAppServiceError.FailToCreateTutorWhileSavingChanges", "Fail to update tutor while saving changes");

    public static Error NonExistChangeVerificationRequest
        => new("TutorAppServiceError.NonExistChangeVerificationRequest", "Non-exist change verification request");

    public static Error NonExistTutorError => new("TutorAppServiceError.NonExistTutorError", "Non-exist tutor");

    public static Error CantRequestTutorToSelf
        => new("TutorAppServiceError.CantRequestTutorToSelf", "Can't request tutor to self");
}