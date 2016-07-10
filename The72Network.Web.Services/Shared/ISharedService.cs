using The72Network.Web.Shared.Enums;

namespace The72Network.Web.Services.Shared
{
  public interface ISharedService
  {
    bool TrySaveSocialType(string username, int profileId, SocialTypes socialType);
  }
}
