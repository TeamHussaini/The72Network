namespace The72Network.Web.Shared
{
  public static class Constants
  {
    public const int ThresholdDays = 7;

    public const int ThresholdSocialTypeCount = 20;

    public const string SendGridEnvironmentKey = "sendgrid_apikey";

    // Admin usernames are saved as Environment Setting
    public const string Admin_Usernames = "admin_usernames";

    public const char Admin_Usernames_Delimiter = ';';

    public const string AdminRoleName = "Admin";

    public const string UserRoleName = "User";
  }
}
