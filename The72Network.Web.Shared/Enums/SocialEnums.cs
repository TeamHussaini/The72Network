using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The72Network.Web.Shared.Enums
  {
  public enum RequestState
  {
    Pending = 0,

    Accepted = 1,

    Declined = 2,
  }

  public enum MessageState
  {
    Unseen = 0,

    Seen = 1
  }

  public enum SocialTypes
  {
    None = 0,

    Request = 1,

    Message = 2,
  }
  }
