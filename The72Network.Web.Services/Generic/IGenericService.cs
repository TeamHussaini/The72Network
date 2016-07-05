using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.Shared.Enums;
using The72Newtork.Web.Shared.Wrappers;

namespace The72Network.Web.Services.Generic
{
  public interface IGenericService
  {
    string GetUserFromTagsAsString(string username, IEnumerable<int> tagIds, TagSearchConfiguration tagSearchConfig, int tagsCount);
  }
}
