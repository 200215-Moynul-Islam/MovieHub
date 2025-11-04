namespace MovieHub.API.Resources
{
    public class TextConstants
    {
        public const string NameRequired_ErrorMessage = "Name is required.";
        public const string NameMaxLengthExceeded_ErrorMessage =
            "Name cannot exceed 100 characters.";
        public const string InvalidNameFormat_ErrorMessage =
            "Name must start with a letter and contain only letters, numbers, spaces, and . , ' - &.";

        public const string PersonNameRequired_ErrorMessage =
            "Name is required.";
        public const string PersonNameMaxLenghtExceeded_ErrorMessage =
            "Name cannot exceed 100 characters.";
        public const string InvalidPersonNameFormat_ErrorMessage =
            "Name must start with a letter and can include letters, spaces, apostrophes, hyphens, or periods.";

        #region Branch
        public const string Branch_LocationRequired_ErrorMessage =
            "Location is required.";
        public const string Branch_LocationMaxLengthExceeded_ErrorMessage =
            "Location cannot exceed 250 characters.";
        public const string Branch_InvalidLocationFormat_ErrorMessage =
            "Location can only contain letters, numbers, spaces, and . / , ' - &.";
        #endregion

        #region Movie
        public const string Movie_TitleRequired_ErrorMessage =
            "Title is required.";
        public const string Movie_TitleMaxLengthExceeded_ErrorMessage =
            "Title can not exceed 200 characters.";
        public const string Movie_InvalidTitleFormat_ErrorMessage =
            "Title can only contain letters, numbers, spaces, and basic punctuation (: - , ' ! ? . & ( )). It must start with a letter or number.";

        public const string Movie_GenreRequired_ErrorMessage =
            "Genre is required.";
        public const string Movie_GenreMaxLengthExceeded_ErrorMessage =
            "Genre can not exceed 200 characters.";
        public const string Movie_InvalidGenreFormat_ErrorMessage =
            "Each genre must start with a letter and can contain letters, spaces, /, -, &. Separate multiple genres with commas.";

        public const string Movie_DurationRequired_ErrorMessage =
            "Duration is required.";
        public const string Movie_DurationOutOfRange_ErrorMessage =
            "Duration must be between 1 and 500 minutes.";

        public const string Movie_DescriptionMinLengthRequired_ErrorMessage =
            "Description must be at least 50 characters long.";
        public const string Movie_DescriptionMaxLengthExceeded_ErrorMessage =
            "Description can not exceed 1000 characters.";

        public const string Movie_PosterUrlRequired_ErrorMessage =
            "Poster URL is required.";
        public const string Movie_PosterUrlMaxLengthExceeded_ErrorMessage =
            "Poster URL length can not exceed 500 characters.";
        public const string Movie_InvalidPosterUrlFormat_ErrorMessage =
            "Invalid poster URL.";

        public const string Movie_ActorsRequired_ErrorMessage =
            "Actors is required.";
        public const string Movie_ActorsMaxLengthExceeded_ErrorMessage =
            "Actors can not exceed 500 characters.";
        public const string Movie_InvalidActorsFormat_ErrorMessage =
            "Actors must be a comma-separated list of names using letters, spaces, apostrophes, hyphens, or periods.";
        #endregion
    }
}
