using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The72Network.Web.Shared.DTO
{
  public class ConnectedUsersDTO
  {
    public ConnectedUsersDTO(string almaMater, string city, string country, string descriptions, string dob, string emailId,
      string imageUrl, string mobilePhone, string profession, string qualifications, IList<string> tags, string username)
    {
      _almaMater = almaMater;
      _city = city;
      _country = country;
      _description = descriptions;
      _dob = dob;
      _emailId = emailId;
      _imageUrl = imageUrl;
      _mobilePhone = mobilePhone;
      _profession = profession;
      _qualifications = qualifications;
      _tags = tags;
      _username = username;
    }
    public string UserName
    {
      get
      {
        return _username;
      }
    }

    public string EmailId
    {
      get
      {
        return _emailId;
      }
    }

    public string MobilePhone
    {
      get
      {
        return _mobilePhone;
      }
    }

    public string Country
    {
      get
      {
        return _country;
      }
    }

    public string Profession
    {
      get
      {
        return _profession;
      }
    }

    public string Qualifications
    {
      get
      {
        return _qualifications;
      }
    }

    public string AlmaMater
    {
      get
      {
        return _almaMater;
      }
    }

    public string DOB
    {
      get
      {
        return _dob;
      }
    }

    public string City
    {
      get
      {
        return _city;
      }
    }

    public IList<string> Tags
    {
      get
      {
        return _tags;
      }
    }

    public string Description
    {
      get
      {
        return _description;
      }
    }

    public string ImageUrl
    {
      get
      {
        return _imageUrl;
      }
    }

    private string _almaMater;

    private string _city;

    private string _country;

    private string _description;

    private string _dob;

    private string _emailId;

    private string _imageUrl;

    private string _mobilePhone;

    private string _profession;

    private string _qualifications;

    private IList<string> _tags;

    private string _username;
  }
}
