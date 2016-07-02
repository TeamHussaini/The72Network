using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The72Network.Web.StorageAccess.DBModels
{
  public class BaseEntity
  {
    [Key][Column(Order=0)]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
  }
}
