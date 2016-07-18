using SendGrid.Helpers.Mail;

namespace The72Network.Web.Services.MailUtility
{
  internal static class MailHelper
  {
    public static string GetFromEmailAddress
    {
      get
      {
        return "salaam@the72network.com";
      }
    }

    public static string GetSubjectForNewUser
    {
      get
      {
        return "Salaam. Greetings on joining this network. You've become part of an effort to impart knowledge to fellow brethren";
      }
    }

    public static Content GetContentForNewUser
    {
      get
      {
        return new Content("text/plain", "We'll have to insert some html here.");
      }
    }
  }
}
