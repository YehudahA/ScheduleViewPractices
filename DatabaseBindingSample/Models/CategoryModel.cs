using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace DatabaseBindingSample.Models
{
    [Table("Categories")]
    public class CategoryModel : BindableBase, ICategory
    {
        [Key]
        public int Id { get; set; }

        private string displayName;
        private string categoryColorString;

        public string DisplayName
        {
            get { return displayName; }
            set { SetProperty(ref displayName, value); }
        }

        public string CategoryColorString
        {
            get { return categoryColorString; }
            set
            {
                if (SetProperty(ref categoryColorString, value))
                {
                    base.OnPropertyChanged(() => CategoryBrush);
                    base.OnPropertyChanged(() => CategoryColor);
                }
            }
        }

        [NotMapped]
        public string CategoryName
        {
            get { return this.DisplayName; }
            set { this.DisplayName = value; }
        }

        // Relationships
        public virtual ICollection<AppointmentModel> Appointments { get; set; }

        #region color properties

        public Color CategoryColor
        {
            get
            {
                Color color = Helpers.ColorHelper.ParseOrDefault(categoryColorString, Colors.White);
                return color;
            }
            set { CategoryColorString = value.ToString(); }
        }

        public Brush CategoryBrush
        {
            get { return new SolidColorBrush(CategoryColor); }
        }

        #endregion // color properties

        #region IEquatable<ICategory>

        public bool Equals(ICategory other)
        {
            CategoryModel category = other as CategoryModel;

            if (category == null)
                return false;

            return category.Id == this.Id;
        }

        #endregion // IEquatable<ICategory>
    }
}