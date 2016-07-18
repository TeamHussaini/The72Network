using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Diagnostics;
using System.Threading;
using The72Network.Web.Shared;

namespace The72Network.Web.Services.MailUtility
{
  public class MailService : IMailService
  {
    public MailService()
    {
    }

    public dynamic SendMailToNewUser(string emailId)
    {
      Email from = new Email(MailHelper.GetFromEmailAddress);
      Email to = new Email(emailId);

      Mail mail = new Mail(from, MailHelper.GetSubjectForNewUser, to, MailHelper.GetContentForNewUser);
      dynamic response = null;
      try
      {
        response = _client.Value.client.mail.send.post(requestBody: mail.Get());
      }
      catch (NullReferenceException nrex)
      {
        Trace.TraceError("Possible reason is that the call is made from FakeSendGrid client. ex : {0}", nrex);

        response = string.Empty;
      }
      catch (Exception ex)
      {
        Trace.TraceError("Sending mail failed with following exception ex : {0}", ex.ToString());
      }

      return response;
    }

    #region Privates

    private static Lazy<SendGridAPIClient> _client = new Lazy<SendGridAPIClient>(() =>
    {
      string apiKey = string.Empty;
      try
      {
        apiKey = Environment.GetEnvironmentVariable(Constants.SendGridEnvironmentKey);
      }
      catch (Exception)
      {
        // If failed to retrieve environment variable, this probably means that we are not in azure environment.
        Trace.TraceError("Failed to retrieve Environment Variable");
        return new FakeSendGridClient();
      }

      if (string.IsNullOrWhiteSpace(apiKey))
      {
        Trace.TraceWarning("Failed to retrieve the ApiKey from ED setting");
        return new FakeSendGridClient();
      }
      else
      {
        Trace.TraceInformation("Setting the API key with this value : {0}", apiKey.Substring(0, 5));
        return new SendGridAPIClient(apiKey);
      }
    }, LazyThreadSafetyMode.ExecutionAndPublication);

    #endregion
  }
}
