using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Store.Model
{
    [MetadataType(typeof(RegionMetadata))]
    public partial class Region
    {
        public class RegionMetadata
        {
            [Required(ErrorMessageResourceType = 
                typeof(Store.Resources.ApplicationMessages),
                ErrorMessageResourceName = "RegionIDRequired")]
            public int RegionID { get; set; }

            [Display(Name = "RegionDescriptionLabel", 
                ResourceType = typeof(Store.Resources.ApplicationMessages))]
            [Required(
                ErrorMessageResourceType = typeof(Store.Resources.ApplicationMessages),
                ErrorMessageResourceName = "RegionDescriptionRequired")]
            [StringLength(50,
                MinimumLength = 4,
                ErrorMessageResourceType = typeof(Store.Resources.ApplicationMessages),
                ErrorMessageResourceName = "RegionDescriptionLength")]
            public string RegionDescription { get; set; }
        }
    }
}
