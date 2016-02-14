using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace The72Network.Models
{
  public class UsersContext : DbContext
  {
    public UsersContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
  }

  [Table("UserProfile")]
  public class UserProfile : Entity
  {
    public string UserName { get; set; }
    public string EmailId { get; set; }
    public string MobilePhone { get; set; }
    public string Country { get; set; }
  }

  [Table("UserExtendedProfile")]
  public class UserExtendedProfile : Entity
  {
    [ForeignKey("UserProfile")]
    public int UserId { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    public string Profession { get; set; }

    public string Qualification { get; set; }

    public string AlmaMater { get; set; }

    public string DOB { get; set; }

    public string City { get; set; }

    public string Tags { get; set; }

    public string ImageUrl { get; set; }
  }

  public class Entity
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
  }


  public class RegisterExternalLoginModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    public string ExternalLoginData { get; set; }
  }

  public class LocalPasswordModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }

  public class RegisterModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "Email Id")]
    public string EmailId { get; set; }


    [Required]
    [Display(Name = "Mobile Phone")]
    public string MobilePhone { get; set; }

    [Required]
    [Display(Name = "Country")]
    public string Country { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class UserExtendedProfileModel
  {
    [Required]
    [Display(Name = "Profession")]
    public string Profession { get; set; }

    [Required]
    [Display(Name = "Qualification")]
    public string Qualification { get; set; }

    [Required]
    [Display(Name = "Alma Mater")]
    public string AlmaMater { get; set; }

    [Required]
    [Display(Name = "Date of Birth")]
    public string DOB { get; set; }

    [Required]
    [Display(Name = "City")]
    public string City { get; set; }

    [Required]
    [Display(Name = "Tags")]
    public string Tags { get; set; }


    [Display(Name = "Image")]
    public string ImageUrl { get; set; }
  }

  public class ExternalLogin
  {
    public string Provider { get; set; }
    public string ProviderDisplayName { get; set; }
    public string ProviderUserId { get; set; }
  }
}
