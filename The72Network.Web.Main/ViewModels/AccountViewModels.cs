using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace The72Network.Web.Main.ViewModels
{
  public class RegisterExternalLoginModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName
    {
      get;
      set;
    }

    public string ExternalLoginData
    {
      get;
      set;
    }
  }

  public class LocalPasswordModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword
    {
      get;
      set;
    }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Min password length : {2}, Max password length : {1}", MinimumLength = 6)]
    [Display(Name = "New password")]
    public string NewPassword
    {
      get;
      set;
    }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword
    {
      get;
      set;
    }
  }

  public class SignInModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName
    {
      get;
      set;
    }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password
    {
      get;
      set;
    }

    [Display(Name = "Remember me")]
    public bool RememberMe
    {
      get;
      set;
    }
  }

  // SignUp model is UserProfile Model
  public class SignUpModel
  {
    [Required]
    [Display(Name = "User name")]
    [Remote("IsUserUnique", "Account", ErrorMessage = "UserName already in use.")]
    public string UserName
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Email Id")]
    [EmailAddress(ErrorMessage = "Email-Id is not valid.")]
    [Remote("IsEmailUnique", "Account", ErrorMessage = "This emailId has already been registered with another username.")]
    public string EmailId
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Mobile Phone")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
    public string MobilePhone
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Country")]
    public string Country
    {
      get;
      set;
    }

    [Required]
    [StringLength(100, ErrorMessage = "Min password length : {2}, Max password length : {1}", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password
    {
      get;
      set;
    }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword
    {
      get;
      set;
    }
  }

  public class UserExtendedProfileModel
  {
    [Required]
    [Display(Name = "Profession")]
    public string Profession
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Qualifications")]
    public string Qualifications
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Alma Mater")]
    public string AlmaMater
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Date of Birth")]
    [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
      ErrorMessage = "Entered DOB is not valid.")]
    public string DOB
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "City")]
    public string City
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Tags")]
    public List<int> Tags
    {
      get;
      set;
    }

    [Required]
    [Display(Name = "Description")]
    [StringLength(500, ErrorMessage = "{0} length should not exceed {1} characters.")]
    public string Description
    {
      get;
      set;
    }

    [Display(Name = "Image")]
    public string ImageUrl
    {
      get;
      set;
    }
  }

  public class CompleteUserProfileViewModel
  {
    public string UserName
    {
      get;
      set;
    }

    public string EmailId
    {
      get;
      set;
    }

    public string MobilePhone
    {
      get;
      set;
    }

    public string Country
    {
      get;
      set;
    }

    public string Profession
    {
      get;
      set;
    }

    public string Qualifications
    {
      get;
      set;
    }

    public string AlmaMater
    {
      get;
      set;
    }

    public string DOB
    {
      get;
      set;
    }

    public string City
    {
      get;
      set;
    }

    public IList<string> Tags
    {
      get;
      set;
    }

    public string Description
    {
      get;
      set;
    }

    public string ImageUrl
    {
      get;
      set;
    }
  }

  public class ExternalLogin
  {
    public string Provider
    {
      get;
      set;
    }
    public string ProviderDisplayName
    {
      get;
      set;
    }
    public string ProviderUserId
    {
      get;
      set;
    }
  }

  public class InboxModel
  {
  }
}
