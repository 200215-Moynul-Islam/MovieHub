using MovieHub.API.Resources;

namespace MovieHub.API.Constants
{
    public static class ErrorMessages
    {
        public const string NameRequired = TextConstants.NameRequired_ErrorMessage;
        public const string NameMaxLengthExceeded =
            TextConstants.NameMaxLengthExceeded_ErrorMessage;
        public const string InvalidNameFormat = TextConstants.InvalidNameFormat_ErrorMessage;

        public static class Branch
        {
            public const string LocationRequired =
                TextConstants.Branch_LocationRequired_ErrorMessage;
            public const string LocationMaxLengthExceeded =
                TextConstants.Branch_LocationMaxLengthExceeded_ErrorMessage;
            public const string InvalidLocationFormat =
                TextConstants.Branch_InvalidLocationFormat_ErrorMessage;
        }
    }
}
