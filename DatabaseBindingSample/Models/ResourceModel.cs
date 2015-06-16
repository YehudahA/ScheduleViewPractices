using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls;

namespace DatabaseBindingSample.Models
{
    [Table("Resources")]
    public class ResourceModel : IResource
    {
        [Key]
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public int ResourceTypeId { get; set; }

        [ForeignKey("ResourceTypeId")]
        public virtual ResourceTypeModel ResourceType { get; set; }

        public virtual ICollection<AppointmentModel> Appointments { get; set; }
        public virtual ICollection<ExceptionAppointmentModel> ExceptionAppointments { get; set; }


        [NotMapped]
        public string DisplayName
        {
            get { return this.ResourceName; }
            set { this.ResourceName = value; }
        }

        // It is NOT necessary
        string IResource.ResourceType
        {
            get { return this.ResourceType.Name; }
            set { throw new System.NotImplementedException(); }
        }

        public bool Equals(IResource other)
        {
            ResourceModel resource = other as ResourceModel;

            return resource != null
                && resource.Id == this.Id;
        }
    }
}