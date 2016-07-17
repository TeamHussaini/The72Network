using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.Shared.DTO;
using The72Network.Web.Shared.Enums;
using The72Network.Web.StorageAccess.Repositories;
using The72Network.Web.StorageAccess.UnitOfWork;

namespace The72Network.Web.Services.Request
{
  public class RequestService : IRequestService
  {
    public IList<RequestDTO> GetPendingRequests(string username)
    {
      using (var socialUnitOfWork = new SocialUnitOfWork())
      {
        var user = socialUnitOfWork.UserProfileRepository.Get(e => e.UserName == username).FirstOrDefault();
        IList<RequestDTO> pendingUserRequests =
          socialUnitOfWork.RequestRepository.Get(e => e.To.Id == user.Id && e.State == RequestState.Pending)
          .Select(x => new RequestDTO(x.From.UserExtendedProfile.Profession, x.From.UserName, x.Id, x.State, x.From.UserExtendedProfile.Tags.Select(t => t.TagName).ToList(), x.Time))
          .ToList();

        return pendingUserRequests;
      }
    }

    public bool ProcessRequest(RequestState state, int requestId)
    {
      bool success = true;
      try
      {
        if (state == RequestState.Pending)
        {
          return true;
        }

        using (var socialUnitOfWork = new SocialUnitOfWork())
        {
          var request = socialUnitOfWork.RequestRepository.Get(e => e.Id == requestId).FirstOrDefault();
          request.State = state;

          socialUnitOfWork.RequestRepository.Update(request);
          socialUnitOfWork.Save();
        }
      }
      catch (Exception ex)
      {
        Trace.TraceError("Failed with following ex : {0}", ex.ToString());
        success = false;
      }

      return success;
    }

    public IList<ConnectedUsersDTO> GetConnectedUsers(string username)
    {
      using (var socialUnitOfWork = new SocialUnitOfWork())
      {
        var user = socialUnitOfWork.UserProfileRepository.Get(e => e.UserName == username).FirstOrDefault();
        var connectedUsers =
          socialUnitOfWork.RequestRepository.Get(e => (e.To.Id == user.Id) && (e.State == RequestState.Accepted))
          .Where(x => x.From != null)
          .Select(x => new ConnectedUsersDTO(
              x.From.UserExtendedProfile.AlmaMater,
              x.From.UserExtendedProfile.City,
              x.From.Country,
              x.From.UserExtendedProfile.Description,
              x.From.UserExtendedProfile.DOB,
              x.From.EmailId,
              x.From.UserExtendedProfile.ImageUrl,
              x.From.MobilePhone,
              x.From.UserExtendedProfile.Profession,
              x.From.UserExtendedProfile.Qualifications,
              x.From.UserExtendedProfile.Tags.Select(t => t.TagName).ToList(),
              x.From.UserName)).ToList();

        connectedUsers.AddRange(
          socialUnitOfWork.RequestRepository.Get(e => (e.From.Id == user.Id) && (e.State == RequestState.Accepted))
          .Where(x => x.To != null)
          .Select(x => new ConnectedUsersDTO(
              x.To.UserExtendedProfile.AlmaMater,
              x.To.UserExtendedProfile.City,
              x.To.Country,
              x.To.UserExtendedProfile.Description,
              x.To.UserExtendedProfile.DOB,
              x.To.EmailId,
              x.To.UserExtendedProfile.ImageUrl,
              x.To.MobilePhone,
              x.To.UserExtendedProfile.Profession,
              x.To.UserExtendedProfile.Qualifications,
              x.To.UserExtendedProfile.Tags.Select(t => t.TagName).ToList(),
              x.To.UserName)).ToList());

        return connectedUsers.ToList();
      }
    }
  }
}
