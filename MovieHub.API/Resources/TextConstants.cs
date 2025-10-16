namespace MovieHub.API.Resources
{
    public class TextConstants
    {
        public const string NameRequired_ErrorMessage = "Name is required.";
        public const string NameMaxLengthExceeded_ErrorMessage =
            "Name cannot exceed 100 characters.";
        public const string InvalidNameFormat_ErrorMessage =
            "Name must start with a letter and contain only letters, numbers, spaces, and . , ' - &.";

        #region Branch
        public const string Branch_LocationRequired_ErrorMessage = "Location is required.";
        public const string Branch_LocationMaxLengthExceeded_ErrorMessage =
            "Location cannot exceed 250 characters.";
        public const string Branch_InvalidLocationFormat_ErrorMessage =
            "Location can only contain letters, numbers, spaces, and . / , ' - &.";
        #endregion
    }
}
