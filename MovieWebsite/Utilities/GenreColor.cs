using System;

namespace MovieWebsite.Utilities
{
    public static class GenreColor
    {
        public static String GetColorClass(string genre)
        {
            genre = genre.ToLower();
            switch (genre)
            {
                case "action":
                    return "yell";
                    break;
                case "sci-fi":
                    return "blue";
                    break;
                case "advanture":
                    return "orange";
                    break;
                case "comedy":
                    return "green";
                    break;
                case "thriller":
                    return "red";
                    break;
                default:
                    return "";
            }
        }
    }
}