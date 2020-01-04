using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that allow user to select one item from a set.
    /// </summary>
    public class MaterialRadioButtonGroup : BaseMaterialSelectionControlGroup
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MaterialRadioButtonGroup"/>.
        /// </summary>
        public MaterialRadioButtonGroup()
        {
            BindableLayout.SetItemTemplate(this.selectionList, new DataTemplate(() =>
            {
                var radio = new MaterialRadioButton()
                {
                    FontFamily = this.FontFamily,
                    FontSize = this.FontSize,
                    HorizontalSpacing = this.HorizontalSpacing,
                    SelectedColor = this.SelectedColor,
                    TextColor = this.TextColor,
                    UnselectedColor = this.UnselectedColor,
                    VerticalSpacing = this.VerticalSpacing
                };

                radio.SetBinding(BaseMaterialSelectionControl.FontFamilyProperty, nameof(FontFamily));
                radio.SetBinding(BaseMaterialSelectionControl.FontSizeProperty, nameof(FontSize));
                radio.SetBinding(BaseMaterialSelectionControl.HorizontalSpacingProperty, nameof(HorizontalSpacing));
                radio.SetBinding(BaseMaterialSelectionControl.IsSelectedProperty, nameof(radio.IsSelected));
                radio.SetBinding(BaseMaterialSelectionControl.SelectedChangeCommandProperty, nameof(radio.SelectedChangeCommand));
                radio.SetBinding(BaseMaterialSelectionControl.TextProperty, nameof(radio.Text));
                radio.SetBinding(BaseMaterialSelectionControl.SelectedColorProperty, nameof(this.SelectedColor));
                radio.SetBinding(BaseMaterialSelectionControl.TextColorProperty, nameof(this.TextColor));
                radio.SetBinding(BaseMaterialSelectionControl.UnselectedColorProperty, nameof(this.UnselectedColor));
                radio.SetBinding(BaseMaterialSelectionControl.VerticalSpacingProperty, nameof(this.VerticalSpacing));

                return radio;
            }));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialRadioButtonGroup"/>.
        /// </summary>
        /// <param name="choices">The list of string which the user will choose from.</param>
        public MaterialRadioButtonGroup(IList choices) : this()
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
                        this.SelectedIndices.Remove(-1);
                        if (this.SelectedIndices.Any())
                        {
                            var selectedItemModel = this.Models?.ElementAt(this.SelectedIndices[0]);
                            selectedItemModel.IsSelected = true;
                        }
                    }
                    break;
            }
        }

        protected override void GroupedControlSelected(bool isSelected, int index)
        {
            this.SelectedIndices.Remove(-1);

            if (isSelected && !this.SelectedIndices.Contains(index))
            {
                if (this.SelectedIndices.Any())
                {
                    // Clear out any other selections as a radio is a single choice

                    //Take a copy of the collection as we will be making modifications
                    var items = this.SelectedIndices.ToArray();
                    foreach (var selectedIndex in items)
                    {
                        var selectedItemModel = this.Models?.ElementAt(selectedIndex);
                        selectedItemModel.IsSelected = false;
                    }
                }

                // Clear out any other indicies as a radio is a single choice
                //this.SelectedIndices.Clear();

                if (this.SelectedIndices.Count == 0)
                {
                    this.SelectedIndices.Add(index);
                }
                else
                {
                    this.SelectedIndices[0] = index;
                }
                this.OnSelectedIndicesChanged(this.SelectedIndices);
            }
            else if (!isSelected && this.SelectedIndices.Contains(index))
            {
                this.SelectedIndices[0] = -1;
                this.OnSelectedIndicesChanged(this.SelectedIndices);
            }
        }

        public void SetSelection(int selectedIndex)
        {
            this.SelectedIndices.Remove(-1);
            this.GroupedControlSelected(true, selectedIndex);
            var selectedItemModel = this.Models?.ElementAt(selectedIndex);
            selectedItemModel.IsSelected = true;
        }
    }
}