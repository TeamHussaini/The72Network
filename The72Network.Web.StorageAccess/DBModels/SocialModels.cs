using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using The72Network.Web.Shared.Enums;

namespace The72Network.Web.StorageAccess.DBModels
{
  [Table("Requests")]
  public class Requests : BaseEntity
  {
    [Key][Column(Order=1)]
    public virtual UserProfile From { get; set; }

    [Key][Column(Order=2)]
    public virtual UserProfile To { get; set; }

    public DateTime Time { get; set; }

    [EnumDataType(typeof(RequestState))]
    public RequestState State { get; set; }
  }

  [Table("Messages")]
  public class Messages : BaseEntity
  {
    public virtual UserProfile From { get; set; }

    public virtual UserProfile To { get; set; }

    public DateTime Time { get; set; }

    public string Message {get; set;}

    public Guid Correlation { get; set; }

    [EnumDataType(typeof(MessageState))]
    public MessageState State { get; set; }
  }

  [Table("SocialManager")]
  public class SocialManager : BaseEntity
  {
    [Key][Column(Order=1)]
    public virtual UserProfile User { get; set; }

    public int SocialId { get; set; }

    [EnumDataType(typeof(SocialTypes))]
    public SocialTypes SocialType { get; set; }

    public DateTime TimeStamp { get; set; }

    public int Count { get; set; }
  }
}