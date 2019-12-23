using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Internals;
using XF.Material.Forms.Utilities;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that allow user to select one or more items from a set.
    /// </summary>
    public class MaterialCheckboxGroup : BaseMaterialSelectionControlGroup
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MaterialCheckboxGroup"/>.
        /// </summary>
        public MaterialCheckboxGroup() : base()
        {
            BindableLayout.SetItemTemplate(this.selectionList, new DataTemplate(() =>
            {
                var checkbox = new MaterialCheckbox()
                {
                    FontFamily = this.FontFamily,
                    FontSize = this.FontSize,
                    HorizontalSpacing = this.HorizontalSpacing,
                    SelectedColor = this.SelectedColor,
                    TextColor = this.TextColor,
                    UnselectedColor = this.UnselectedColor,
                    VerticalSpacing = this.VerticalSpacing
                };

                checkbox.SetBinding(BaseMaterialSelectionControl.FontFamilyProperty, nameof(FontFamily));
                checkbox.SetBinding(BaseMaterialSelectionControl.FontSizeProperty, nameof(FontSize));
                checkbox.SetBinding(BaseMaterialSelectionControl.HorizontalSpacingProperty, nameof(HorizontalSpacing));
                checkbox.SetBinding(BaseMaterialSelectionControl.IsSelectedProperty, nameof(checkbox.IsSelected));
                checkbox.SetBinding(BaseMaterialSelectionControl.SelectedChangeCommandProperty, nameof(checkbox.SelectedChangeCommand));
                checkbox.SetBinding(BaseMaterialSelectionControl.TextProperty, nameof(checkbox.Text));
                checkbox.SetBinding(BaseMaterialSelectionControl.SelectedColorProperty, nameof(this.SelectedColor));
                checkbox.SetBinding(BaseMaterialSelectionControl.TextColorProperty, nameof(this.TextColor));
                checkbox.SetBinding(BaseMaterialSelectionControl.UnselectedColorProperty, nameof(this.UnselectedColor));
                checkbox.SetBinding(BaseMaterialSelectionControl.VerticalSpacingProperty, nameof(this.VerticalSpacing));

                return checkbox;
            }));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialCheckboxGroup"/>.
        /// </summary>
        /// <param name="choices">The list of string which the user will choose from.</param>
        public MaterialCheckboxGroup(IList choices) : this()
        {
            this.Choices = choices;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.Choices):
                    {
                        if (this.SelectedIndices.Any())
                        {
                            foreach (var index in this.SelectedIndices)
                            {
                                var selectedItemModel = this.Models?.ElementAt(index);
                                selectedItemModel.IsSelected = true;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
