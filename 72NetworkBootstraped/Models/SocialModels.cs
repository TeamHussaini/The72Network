using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _72NetworkBootstraped.Models
{
  #region Entity

  [Table("Requests")]
  public class Requests : Entity
  {
    [Key][Column(Order=1)]
    public UserProfile From { get; set; }

    [Key][Column(Order=2)]
    public UserProfile To { get; set; }

    public DateTime Time { get; set; }

    [EnumDataType(typeof(RequestState))]
    public RequestState State { get; set; }
  }

  [Table("Messages")]
  public class Messages : Entity
  {
    public UserProfile From { get; set; }

    public UserProfile To { get; set; }

    public DateTime Time { get; set; }

    public string Message {get; set;}

    public Guid Correlation { get; set; }

    [EnumDataType(typeof(MessageState))]
    public MessageState State { get; set; }
  }

  [Table("SocialManager")]
  public class SocialManager : Entity
  {
    [Key][Column(Order=1)]
    public UserProfile User { get; set; }

    public int SocialId { get; set; }

    [EnumDataType(typeof(SocialTypes))]
    public SocialTypes SocialType { get; set; }

    public DateTime TimeStamp { get; set; }

    public int Count { get; set; }
  }

  #endregion

  #region Models

  public class RequestPageViewModel
  {
    public string UserName { get; set; }

    public string Profession { get; set; }

    public int RequestId { get; set; }

    public DateTime TimeStamp { get; set; }

    public RequestState State { get; set; }

    public List<string> Tags { get; set; }
  }

  public class RequestPostModel
  {
    public int RequestId { get; set; }

    public string State { get; set; }
  }

  public class MessageModel
  {
    [Required]
    public UserProfile From { get; set; }

    [Required]
    public UserProfile To { get; set; }

    [Required]
    public DateTime Time { get; set; }

    [Required]
    public string Message { get; set; }

    [Required]
    public Guid Correlation { get ; set; }
  }

  #endregion

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