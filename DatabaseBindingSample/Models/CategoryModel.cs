using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace DatabaseBindingSample.Models
{
    [Table("Categories")]
    public class CategoryModel : BindableBase, ICategory
    {
        public int Id { get; set; }

        private string categoryName, categoryColorString;

        public string CategoryName
        {
            get { return this.categoryName; }
            set
            {
                SetProperty(ref categoryName, value);
                OnPropertyChanged(() => this.DisplayName);
            }
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

        // Relationships
        public virtual ICollection<AppointmentModel> Appointments { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get { return this.CategoryName; }
            set { this.CategoryName = value; }
        }

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