namespace CleanArchitecture.Domain;

public class AppConst
{
    public const string WebApiProjectName = "CleanArchitecture.WebApi";
}

public class UrlConst
{
    public static string Posts = "/posts";
    public static string Home = "/";
    public static string About = "/about";
    public static string Contact = "/contact";
    public static string PostsByCategorySlug = "/posts-by-category/{0}";
    public static string PostDetails = "/post-by-slug/{0}";
    public static string PostsByTagSlug = "/posts-by-tag/{0}";
    public static string Login = "/login";
    public static string Register = "/register";
    public static string Profile = "/profile";
    public static string Author = "/author/{0}";

    public string ChangePassword = "/change-password";
}