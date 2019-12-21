using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.Utilities;

namespace XF.Material.Forms.UI.Internals
{
    /// <summary>
    /// Base class of selection control groups. Used by <see cref="MaterialRadioButtonGroup"/> and <see cref="MaterialCheckboxGroup"/>.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract partial class BaseMaterialSelectionControlGroup : ContentView
    {
        internal ObservableCollection<MaterialSelectionControlModel> Models => selectionList.GetValue(BindableLayout.ItemsSourceProperty) as ObservableCollection<MaterialSelectionControlModel>;

        /// <summary>
        /// Backing field for the bindable property <see cref="GroupName"/>.
        /// </summary>
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName), typeof(string), typeof(MaterialCheckboxGroup), string.Empty, BindingMode.OneWay);

        /// <summary>
        /// Backing field for the bindable property <see cref="Choices"/>.
        /// </summary>
        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(nameof(Choices), typeof(IList), typeof(BaseMaterialSelectionControlGroup));

        /// <summary>
        /// Backing field for the bindable property <see cref="FontFamily"/>.
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(BaseMaterialSelectionControlGroup), Material.FontFamily.Body1);

        /// <summary>
        /// Backing field for the bindable property <see cref="FontSize"/>.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(BaseMaterialSelectionControlGroup), Material.GetResource<double>(MaterialConstants.MATERIAL_FONTSIZE_BODY1));

        /// <summary>
        /// Backing field for the bindable property <see cref="HorizontalSpacing"/>.
        /// </summary>
        public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.Create(nameof(HorizontalSpacing), typeof(double), typeof(BaseMaterialSelectionControlGroup), 0.0);

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedColor"/>.
        /// </summary>
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(BaseMaterialSelectionControlGroup), Material.Color.Secondary);

        /// <summary>
        /// Backing field for the bindable property <see cref="TextColor"/>.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseMaterialSelectionControlGroup), Color.FromHex("#DE000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="UnselectedColor"/>.
        /// </summary>
        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(BaseMaterialSelectionControlGroup), Color.FromHex("#84000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.Create(nameof(VerticalSpacing), typeof(double), typeof(BaseMaterialSelectionControlGroup), 0.0);

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedIndices"/>.
        /// </summary>
        public static readonly BindableProperty SelectedIndicesProperty = BindableProperty.Create(nameof(SelectedIndices), typeof(IList<int>), typeof(MaterialCheckboxGroup), new List<int>(), BindingMode.TwoWay);

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedIndicesChangedCommand"/>.
        /// </summary>
        public static readonly BindableProperty SelectedIndicesChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndicesChangedCommand), typeof(Command<int[]>), typeof(MaterialCheckboxGroup));

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedItemsChangedCommand"/>.
        /// </summary>
        public static readonly BindableProperty SelectedItemsChangedCommandProperty = BindableProperty.Create(nameof(SelectedItemsChangedCommand), typeof(Command<IList>), typeof(MaterialCheckboxGroup));

        /// <summary>
        /// Backing field for the bindable property <see cref="NamedGroupSelectedItemsChangedCommand"/>.
        /// </summary>
        public static readonly BindableProperty NamedGroupSelectedItemsChangedCommandProperty = BindableProperty.Create(nameof(NamedGroupSelectedItemsChangedCommand), typeof(Command<NamedGroupList>), typeof(MaterialCheckboxGroup));

        /// <summary>
        /// Backing field for the bindable property <see cref="ChoicesBindingName"/>.
        /// </summary>
        public static readonly BindableProperty ChoicesBindingNameProperty = BindableProperty.Create(nameof(ChoicesBindingName), typeof(string), typeof(BaseMaterialSelectionControlGroup), null);


        internal static readonly BindableProperty ShouldShowScrollbarProperty = BindableProperty.Create(nameof(ShouldShowScrollbar), typeof(bool), typeof(BaseMaterialSelectionControlGroup), false);

        /// <summary>
        /// Gets or sets the name of the property to display of each object in the <see cref="Choices"/> property. This will be ignored if the objects are strings.
        /// </summary>
        public string ChoicesBindingName
        {
            get;
            set;
        }

        internal bool ShouldShowScrollbar
        {
            get => (bool)this.GetValue(ShouldShowScrollbarProperty);
            set => this.SetValue(ShouldShowScrollbarProperty, value);
        }

        /// <summary>
        /// Gets or sets the collection of objects which the user will choose from.
        /// </summary>
        public IList Choices
        {
            get => (IList)this.GetValue(ChoicesProperty);
            set => this.SetValue(ChoicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of the text of each selection controls.
        /// </summary>
        public string FontFamily
        {
            get => (string)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the text of each selection controls.
        /// </summary>
        public double FontSize
        {
            get => (double)this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between the selection control and its text.
        /// </summary>
        public double HorizontalSpacing
        {
            get => (double)this.GetValue(HorizontalSpacingProperty);
            set => this.SetValue(HorizontalSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the color that will be used to tint this control when selected.
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)this.GetValue(SelectedColorProperty);
            set => this.SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the text of each selection control.
        /// </summary>
        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color that will be used to tint this control when unselected.
        /// </summary>
        public Color UnselectedColor
        {
            get => (Color)this.GetValue(UnselectedColorProperty);
            set => this.SetValue(UnselectedColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between each selection control.
        /// </summary>
        public double VerticalSpacing
        {
            get => (double)this.GetValue(VerticalSpacingProperty);
            set => this.SetValue(VerticalSpacingProperty, value);
        }

        /// <summary>
        /// Raised when there is a change in the collection of selected indices.
        /// </summary>
        public event EventHandler<SelectedIndicesChangedEventArgs> SelectedIndicesChanged;

        /// <summary>
        /// Raised when there is a change in the collection of selected items.
        /// </summary>
        public event EventHandler<SelectedItemsChangedEventArgs> SelectedItemsChanged;

        /// <summary>
        /// Raised when there is a change in the collection of selected items.
        /// </summary>
        public event EventHandler<NamedGroupSelectedItemsChangedEventArgs> NamedGroupSelectedItemsChanged;


        /// <summary>
        /// Gets or sets a name for this control.
        /// </summary>
        public string GroupName
        {
            get => (string)this.GetValue(GroupNameProperty);
            set => this.SetValue(GroupNameProperty, value);
        }

        /// <summary>
        /// Gets or sets the indices that are selected.
        /// </summary>
        public IList<int> SelectedIndices
        {
            get => (IList<int>)this.GetValue(SelectedIndicesProperty);
            set => this.SetValue(SelectedIndicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when there is a change in the collection of selected indices.
        /// </summary>
        public Command<int[]> SelectedIndicesChangedCommand
        {
            get => (Command<int[]>)this.GetValue(SelectedIndicesChangedCommandProperty);
            set => this.SetValue(SelectedIndicesChangedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when there is a change in the collection of selected indices.
        /// </summary>
        public Command<IList> SelectedItemsChangedCommand
        {
            get => (Command<IList>)this.GetValue(SelectedItemsChangedCommandProperty);
            set => this.SetValue(SelectedItemsChangedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when there is a change in the collection of selected indices.
        /// </summary>
        public Command<NamedGroupList> NamedGroupSelectedItemsChangedCommand
        {
            get => (Command<NamedGroupList>)this.GetValue(NamedGroupSelectedItemsChangedCommandProperty);
            set => this.SetValue(NamedGroupSelectedItemsChangedCommandProperty, value);
        }

        protected BaseMaterialSelectionControlGroup()
        {
            this.InitializeComponent();
        }

        protected virtual void CreateChoices()
        {
            var models = new ObservableCollection<MaterialSelectionControlModel>();
            var listType = this.Choices[0].GetType();

            for (var i = 0; i < this.Choices.Count; i++)
            {
                var i1 = i;

                var choiceString = "";
                if (!string.IsNullOrEmpty(this.ChoicesBindingName))
                {
                    var propInfo = listType.GetProperty(this.ChoicesBindingName);

                    if (propInfo == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property {this.ChoicesBindingName} was not found for item in {this.Choices}.");
                        choiceString = (string)this.Choices[i];
                    }
                    else
                    {
                        var propValue = propInfo.GetValue(this.Choices[i]);
                        choiceString = propValue.ToString();
                    }
                }
                else
                {
                    choiceString = (string)this.Choices[i];
                }

                var model = new MaterialSelectionControlModel
                {
                    SelectedChangeCommand = new Command<bool>((isSelected) => this.GroupedControlSelected(isSelected, i1)),
                    Text = choiceString,
                    HorizontalSpacing = this.HorizontalSpacing,
                    FontFamily = this.FontFamily,
                    FontSize = this.FontSize,
                    SelectedColor = this.SelectedColor,
                    UnselectedColor = this.UnselectedColor,
                    TextColor = this.TextColor,
                    VerticalSpacing = this.VerticalSpacing
                };

                models.Add(model);
            }

            selectionList.SetValue(BindableLayout.ItemsSourceProperty, models);
        }

        protected virtual void GroupedControlSelected(bool isSelected, int index)
        {
            try
            {
                if (isSelected && !this.SelectedIndices.Contains(index))
                {
                    this.SelectedIndices.Add(index);
                    this.OnSelectedIndicesChanged(this.SelectedIndices);
                }
                else if (!isSelected && this.SelectedIndices.Contains(index))
                {
                    this.SelectedIndices.Remove(index);
                    this.OnSelectedIndicesChanged(this.SelectedIndices);
                }
            }
            catch (NotSupportedException)
            {
                throw new NotSupportedException("Please use a collection type that has no fixed size for the property SelectedIndices");
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.Choices) when this.Choices != null && this.Choices.Count > 0:
                    this.CreateChoices();
                    break;
                case nameof(this.IsEnabled):
                    this.Opacity = this.IsEnabled ? 1.0 : 0.38;
                    break;
                case nameof(this.SelectedIndices):
                    {
                        this.OnSelectedIndicesChanged();
                    }
                    break;
            }
        }

        private void OnSelectedIndicesChanged()
        {
            switch (this.SelectedIndices)
            {
                case null:
                    throw new InvalidOperationException("The property 'SelectedIndices' was assigned with a null value.");
                case Array _:
                    throw new InvalidOperationException("The property 'SelectedIndices' is 'System.Array', please use a collection that has no fixed size");
                default:
                    {
                        if (!this.SelectedIndices.Any())
                        {
                            foreach (var model in this.Models)
                            {
                                model.IsSelected = false;
                            }
                        }
                        else
                        {
                            foreach (var index in this.SelectedIndices)
                            {
                                var model = this.Models?.ElementAt(index);
                                model.IsSelected = true;
                            }
                        }

                        break;
                    }
            }

            this.OnSelectedIndicesChanged(this.SelectedIndices);
        }

        /// <summary>
        /// Called when there is a change in the collection of selected indices.
        /// </summary>
        /// <param name="selectedIndices">The collection of new selected indices.</param>
        protected virtual void OnSelectedIndicesChanged(IList<int> selectedIndices)
        {
            if (this.SelectedItemsChangedCommand != null || this.SelectedItemsChanged != null
                || this.NamedGroupSelectedItemsChangedCommand != null || this.NamedGroupSelectedItemsChanged != null)
            {
                IList items = this.Choices.Subset(selectedIndices.ToArray());
                this.SelectedItemsChangedCommand?.Execute(items);
                this.NamedGroupSelectedItemsChangedCommand?.Execute(new NamedGroupList(this.GroupName, items));
                this.SelectedItemsChanged?.Invoke(this, new SelectedItemsChangedEventArgs(items));
                this.NamedGroupSelectedItemsChanged?.Invoke(this, new NamedGroupSelectedItemsChangedEventArgs(this.GroupName, items));
            }
            if (this.SelectedIndicesChangedCommand != null || this.SelectedIndicesChanged != null)
            {
                int[] indices = selectedIndices.ToArray();
                this.SelectedIndicesChangedCommand?.Execute(indices);
                this.SelectedIndicesChanged?.Invoke(this, new SelectedIndicesChangedEventArgs(indices));
            }
        }
    }
}
