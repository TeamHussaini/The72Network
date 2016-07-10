using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.Shared.DTO;
using The72Network.Web.Shared.Enums;
using The72Network.Web.StorageAccess.UnitOfWork;

namespace The72Network.Web.Services.Request
{
  public interface IRequestService
  {
    IList<RequestDTO> GetPendingRequests(string username);

    bool ProcessRequest(RequestState state, int requestId);

    IList<ConnectedUsersDTO> GetConnectedUsers(string username);
  }
}
