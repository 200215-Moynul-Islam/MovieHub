using MovieHub.API.Resources;

namespace MovieHub.API.Constants
{
    public class BusinessErrorMessages
    {
        public const string InternalServerError =
            TextConstants.InternalServerErrorMessage;

        public class User
        {
            public const string UsernameUnavailable =
                TextConstants.User_UsernameUnavailable_ErrorMessage;
            public const string InvalidEmailOrUsername =
                TextConstants.User_InvalidEmailOrUsername_ErrorMessage;
            public const string InvalidCredential =
                TextConstants.User_InvalidEmailOrUsernameOrPassword_ErrorMessage;
            public const string EmailUnavailable =
                TextConstants.User_EmailUnavailable_ErrorMessage;
            public const string NotFound =
                TextConstants.User_NotFound_ErrorMessage;
            public const string ManagerNotFound =
                TextConstants.User_ManagerNotFound_ErrorMessage;
            public const string ManagerUnavailable =
                TextConstants.User_ManagerUnavailable_ErrorMassage;
        }

        public class Branch
        {
            public const string NameUnavailable =
                TextConstants.Branch_NameUnavailable_ErrorMessage;
            public const string NotFound =
                TextConstants.Branch_NotFound_ErrorMessage;
        }

        public class Hall
        {
            public const string NameUnavailable =
                TextConstants.Hall_NameUnavailable_ErrorMessage;
            public const string NotFound =
                TextConstants.Hall_NotFound_ErrorMessage;
            public const string HasUpcomingShowTimes =
                TextConstants.Hall_HasUpcomingShowTimes_ErrorMessage;
        }

        public class Seat
        {
            public const string RowNumberOutOfRange =
                TextConstants.Seat_RowNumberOutOfRange_ErrorMessage;
        }

        public class Movie
        {
            public const string NameUnavailable =
                TextConstants.Movie_NameUnavailable_ErrorMessage;
            public const string NotFound =
                TextConstants.Movie_NotFound_ErrorMessage;
            public const string HasUpcomingShowTimes =
                TextConstants.Movie_HasUpcomingShowTimes_ErrorMessage;
        }

        public class ShowTime
        {
            public const string NotFound =
                TextConstants.ShowTime_NotFound_ErrorMessage;
            public const string Conflict =
                TextConstants.ShowTime_Conflict_ErrorMessage;
            public const string Started =
                TextConstants.ShowTime_Started_ErrorMessage;
        }

        public class Booking
        {
            public const string NotFoundSeatsFailure =
                TextConstants.Booking_NotFoundSeatsFailure_ErrorMessage;
            public const string UnavailableSeatsFailure =
                TextConstants.Booking_UnavailableSeatsFailure_ErrorMessage;
        }
    }
}
