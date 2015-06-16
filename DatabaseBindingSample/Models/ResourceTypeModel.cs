using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls;

namespace DatabaseBindingSample.Models
{
    [Table("ResourceTypes")]
    public class ResourceTypeModel : IResourceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AllowMultipleSelection { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        public virtual List<ResourceModel> Resources { get; set; }

        System.Collections.IList IResourceType.Resources
        {
            get { return this.Resources; }
        }
    }
}
