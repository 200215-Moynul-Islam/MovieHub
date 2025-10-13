namespace MovieHub.API.Constants
{
    public static class ErrorMessages
    {
        public const string NameRequired = "Name is required.";
        public const string NameMaxLengthExceeded = "Name cannot exceed 100 characters.";
        public const string InvalidNameFormat = "Name must start with a letter and contain only letters, numbers, spaces, and . , ' - &.";

        public static class Branch
        {
            public const string LocationRequired = "Location is required.";
            public const string LocationMaxLengthExceeded = "Location cannot exceed 250 characters.";
            public const string InvalidLocationFormat = "Location can only contain letters, numbers, spaces, and . / , ' - &.";
        }
    }
}
