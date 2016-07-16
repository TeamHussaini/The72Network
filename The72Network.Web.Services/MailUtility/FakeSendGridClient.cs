using SendGrid;

namespace The72Network.Web.Services.MailUtility
{
  internal class FakeSendGridClient : SendGridAPIClient
  {
    public FakeSendGridClient(string apiKey = "")
      : base(apiKey)
    {
    }
  }
}
