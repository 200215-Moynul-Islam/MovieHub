using MovieHub.API.Resources;

namespace MovieHub.API.Constants
{
    public static class ErrorMessages
    {
        public const string NameRequired =
            TextConstants.NameRequired_ErrorMessage;
        public const string NameMaxLengthExceeded =
            TextConstants.NameMaxLengthExceeded_ErrorMessage;
        public const string InvalidNameFormat =
            TextConstants.InvalidNameFormat_ErrorMessage;

        public const string PersonNameRequired =
            TextConstants.PersonNameRequired_ErrorMessage;
        public const string PersonNameMaxLenghtExceeded =
            TextConstants.PersonNameMaxLenghtExceeded_ErrorMessage;
        public const string InvalidPersonNameFormat =
            TextConstants.InvalidPersonNameFormat_ErrorMessage;
        public const string EmailOrUsernameMaxLengthExceeded =
            TextConstants.EmailOrUsernameMaxLengthExceeded_ErrorMessage;

        public static class Branch
        {
            public const string LocationRequired =
                TextConstants.Branch_LocationRequired_ErrorMessage;
            public const string LocationMaxLengthExceeded =
                TextConstants.Branch_LocationMaxLengthExceeded_ErrorMessage;
            public const string InvalidLocationFormat =
                TextConstants.Branch_InvalidLocationFormat_ErrorMessage;
        }

        public static class Movie
        {
            public const string TitleRequired =
                TextConstants.Movie_TitleRequired_ErrorMessage;
            public const string TitleMaxLengthExceeded =
                TextConstants.Movie_TitleMaxLengthExceeded_ErrorMessage;
            public const string InvalidTitleFormat =
                TextConstants.Movie_InvalidTitleFormat_ErrorMessage;

            public const string GenreRequired =
                TextConstants.Movie_GenreRequired_ErrorMessage;
            public const string GenreMaxLengthExceeded =
                TextConstants.Movie_GenreMaxLengthExceeded_ErrorMessage;
            public const string InvalidGenreFormat =
                TextConstants.Movie_InvalidGenreFormat_ErrorMessage;

            public const string DurationRequired =
                TextConstants.Movie_DurationRequired_ErrorMessage;
            public const string DurationOutOfRange =
                TextConstants.Movie_DurationOutOfRange_ErrorMessage;

            public const string DescriptionMinLengthRequired =
                TextConstants.Movie_DescriptionMinLengthRequired_ErrorMessage;
            public const string DescriptionMaxLengthExceeded =
                TextConstants.Movie_DescriptionMaxLengthExceeded_ErrorMessage;

            public const string PosterUrlRequired =
                TextConstants.Movie_PosterUrlRequired_ErrorMessage;
            public const string PosterUrlMaxLengthExceeded =
                TextConstants.Movie_PosterUrlMaxLengthExceeded_ErrorMessage;
            public const string InvalidPosterUrlFormat =
                TextConstants.Movie_InvalidPosterUrlFormat_ErrorMessage;

            public const string ActorsRequired =
                TextConstants.Movie_ActorsRequired_ErrorMessage;
            public const string ActorsMaxLengthExceeded =
                TextConstants.Movie_ActorsMaxLengthExceeded_ErrorMessage;
            public const string InvalidActorsFormat =
                TextConstants.Movie_InvalidActorsFormat_ErrorMessage;
        }

        public static class ShowTime
        {
            public const string BufferMinutesOutOfRange =
                TextConstants.ShowTime_BufferMinutesOutOfRange_ErrorMessage;
        }

        public static class Booking
        {
            public const string MinSeatsRequired =
                TextConstants.Booking_MinSeatsRequired_ErrorMessage;
        }

        public static class User
        {
            public const string EmailRequired =
                TextConstants.User_EmailRequired_ErrorMessage;
            public const string EmailMaxLengthExceeded =
                TextConstants.User_EmailMaxLengthExceeded_ErrorMessage;
            public const string InvalidEmailFormat =
                TextConstants.User_InvalidEmailFormat_ErrorMessage;
            public const string UsernameRequired =
                TextConstants.User_UsernameRequired_ErrorMessage;
            public const string UsernameLengthOutOfRange =
                TextConstants.User_UsernameLengthOutOfRange_ErrorMessage;
            public const string InvalidUsernameFormat =
                TextConstants.User_InvalidUsernameFormat_ErrorMessage;
            public const string PasswordRequired =
                TextConstants.User_PasswordRequired_ErrorMessage;
            public const string PasswordLengthOutOfRange =
                TextConstants.User_PasswordLengthOutOfRange_ErrorMessage;
            public const string InvalidPasswordFormat =
                TextConstants.User_InvalidPasswordFormat_ErrorMessage;
        }

        public static class CustomAttributes
        {
            public const string FutureDate_Default =
                TextConstants.CustomAttributes_FutureDate_DefaultErrorMessage;

            // Consider removing this to a exception message specicic file.
            public const string FutureDate_InvalidProperty =
                TextConstants.CustomAttributes_FutureDate_InvalidProperty_ErrorMessage;

            public const string EmailOrUsername_Default =
                TextConstants.CustomAttributes_EmailOrUsername_DefaultErrorMessage;
        }
    }
}
