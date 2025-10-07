namespace MovieHub.API.Constants
{
    public static class ValidationConstants
    {
        public const int MaxNameLength = 100;
        public const int MaxLocationLength = 250;

        #region Hall
        public const int MinRows = 1;
        public const int MaxRows = 20;
        public const int MinColumns = 1;
        public const int MaxColumns = 50;
        #endregion

        #region Movie
        public const int MaxMovieTitleLength = 200;
        public const int MaxMovieGenreLength = 200;
        public const int MinMovieDuration = 1; // in minutes
        public const int MaxMovieDuration = 500; // in minutes
        public const int MaxMovieDirectorLength = 100;
        public const int MaxMovieDescriptionLength = 1000;
        public const int MaxMoviePosterUrlLength = 500;
        public const int MaxMovieActorsLength = 500;
        #endregion

        #region Seat
        public const int MinSeatNumberLength = 2;
        public const int MaxSeatNumberLength = 3;
        #endregion
    }
}
