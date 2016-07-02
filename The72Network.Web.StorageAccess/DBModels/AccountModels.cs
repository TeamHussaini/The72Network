using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace The72Network.Web.StorageAccess.DBModels
{
  [Table("UserProfile")]
  public class UserProfile : BaseEntity
  {
    public string UserName { get; set; }

    public string EmailId { get; set; }

    public string MobilePhone { get; set; }

    public string Country { get; set; }

    public virtual UserExtendedProfile UserExtendedProfile { get; set; }
  }

  [Table("UserExtendedProfile")]
  public class UserExtendedProfile : BaseEntity
  {
    [Key, ForeignKey("UserProfile")]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
    public override int Id { get; set; } 

    public string Profession { get; set; }

    public string Qualifications { get; set; }

    public string AlmaMater { get; set; }

    public string DOB { get; set; }

    public string City { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    [Timestamp]
    public virtual ICollection<Tag> Tags { get; set; }
  }

  [Table("Tag")]
  public class Tag : BaseEntity
  {
    public string TagName { get; set; }

    [Timestamp]
    public virtual ICollection<UserExtendedProfile> Users { get; set; }
  }
}
