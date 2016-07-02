using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.Shared.Enums;
using The72Network.Web.StorageAccess.DBModels;

namespace The72Network.Web.Main.ViewModels
{
  public class RequestPageViewModel
  {
    public string UserName
    {
      get;
      set;
    }

    public string Profession
    {
      get;
      set;
    }

    public int RequestId
    {
      get;
      set;
    }

    public DateTime TimeStamp
    {
      get;
      set;
    }

    public RequestState State
    {
      get;
      set;
    }

    public List<string> Tags
    {
      get;
      set;
    }
  }

  public class RequestPostModel
  {
    public int RequestId
    {
      get;
      set;
    }

    public string State
    {
      get;
      set;
    }
  }

  public class MessageModel
  {
    [Required]
    public UserProfile From
    {
      get;
      set;
    }

    [Required]
    public UserProfile To
    {
      get;
      set;
    }

    [Required]
    public DateTime Time
    {
      get;
      set;
    }

    [Required]
    public string Message
    {
      get;
      set;
    }

    [Required]
    public Guid Correlation
    {
      get;
      set;
    }
  }
}
