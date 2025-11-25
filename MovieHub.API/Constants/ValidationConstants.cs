namespace MovieHub.API.Constants
{
    public static class ValidationConstants
    {
        public const int MaxNameLength = 100;
        public const string NameRegex = @"^[A-Za-z][A-Za-z0-9 .,'&-]*$";
        public const int MaxPersonNameLength = 100;
        public const string PersonNameRegex = @"^[A-Za-z][A-Za-z\s\.\'\-]*$";

        public static class Branch
        {
            public const int MaxLocationLength = 250;
            public const string LocationRegex = @"^[A-Za-z0-9 ./,'&-]*$";
        }

        public static class Hall
        {
            public const int MinRows = 1;
            public const int MaxRows = 20;
            public const int MinColumns = 1;
            public const int MaxColumns = 50;
        }

        public static class Movie
        {
            public const int MaxTitleLength = 200;
            public const string MovieTitleRegex =
                @"^[A-Za-z0-9][A-Za-z0-9\s:\-,'!?.&()]*$";
            public const int MaxGenreLength = 200;
            public const string GenreRegex =
                @"^[A-Za-z][A-Za-z\s\/\-&]*(,\s*[A-Za-z][A-Za-z\s\/\-&]*)*$";
            public const int MinDuration = 1; // in minutes
            public const int MaxDuration = 500; // in minutes
            public const int MinDescriptionLength = 50;
            public const int MaxDescriptionLength = 1000;
            public const int MaxPosterUrlLength = 500;
            public const int MaxActorsLength = 500;
            public const string ActorsRegex =
                @"^[A-Za-z][A-Za-z\s\.\'\-]*(,\s*[A-Za-z][A-Za-z\s\.\'\-]*)*$";
        }

        public static class Seat
        {
            public const int MinSeatNumberLength = 2;
            public const int MaxSeatNumberLength = 3;
        }

        public static class ShowTime
        {
            public const int MinBufferMinutes = 20;
            public const int MaxBufferMinutes = 60;
        }

        public static class Booking
        {
            public const int MinSeats = 1;
        }

        public static class User
        {
            public const int MinUsernameLength = 3;
            public const int MaxUsernameLength = 20;
        }
    }
}
