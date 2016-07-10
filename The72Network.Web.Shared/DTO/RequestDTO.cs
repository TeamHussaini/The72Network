using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.Shared.Enums;

namespace The72Network.Web.Shared.DTO
{
  public class RequestDTO
  {
    public RequestDTO(string profession, string username, int requestId, RequestState requestState, IList<string> tags, DateTime timeStamp)
    {
      _profession = profession;
      _username = username;
      _requestId = requestId;
      _requestState = requestState;
      _tags = tags;
      _timeStamp = timeStamp;
    }

    public string Profession
    {
      get
      {
        return _profession;
      }
    }

    public string Username
    {
      get
      {
        return _username;
      }
    }

    public int RequestId
    {
      get
      {
        return _requestId;
      }
    }

    public RequestState State
    {
      get
      {
        return _requestState;
      }
    }

    public IList<string> Tags
    {
      get
      {
        return _tags;
      }
    }

    public DateTime TimeStamp
    {
      get
      {
        return _timeStamp;
      }
    }

    private string _profession;

    private string _username;

    private int _requestId;

    private RequestState _requestState;

    private IList<string> _tags;

    private DateTime _timeStamp;
  }
}
