using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Internals;
using XF.Material.Forms.Utilities;

namespace XF.Material.Forms.UI.Internals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseMaterialInputView : ContentView, IMaterialElementConfiguration
    {
        internal CommonTextControlAccessor InputControl;

        public static readonly BindableProperty AlwaysShowUnderlineProperty = BindableProperty.Create(nameof(AlwaysShowUnderline), typeof(bool), typeof(BaseMaterialInputView), false);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#DCDCDC"));

        public static readonly BindableProperty ChoiceSelectedCommandProperty = BindableProperty.Create(nameof(ChoiceSelectedCommand), typeof(ICommand), typeof(BaseMaterialInputView));

        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(nameof(Choices), typeof(IList), typeof(BaseMaterialInputView));

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(BaseMaterialInputView), Material.Color.Error);

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(BaseMaterialInputView));

        public static readonly BindableProperty FloatingPlaceholderColorProperty = BindableProperty.Create(nameof(FloatingPlaceholderColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#99000000"));

        public static readonly BindableProperty FloatingPlaceholderEnabledProperty = BindableProperty.Create(nameof(FloatingPlaceholderEnabled), typeof(bool), typeof(BaseMaterialInputView), true);

        public static readonly BindableProperty FloatingPlaceholderFontSizeProperty = BindableProperty.Create(nameof(FloatingPlaceholderFontSize), typeof(double), typeof(BaseMaterialInputView), 0d);

        public static readonly BindableProperty FocusCommandProperty = BindableProperty.Create(nameof(FocusCommand), typeof(Command<bool>), typeof(BaseMaterialInputView));

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(BaseMaterialInputView), false);

        public static readonly BindableProperty HelperTextColorProperty = BindableProperty.Create(nameof(HelperTextColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#99000000"));

        public static readonly BindableProperty HelperTextFontFamilyProperty = BindableProperty.Create(nameof(HelperTextFontFamily), typeof(string), typeof(BaseMaterialInputView));

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(BaseMaterialInputView), string.Empty);

        public static readonly BindableProperty HorizontalPaddingProperty = BindableProperty.Create(nameof(HorizontalPadding), typeof(MaterialHorizontalThickness), typeof(BaseMaterialInputView), new MaterialHorizontalThickness(12d), defaultBindingMode: BindingMode.OneTime);

        public static readonly BindableProperty InputTypeProperty = BindableProperty.Create(nameof(InputType), typeof(MaterialTextFieldInputType), typeof(BaseMaterialInputView), MaterialTextFieldInputType.Default);

        public static readonly BindableProperty IsAutoCapitalizationEnabledProperty = BindableProperty.Create(nameof(IsAutoCapitalizationEnabled), typeof(bool), typeof(BaseMaterialInputView), false);

        public static readonly BindableProperty IsMaxLengthCounterVisibleProperty = BindableProperty.Create(nameof(IsMaxLengthCounterVisible), typeof(bool), typeof(BaseMaterialInputView), true);

        public static readonly BindableProperty IsSpellCheckEnabledProperty = BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(BaseMaterialInputView), false);

        public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(BaseMaterialInputView), false);

        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(string), typeof(BaseMaterialInputView));

        public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#99000000"));

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(BaseMaterialInputView), 0);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#99000000"));

        public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(BaseMaterialInputView));

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(BaseMaterialInputView), string.Empty);

        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(BaseMaterialInputView));

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(BaseMaterialInputView));

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(BaseMaterialInputView), ReturnType.Default);

        public static readonly BindableProperty ShouldAnimateUnderlineProperty = BindableProperty.Create(nameof(ShouldAnimateUnderline), typeof(bool), typeof(BaseMaterialInputView), true);

        public static readonly BindableProperty TextChangeCommandProperty = BindableProperty.Create(nameof(TextChangeCommand), typeof(Command<string>), typeof(BaseMaterialInputView));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#D0000000"));

        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(BaseMaterialInputView));

        public static readonly BindableProperty TextFontSizeProperty = BindableProperty.Create(nameof(TextFontSize), typeof(double), typeof(BaseMaterialInputView), 16d);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(BaseMaterialInputView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(BaseMaterialInputView), Material.Color.Secondary);

        public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(string), typeof(BaseMaterialInputView));

        public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#99000000"));

        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(BaseMaterialInputView), Color.FromHex("#99000000"));

        public static readonly BindableProperty IsMultiLineProperty = BindableProperty.Create(nameof(IsMultiLine), typeof(bool), typeof(BaseMaterialInputView), false);

        private const double AnimationDuration = 0.35;
        private readonly Easing _animationCurve = Easing.SinOut;
        private bool _counterEnabled;
        private DisplayOrientation DisplayOrientation;
        private List<int> _selectedIndicies = new List<int>();
        private bool _wasFocused;
        protected Dictionary<string, Action> _propertyChangeActions;
        private double? _cachedInputHeight;

        /// <summary>
        /// Initializes a new instance of <see cref="BaseMaterialInputView"/>.
        /// </summary>
        public BaseMaterialInputView(View inputControl)
        {
            InputControl = new CommonTextControlAccessor(inputControl);
            this.InitializeComponent();
            this._inputContainer.Content = inputControl;
            this.SetPropertyChangeHandler(ref _propertyChangeActions);
            this.SetControl();
            this.DisplayOrientation = DeviceDisplay.MainDisplayInfo.Orientation;
        }

        public event EventHandler<SelectedItemChangedEventArgs> ChoiceSelected;

        /// <summary>
        /// Raised when this text field receives focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Focused;

        /// <summary>
        /// Raised when this text field loses focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Unfocused;

        /// <summary>
        /// Raised when the input text of this text field has changed.
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// Raised when the user finalizes the input on this text field using the return key.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Gets or sets whether the underline accent of this text field should always show or not.
        /// </summary>
        public bool AlwaysShowUnderline
        {
            get => (bool)this.GetValue(AlwaysShowUnderlineProperty);
            set => this.SetValue(AlwaysShowUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of this text field.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the collection of objects which the user will choose from. This is required when <see cref="InputType"/> is set to <see cref="BaseMaterialInputViewInputType.Choice"/> or <see cref="BaseMaterialInputViewInputType.MultiChoice"/>.
        /// </summary>
        public IList Choices
        {
            get => (IList)this.GetValue(ChoicesProperty);
            set => this.SetValue(ChoicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the name of the property to display of each object in the <see cref="Choices"/> property. This will be ignored if the objects are strings.
        /// </summary>
        public string ChoicesBindingName { get; set; }

        /// <summary>
        /// Gets or sets the command that will execute if a choice was selected when the <see cref="InputType"/> is set to <see cref="BaseMaterialInputViewInputType.Choice"/> or <see cref="BaseMaterialInputViewInputType.MultiChoice"/>.
        /// </summary>
        public ICommand ChoiceSelectedCommand
        {
            get => (ICommand)this.GetValue(ChoiceSelectedCommandProperty);
            set => this.SetValue(ChoiceSelectedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color to indicate an error in this text field.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Error"/>.
        /// </summary>
        public Color ErrorColor
        {
            get => (Color)this.GetValue(ErrorColorProperty);
            set => this.SetValue(ErrorColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the error text of this text field.
        /// </summary>
        public string ErrorText
        {
            get => (string)this.GetValue(ErrorTextProperty);
            set => this.SetValue(ErrorTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the floating placeholder.
        /// </summary>
        public Color FloatingPlaceholderColor
        {
            get => (Color)this.GetValue(FloatingPlaceholderColorProperty);
            set => this.SetValue(FloatingPlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the placeholder label will float at top of the text field when focused or when it has text.
        /// </summary>
        public bool FloatingPlaceholderEnabled
        {
            get => (bool)this.GetValue(FloatingPlaceholderEnabledProperty);
            set => this.SetValue(FloatingPlaceholderEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the floating placeholder.
        /// </summary>
        public double FloatingPlaceholderFontSize
        {
            get => (double)this.GetValue(FloatingPlaceholderFontSizeProperty);
            set => this.SetValue(FloatingPlaceholderFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will be executed when this text field receives or loses focus.
        /// </summary>
        public Command<bool> FocusCommand
        {
            get => (Command<bool>)this.GetValue(FocusCommandProperty);
            set => this.SetValue(FocusCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the boolean value whether this text field has an error, and if it will show the its error text.
        /// </summary>
        public bool HasError
        {
            get => (bool)this.GetValue(HasErrorProperty);
            set => this.SetValue(HasErrorProperty, value);
        }

        /// <summary>
        /// Gets or sets the helper text of this text field.
        /// </summary>
        public string HelperText
        {
            get => (string)this.GetValue(HelperTextProperty);
            set => this.SetValue(HelperTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text field's helper text.
        /// </summary>
        public Color HelperTextColor
        {
            get => (Color)this.GetValue(HelperTextColorProperty);
            set => this.SetValue(HelperTextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's helper text.
        /// </summary>
        public string HelperTextFontFamily
        {
            get => (string)this.GetValue(HelperTextFontFamilyProperty);
            set => this.SetValue(HelperTextFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the horizontal padding of the text field.
        /// </summary>
        public MaterialHorizontalThickness HorizontalPadding
        {
            get => (MaterialHorizontalThickness)this.GetValue(HorizontalPaddingProperty);
            set => this.SetValue(HorizontalPaddingProperty, value);
        }

        /// <summary>
        /// Gets or sets the keyboard input type of this text field.
        /// </summary>
        public MaterialTextFieldInputType InputType
        {
            get => (MaterialTextFieldInputType)this.GetValue(InputTypeProperty);
            set => this.SetValue(InputTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets whether auto capitialization is enabled.
        /// </summary>
        public bool IsAutoCapitalizationEnabled
        {
            get => (bool)this.GetValue(IsAutoCapitalizationEnabledProperty);
            set => this.SetValue(IsAutoCapitalizationEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the counter for the max input length of this text field is visible or not.
        /// </summary>
        public bool IsMaxLengthCounterVisible
        {
            get => (bool)this.GetValue(IsMaxLengthCounterVisibleProperty);
            set => this.SetValue(IsMaxLengthCounterVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets whether spell checking is enabled.
        /// </summary>
        public bool IsSpellCheckEnabled
        {
            get => (bool)this.GetValue(IsSpellCheckEnabledProperty);
            set => this.SetValue(IsSpellCheckEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets whether text prediction is enabled.
        /// </summary>
        public bool IsTextPredictionEnabled
        {
            get => (bool)this.GetValue(IsTextPredictionEnabledProperty);
            set => this.SetValue(IsTextPredictionEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        public string LeadingIcon
        {
            get => (string)this.GetValue(LeadingIconProperty);
            set => this.SetValue(LeadingIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the icon of this text field.
        /// </summary>
        public Color LeadingIconTintColor
        {
            get => (Color)this.GetValue(LeadingIconTintColorProperty);
            set => this.SetValue(LeadingIconTintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum allowed number of characters in this text field.
        /// </summary>
        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets the text of this text field's placeholder.
        /// </summary>
        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(this.Placeholder)} must not be null, empty, or a white space.");
                }

                this.SetValue(PlaceholderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of this text field's placeholder.
        /// </summary>
        public Color PlaceholderColor
        {
            get => (Color)this.GetValue(PlaceholderColorProperty);
            set => this.SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's placeholder
        /// </summary>
        public string PlaceholderFontFamily
        {
            get => (string)this.GetValue(PlaceholderFontFamilyProperty);
            set => this.SetValue(PlaceholderFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will run when the user returns the input in this textfield.
        /// </summary>
        public ICommand ReturnCommand
        {
            get => (ICommand)this.GetValue(ReturnCommandProperty);
            set => this.SetValue(ReturnCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter of <see cref="ReturnCommand"/>.
        /// </summary>
        public object ReturnCommandParameter
        {
            get => this.GetValue(ReturnCommandParameterProperty);
            set => this.SetValue(ReturnCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the return type of this textfield.
        /// </summary>
        public ReturnType ReturnType
        {
            get => (ReturnType)this.GetValue(ReturnTypeProperty);
            set => this.SetValue(ReturnTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the underline indicator will be animated. If set to false, the underline will not be shown.
        /// </summary>
        public bool ShouldAnimateUnderline
        {
            get => (bool)this.GetValue(ShouldAnimateUnderlineProperty);
            set => this.SetValue(ShouldAnimateUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the input text of this text field.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute if there is a change in this text field's input text.
        /// </summary>
        public Command<string> TextChangeCommand
        {
            get => (Command<string>)this.GetValue(TextChangeCommandProperty);
            set => this.SetValue(TextChangeCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text field's input text.
        /// </summary>
        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's input text.
        /// </summary>
        public string TextFontFamily
        {
            get => (string)this.GetValue(TextFontFamilyProperty);
            set => this.SetValue(TextFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the text's font size.
        /// </summary>
        public double TextFontSize
        {
            get => (double)this.GetValue(TextFontSizeProperty);
            set => this.SetValue(TextFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the underline and the placeholder of this text field when focused.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the right side of this text field.
        /// </summary>
        public string TrailingIcon
        {
            get => (string)this.GetValue(TrailingIconProperty);
            set => this.SetValue(TrailingIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the trailing icon of this text field.
        /// </summary>
        public Color TrailingIconTintColor
        {
            get => (Color)this.GetValue(TrailingIconTintColorProperty);
            set => this.SetValue(TrailingIconTintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the underline when this text field is activated. <see cref="AlwaysShowUnderline"/> is set to true.
        /// </summary>
        public Color UnderlineColor
        {
            get => (Color)this.GetValue(UnderlineColorProperty);
            set => this.SetValue(UnderlineColorProperty, value);
        }

        public bool IsMultiLine
        {
            get => (bool)this.GetValue(IsMultiLineProperty);
            set => this.SetValue(IsMultiLineProperty, value);
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool IsFocusedInternal
        {
            get;
            set;
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ElementChanged(bool created)
        {
            if (created)
            {
                DeviceDisplay.MainDisplayInfoChanged += this.CurrentOnOrientationChanged;
            }
            else
            {
                DeviceDisplay.MainDisplayInfoChanged -= this.CurrentOnOrientationChanged;
            }
        }

        /// <summary>
        /// Requests to set focus on this text field.
        /// </summary>
        public new void Focus()
        {
            this.IsFocusedInternal = true;

            if (this.RespondsToInputCycle(this.InputType))
            {
                this.InputControl.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(this.Text))
                {
                    this.AnimateToInactiveOrFocusedState();
                }
                else
                {
                    this.AnimateToActivatedState();
                }
            }
        }

        /// <summary>
        /// Requests to unset the focus on this text field.
        /// </summary>
        public new void Unfocus()
        {
            this.IsFocusedInternal = false;

            if (this.RespondsToInputCycle(this.InputType))
            {
                this.InputControl.Unfocus();
            }
            else
            {
                if (string.IsNullOrEmpty(this.Text))
                {
                    this.AnimateToInactiveOrFocusedState();
                }
                else
                {
                    this.AnimateToActivatedState();
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.AnimateToInactiveOrFocusedStateOnStart(this.BindingContext);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            this.AnimateToInactiveOrFocusedStateOnStart(this.Parent);
        }

        /// <inheritdoc />
        /// <summary>
        /// Method that is called when a bound property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == null) return;

            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(propertyName, out var handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        protected void AnimateToActivatedState()
        {
            var anim = new Animation();
            var hasText = !string.IsNullOrEmpty(this.Text);

            if (IsFocusedInternal)
            {
                var tintColor = this.HasError ? this.ErrorColor : this.TintColor;

                if (this.ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, 1, 2, _animationCurve, () =>
                    {
                        underline.Color = tintColor;
                    }));
                }

                placeholder.TextColor = tintColor;
            }
            else
            {
                var underlineColor = this.HasError ? this.ErrorColor : this.UnderlineColor;
                var placeholderColor = this.HasError ? this.ErrorColor : this.FloatingPlaceholderColor;

                var endHeight = hasText ? 1 : 0;

                if (this.ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, underline.HeightRequest, endHeight, _animationCurve, () =>
                    {
                        underline.Color = underlineColor;
                    }));
                }

                placeholder.TextColor = placeholderColor;
            }

            anim.Commit(this, "UnfocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: _animationCurve);
        }

        protected void AnimateToInactiveOrFocusedState()
        {
            Color tintColor;
            double preferredStartFont = Math.Abs(this.FloatingPlaceholderFontSize) < double.Epsilon ? InputControl.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double preferredEndFont = Math.Abs(this.FloatingPlaceholderFontSize) < double.Epsilon ? InputControl.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double startFont = IsFocusedInternal ? InputControl.FontSize : preferredStartFont;
            double endFOnt = IsFocusedInternal ? preferredEndFont : InputControl.FontSize;
            var startY = placeholder.TranslationY;
            double endY = IsFocusedInternal ? -(InputControl.FontSize * 0.8) : 0;

            if (this.HasError)
            {
                tintColor = IsFocusedInternal ? this.ErrorColor : this.PlaceholderColor;
            }
            else
            {
                tintColor = this.IsFocusedInternal ? this.TintColor : this.PlaceholderColor;
            }

            var anim = this.FloatingPlaceholderEnabled ? new Animation
            {
                {
                    0.0,
                    AnimationDuration,
                    new Animation(v => placeholder.FontSize = v, startFont, endFOnt, _animationCurve)
                },
                {
                    0.0,
                    AnimationDuration,
                    new Animation(v => placeholder.TranslationY = v, startY, endY, _animationCurve, () =>
                    {
                        if(this.HasError && InputControl.IsFocused)
                        {
                            placeholder.TextColor = this.ErrorColor;
                        }

                        placeholder.TextColor = tintColor;
                    })
                }
            } : new Animation();

            if (this.IsFocusedInternal)
            {
                if (this.ShouldAnimateUnderline)
                {
                    underline.Color = this.HasError ? this.ErrorColor : this.TintColor;

                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, this.Width, _animationCurve, () =>
                    {
                        underline.WidthRequest = -1;
                        underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                    }));
                }
            }
            else
            {
                if (this.ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, underline.HeightRequest, 0, _animationCurve, () =>
                    {
                        underline.WidthRequest = 0;
                        underline.HeightRequest = 2;
                        underline.HorizontalOptions = LayoutOptions.Center;
                    }));
                }
            }

            anim.Commit(this, "FocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: _animationCurve);
        }

        private void AnimateToInactiveOrFocusedStateOnStart(object startObject)
        {
            var placeholderEndY = -(InputControl.FontSize * 0.8);
            var placeholderEndFont = InputControl.FontSize * 0.75;

            if (!this.FloatingPlaceholderEnabled && string.IsNullOrEmpty(InputControl.Text))
            {
                placeholder.TextColor = this.PlaceholderColor;
            }

            if (startObject != null && !string.IsNullOrEmpty(this.Text) && !_wasFocused)
            {
                if (placeholder.TranslationY == placeholderEndY)
                {
                    return;
                }
                InputControl.Opacity = 0;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, InputControl.FontSize, placeholderEndFont, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, placeholderEndY, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.HasError ? this.ErrorColor : this.FloatingPlaceholderColor;
                            InputControl.Opacity = 1;
                        }));
                    }

                    if (this.ShouldAnimateUnderline)
                    {
                        underline.Color = this.HasError ? this.ErrorColor : this.TintColor;
                        underline.HeightRequest = 1;
                        anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, this.Width, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });

                InputControl.Opacity = 1;

                return;
            }

            if (startObject != null && string.IsNullOrEmpty(this.Text) && placeholder.TranslationY == placeholderEndY)
            {
                if (this.IsFocusedInternal)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, placeholderEndFont, InputControl.FontSize, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, 0, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.PlaceholderColor;
                            InputControl.Opacity = 1;
                        }));
                    }

                    if (this.ShouldAnimateUnderline)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, this.Width, 0, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.Center));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });
            }
        }

        private void ChangeToErrorState()
        {
            const int animDuration = 250;
            placeholder.TextColor = (this.FloatingPlaceholderEnabled && this.IsFocusedInternal) || (this.FloatingPlaceholderEnabled && !string.IsNullOrEmpty(this.Text)) ? this.ErrorColor : this.PlaceholderColor;
            counter.TextColor = this.ErrorColor;
            underline.Color = this.ShouldAnimateUnderline ? this.ErrorColor : Color.Transparent;
            persistentUnderline.Color = this.AlwaysShowUnderline ? this.ErrorColor : Color.Transparent;
            trailingIcon.IsVisible = true;
            trailingIcon.Source = "xf_error";
            trailingIcon.TintColor = this.ErrorColor;

            if (string.IsNullOrEmpty(this.ErrorText))
            {
                helper.TextColor = this.ErrorColor;
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await helper.FadeTo(0, animDuration / 2, _animationCurve);
                    helper.TranslationY = -4;
                    helper.TextColor = this.ErrorColor;
                    helper.Text = this.ErrorText;
                    await Task.WhenAll(helper.FadeTo(1, animDuration / 2, _animationCurve), helper.TranslateTo(0, 0, animDuration / 2, _animationCurve));
                });
            }
        }

        private void ChangeToNormalState()
        {
            const double opactiy = 1;
            this.IsEnabled = true;
            InputControl.Opacity = opactiy;
            placeholder.Opacity = opactiy;
            helper.Opacity = opactiy;
            underline.Opacity = opactiy;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (IsChoiceInput(this.InputType))
                {
                    trailingIcon.Source = "xf_arrow_dropdown";
                    trailingIcon.TintColor = this.TextColor;
                }
                else
                {
                    trailingIcon.Source = this.TrailingIcon;
                    trailingIcon.IsVisible = this.ShowsTrailingIcon(this.InputType);
                }

                var accentColor = this.TintColor;
                placeholder.TextColor = accentColor;
                counter.TextColor = this.HelperTextColor;
                underline.Color = accentColor;
                persistentUnderline.Color = this.UnderlineColor;

                if (string.IsNullOrEmpty(this.ErrorText))
                {
                    helper.TextColor = this.HelperTextColor;
                }
                else
                {
                    await helper.FadeTo(0, 150, _animationCurve);
                    helper.TranslationY = -4;
                    helper.TextColor = this.HelperTextColor;
                    helper.Text = this.HelperText;
                    await Task.WhenAll(helper.FadeTo(1, 150, _animationCurve), helper.TranslateTo(0, 0, 150, _animationCurve));
                }
            });
        }

        protected void CurrentOnOrientationChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation != this.DisplayOrientation)
            {
                if (!string.IsNullOrEmpty(InputControl.Text) && this.ShouldAnimateUnderline)
                {
                    underline.WidthRequest = -1;
                    underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                }

                this.DisplayOrientation = e.DisplayInfo.Orientation;
            }
        }

        protected void Entry_Completed(object sender, EventArgs e) => this.Completed?.Invoke(this, EventArgs.Empty);

        protected void Entry_Focused(object sender, FocusEventArgs e)
        {
            this.IsFocusedInternal = true;
            _wasFocused = true;
            this.FocusCommand?.Execute(this.IsFocusedInternal);
            this.Focused?.Invoke(this, e);
            this.UpdateCounter();
        }

        protected virtual void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.IsFocused) when string.IsNullOrEmpty(this.InputControl.Text):
                    this.IsFocusedInternal = InputControl.IsFocused;
                    this.AnimateToInactiveOrFocusedState();
                    break;

                case nameof(this.IsFocused) when !string.IsNullOrEmpty(this.InputControl.Text):
                    this.IsFocusedInternal = InputControl.IsFocused;
                    this.AnimateToActivatedState();
                    break;

                case nameof(this.Text):
                    this.Text = this.InputControl.Text;
                    this.UpdateCounter();
                    break;
            }
        }

        protected void Entry_SizeChanged(object sender, EventArgs e)
        {
            var baseHeight = this.FloatingPlaceholderEnabled ? 56 : 40;
            if (_cachedInputHeight == null)
            {
                this._cachedInputHeight = this.InputControl.Height;
            }

            if (this.InputControl.AutoSize == EditorAutoSizeOption.Disabled)
            {
                var diff = this._cachedInputHeight.Value - 20;
                var rawRowHeight = baseHeight + diff;
                if (rawRowHeight >= 0)
                    _autoSizingRow.Height = new GridLength(rawRowHeight);
            }
            else
            {
                this._cachedInputHeight = this.InputControl.Height;
            }

            double iconVerticalMargin = (_autoSizingRow.Height.Value - 24) / 2;

            if (leadingIcon.IsVisible)
            {
                leadingIcon.Margin = new Thickness(this.HorizontalPadding.Left, iconVerticalMargin, 0, iconVerticalMargin);
                InputControl.Margin = new Thickness(12, InputControl.Margin.Top, this.HorizontalPadding.Right, InputControl.Margin.Bottom);
            }
            else
            {
                InputControl.Margin = new Thickness(this.HorizontalPadding.Left, InputControl.Margin.Top, this.HorizontalPadding.Right, InputControl.Margin.Bottom);
            }

            if (trailingIcon.IsVisible)
            {
                var entryPaddingLeft = leadingIcon.IsVisible ? 12 : this.HorizontalPadding;
                trailingIcon.Margin = new Thickness(12, iconVerticalMargin, this.HorizontalPadding.Right, iconVerticalMargin);
                InputControl.Margin = new Thickness(entryPaddingLeft.Left, InputControl.Margin.Top, 0, InputControl.Margin.Bottom);
            }

            helper.Margin = new Thickness(this.HorizontalPadding.Left, helper.Margin.Top, 12, 0);
            counter.Margin = new Thickness(0, counter.Margin.Top, this.HorizontalPadding.Right, 0);

            var placeholderLeftMargin = this.FloatingPlaceholderEnabled ? this.HorizontalPadding.Left : InputControl.Margin.Left;

            placeholder.Margin = new Thickness(placeholderLeftMargin, placeholder.Margin.Top, 0, 0);

            if (this.HasError)
            {
                underline.Color = this.ErrorColor;
            }
        }

        protected virtual void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TextChangeCommand?.Execute(InputControl.Text);
            this.TextChanged?.Invoke(this, e);
        }

        protected virtual void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            this.IsFocusedInternal = false;
            this.FocusCommand?.Execute(this.IsFocusedInternal);
            this.Unfocused?.Invoke(this, e);
            this.UpdateCounter();
        }

        private IList<string> GetChoices()
        {
            var choiceStrings = new List<string>(this.Choices.Count);

            for (int i = 0; i < this.Choices.Count; i++)
            {
                this.GetChoiceString(i);
            }

            return choiceStrings;
        }

        private string GetChoiceString(int index)
        {
            if (index < 0)
            {
                return "";
            }
            else
            {
                //Ok
            }

            var choice = this.Choices[index];

            var listType = this.Choices[0].GetType();

            if (!string.IsNullOrEmpty(this.ChoicesBindingName))
            {
                var propInfo = listType.GetProperty(this.ChoicesBindingName);

                if (propInfo == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Property {this.ChoicesBindingName} was not found for item in {this.Choices}.");
                    return choice.ToString();
                }
                else
                {
                    var propValue = propInfo.GetValue(choice);
                    return propValue.ToString();
                }
            }
            else
            {
                return choice.ToString();
            }
        }

        private object GetSelectedChoice(int index)
        {
            if (index < 0)
            {
                return null;
            }

            return this.Choices[index];
        }

        private IList GetSelectedChoices(List<int> indicies)
        {
            if (indicies.Count() < 0)
            {
                return null;
            }

            return this.Choices.Subset(indicies.ToArray());
        }

        protected void OnAlwaysShowUnderlineChanged(bool isShown)
        {
            persistentUnderline.IsVisible = isShown;
            persistentUnderline.Color = this.UnderlineColor;
        }

        protected void OnBackgroundColorChanged(Color backgroundColor)
        {
            backgroundCard.BackgroundColor = backgroundColor;
        }

        protected void OnChoicesChanged(ICollection choices)
        {
        }

        protected void OnEnabledChanged(bool isEnabled)
        {
            this.Opacity = isEnabled ? 1 : 0.33;
            helper.IsVisible = isEnabled && !string.IsNullOrEmpty(this.HelperText);
        }

        protected void OnErrorColorChanged(Color errorColor)
        {
            trailingIcon.TintColor = errorColor;
        }

        protected void OnErrorTextChanged()
        {
            if (this.HasError)
            {
                this.ChangeToErrorState();
            }
        }

        protected void OnFloatingPlaceholderEnabledChanged(bool isEnabled)
        {
            double marginTopVariation = Device.RuntimePlatform == Device.iOS ? 18 : 20;
            InputControl.Margin = isEnabled ? new Thickness(InputControl.Margin.Left, 24, InputControl.Margin.Right, 0) : new Thickness(InputControl.Margin.Left, marginTopVariation - 9, InputControl.Margin.Right, 0);

            var iconMargin = leadingIcon.Margin;
            leadingIcon.Margin = isEnabled ? new Thickness(iconMargin.Left, 16, iconMargin.Right, 16) : new Thickness(iconMargin.Left, 8, iconMargin.Right, 8);

            var trailingIconMargin = trailingIcon.Margin;
            trailingIcon.Margin = isEnabled ? new Thickness(trailingIconMargin.Left, 16, trailingIconMargin.Right, 16) : new Thickness(trailingIconMargin.Left, 8, trailingIconMargin.Right, 8);
        }

        protected void OnHasErrorChanged()
        {
            if (this.HasError)
            {
                this.ChangeToErrorState();
            }
            else
            {
                this.ChangeToNormalState();
            }
        }

        protected void OnHelperTextChanged(string helperText)
        {
            helper.Text = helperText;
            helper.IsVisible = !string.IsNullOrEmpty(helperText);
        }

        protected void OnHelperTextColorChanged(Color textColor)
        {
            helper.TextColor = counter.TextColor = textColor;
        }

        protected void OnHelpertTextFontFamilyChanged(string fontFamily)
        {
            helper.FontFamily = counter.FontFamily = fontFamily;
        }

        protected void OnInputTypeChanged(MaterialTextFieldInputType inputType)
        {
            switch (inputType)
            {
                case MaterialTextFieldInputType.Chat:
                    InputControl.Keyboard = Keyboard.Chat;
                    break;

                case MaterialTextFieldInputType.Default:
                    InputControl.Keyboard = Keyboard.Default;
                    break;

                case MaterialTextFieldInputType.Email:
                    InputControl.Keyboard = Keyboard.Email;
                    break;

                case MaterialTextFieldInputType.Numeric:
                    InputControl.Keyboard = Keyboard.Numeric;
                    break;

                case MaterialTextFieldInputType.Plain:
                    InputControl.Keyboard = Keyboard.Plain;
                    break;

                case MaterialTextFieldInputType.Telephone:
                    InputControl.Keyboard = Keyboard.Telephone;
                    break;

                case MaterialTextFieldInputType.Text:
                    InputControl.Keyboard = Keyboard.Text;
                    break;

                case MaterialTextFieldInputType.Url:
                    InputControl.Keyboard = Keyboard.Url;
                    break;

                case MaterialTextFieldInputType.NumericPassword:
                    InputControl.Keyboard = Keyboard.Numeric;
                    break;

                case MaterialTextFieldInputType.Password:
                    InputControl.Keyboard = Keyboard.Text;
                    break;

                case MaterialTextFieldInputType.Choice:
                case MaterialTextFieldInputType.MultiChoice:
                case MaterialTextFieldInputType.Date:
                case MaterialTextFieldInputType.Time:
                    break;
            }

            // Hint: Will use this for MaterialTextArea
            if (inputType == MaterialTextFieldInputType.MultiLineText)
            {
                this.IsMultiLine = true;
            }

            if(this.IsMultiLine)
            {
                this.InputControl.AutoSize = EditorAutoSizeOption.TextChanges;
                this._autoSizingRow.Height = GridLength.Auto;
            }
            else
            {
                this.InputControl.AutoSize = EditorAutoSizeOption.Disabled;
                this._autoSizingRow.Height = 56;
            }
            _gridContainer.InputTransparent = !this.RespondsToInputCycle(inputType);
            trailingIcon.IsVisible = this.ShowsTrailingIcon(inputType);

            InputControl.IsNumericKeyboard = inputType == MaterialTextFieldInputType.Telephone || inputType == MaterialTextFieldInputType.Numeric;
            InputControl.IsPassword = inputType == MaterialTextFieldInputType.Password || inputType == MaterialTextFieldInputType.NumericPassword;
        }

        private bool ShowsTrailingIcon(MaterialTextFieldInputType inputType)
        {
            return this.TrailingIcon != null || this.IsChoiceInput(inputType);
        }

        private bool RespondsToInputCycle(MaterialTextFieldInputType inputType)
        {
            return !this.IsPickerInput(inputType) && !this.IsChoiceInput(inputType);
        }

        public bool IsPickerInput(MaterialTextFieldInputType inputType)
        {
            return inputType == MaterialTextFieldInputType.Date || inputType == MaterialTextFieldInputType.Time;
        }

        public bool IsChoiceInput(MaterialTextFieldInputType inputType)
        {
            return inputType == MaterialTextFieldInputType.Choice || inputType == MaterialTextFieldInputType.MultiChoice;
        }

        protected void OnKeyboardFlagsChanged(bool isAutoCapitalizationEnabled, bool isSpellCheckEnabled, bool isTextPredictionEnabled)
        {
            var flags = KeyboardFlags.CapitalizeWord | KeyboardFlags.Spellcheck | KeyboardFlags.Suggestions;

            if (!isAutoCapitalizationEnabled)
            {
                flags &= ~KeyboardFlags.CapitalizeWord;
            }

            if (!isSpellCheckEnabled)
            {
                flags &= ~KeyboardFlags.Spellcheck;
            }

            if (!isTextPredictionEnabled)
            {
                flags &= ~KeyboardFlags.Suggestions;
            }

            InputControl.Keyboard = Keyboard.Create(flags);
        }

        protected void OnLeadingIconChanged(string icon)
        {
            leadingIcon.Source = icon;
            this.OnLeadingIconTintColorChanged(this.LeadingIconTintColor);
        }

        protected void OnLeadingIconTintColorChanged(Color tintColor)
        {
            leadingIcon.TintColor = tintColor;
        }


        protected void OnTrailingIconChanged(string icon)
        {
            this.trailingIcon.Source = icon;
            this.OnTrailingIconTintColorChanged(this.TrailingIconTintColor);
        }

        protected void OnTrailingIconTintColorChanged(Color tintColor)
        {
            this.trailingIcon.TintColor = tintColor;
            this.trailingIcon.IsVisible = this.ShowsTrailingIcon(this.InputType);
        }

        protected void OnMaxLengthChanged(int maxLength, bool isMaxLengthCounterVisible)
        {
            _counterEnabled = maxLength > 0 && isMaxLengthCounterVisible;
            InputControl.MaxLength = maxLength > 0 ? maxLength : (int)InputView.MaxLengthProperty.DefaultValue;
        }

        protected void OnPlaceholderChanged(string placeholderText)
        {
            placeholder.Text = placeholderText;
        }

        protected void OnPlaceholderColorChanged(Color placeholderColor)
        {
            placeholder.TextColor = placeholderColor;
        }

        protected void OnPlaceholderFontFamilyChanged(string fontFamily)
        {
            placeholder.FontFamily = fontFamily;
        }

        protected void OnReturnCommandChanged(ICommand returnCommand)
        {
            InputControl.ReturnCommand = returnCommand;
        }

        protected void OnReturnCommandParameterChanged(object parameter)
        {
            InputControl.ReturnCommandParameter = parameter;
        }

        protected void OnReturnTypeChangedd(ReturnType returnType)
        {
            InputControl.ReturnType = returnType;
        }

        protected virtual async Task OnPartcipatingInNonUserInteractiveInput()
        {
            if (this.IsChoiceInput(this.InputType))
            {
                await this.OnSelectChoices();
            }
        }

        private async Task OnSelectChoices()
        {
            if (this.Choices == null || this.Choices?.Count <= 0)
            {
                throw new InvalidOperationException("The property `Choices` is null or empty");
            }

            string title = MaterialConfirmationDialog.GetDialogTitle(this);
            string confirmingText = MaterialConfirmationDialog.GetDialogConfirmingText(this);
            string dismissiveText = MaterialConfirmationDialog.GetDialogDismissiveText(this);
            Dialogs.Configurations.MaterialConfirmationDialogConfiguration configuration = MaterialConfirmationDialog.GetDialogConfiguration(this);

            List<int> result = new List<int>();

            if (this.InputType == MaterialTextFieldInputType.Choice)
            {
                if (_selectedIndicies.Count > 0)
                {
                    int choiceIndicies = await MaterialDialog.Instance.SelectChoiceAsync(title, this.Choices, _selectedIndicies[0], this.ChoicesBindingName, confirmingText, dismissiveText, configuration);
                    result.Add(choiceIndicies);
                }
                else
                {
                    int choiceIndicies = await MaterialDialog.Instance.SelectChoiceAsync(title, this.Choices, this.ChoicesBindingName, confirmingText, dismissiveText, configuration);

                    result.Add(choiceIndicies);
                }

                if (result.Count > 0)
                {
                    _selectedIndicies = result;
                    this.Text = this.GetChoiceString(_selectedIndicies[0]);
                }
            }
            else //MultiChoice
            {
                if (_selectedIndicies.Count > 0)
                {
                    IEnumerable<int> choiceIndicies = await MaterialDialog.Instance.SelectChoicesAsync(title, this.Choices, _selectedIndicies, this.ChoicesBindingName, confirmingText, dismissiveText, configuration);
                    if (choiceIndicies != null)
                    {
                        result = choiceIndicies.ToList();
                    }
                    else
                    {
                        //retain empty list from above
                    }
                }
                else
                {
                    IEnumerable<int> choiceIndicies = await MaterialDialog.Instance.SelectChoicesAsync(title, this.Choices, this.ChoicesBindingName, confirmingText, dismissiveText, configuration);


                    if (choiceIndicies != null)
                    {
                        result = choiceIndicies.ToList();
                    }
                    else
                    {
                        //retain empty list from above
                    }


                }

                if (result.Count > 0)
                {
                    _selectedIndicies = result;
                    var selectedChoices = this.GetSelectedChoices(_selectedIndicies);
                    this.ChoiceSelected?.Invoke(this, new SelectedItemChangedEventArgs(selectedChoices));
                    this.ChoiceSelectedCommand?.Execute(selectedChoices);
                }
                else
                {
                    _selectedIndicies.Clear();
                }

            }

            if (result.Count > 0)
            {
                this.Text = result.Count > 1 ? "Multiple" : this.GetChoiceString(_selectedIndicies[0]);
            }
        }

        protected void OnTextChanged(string text)
        {
            if (!string.IsNullOrEmpty(text) && !this.FloatingPlaceholderEnabled)
            {
                placeholder.IsVisible = false;
            }
            else if (string.IsNullOrEmpty(text) && !this.FloatingPlaceholderEnabled)
            {
                placeholder.IsVisible = true;
            }

            //if (this.InputType == BaseMaterialInputViewInputType.Choice && !string.IsNullOrEmpty(text) && _choices?.Contains(text) == false)
            //{
            //    Debug.WriteLine($"The `Text` property value `{this.Text}` does not match any item in the collection `Choices`.");
            //    this.Text = null;
            //    return;
            //}

            if (this.InputType == MaterialTextFieldInputType.Choice && !string.IsNullOrEmpty(text))
            {
                if (_selectedIndicies.Count > 0)
                {
                    var selectedChoice = this.GetSelectedChoice(_selectedIndicies[0]);
                    this.ChoiceSelected?.Invoke(this, new SelectedItemChangedEventArgs(selectedChoice));
                    this.ChoiceSelectedCommand?.Execute(selectedChoice);
                }
            }
            else if (this.InputType == MaterialTextFieldInputType.Choice && string.IsNullOrEmpty(text))
            {
                _selectedIndicies.Clear();
            }

            InputControl.Text = text;

            this.AnimateToInactiveOrFocusedStateOnStart(this);
            this.UpdateCounter();
        }

        protected void OnTextColorChanged(Color textColor)
        {
            InputControl.TextColor = textColor;
        }

        protected void OnTextFontFamilyChanged(string fontFamily)
        {
            InputControl.FontFamily = fontFamily;
        }

        protected void OnTextFontSizeChanged(double fontSize)
        {
            placeholder.FontSize = InputControl.FontSize = fontSize;
        }

        protected void OnTintColorChanged(Color tintColor)
        {
            InputControl.TintColor = tintColor;
        }

        protected void OnUnderlineColorChanged(Color underlineColor)
        {
            if (this.AlwaysShowUnderline)
            {
                persistentUnderline.Color = underlineColor;
            }
        }

        private void SetControl()
        {
            if (this.RespondsToInputCycle(this.InputType))
            {
                this.OnInputTypeChanged(this.InputType);
            }
            trailingIcon.TintColor = this.TrailingIconTintColor;
            persistentUnderline.Color = this.UnderlineColor;
            tapGesture.Command = new Command(() =>
            {
                if (this.RespondsToInputCycle(this.InputType))
                {
                    if (!this.IsFocusedInternal)
                    {
                        InputControl.Focus();
                    }
                }
            });

            mainTapGesture.Command = new Command(async () => await this.OnPartcipatingInNonUserInteractiveInput());
        }

        protected virtual void SetPropertyChangeHandler(ref Dictionary<string, Action> propertyChangeActions)
        {
            propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(this.Text), () => this.OnTextChanged(this.Text) },
                { nameof(this.TextColor), () => this.OnTextColorChanged(this.TextColor) },
                { nameof(this.TextFontFamily), () => this.OnTextFontFamilyChanged(this.TextFontFamily) },
                { nameof(this.TintColor), () => this.OnTintColorChanged(this.TintColor) },
                { nameof(this.Placeholder), () => this.OnPlaceholderChanged(this.Placeholder) },
                { nameof(this.PlaceholderColor), () => this.OnPlaceholderColorChanged(this.PlaceholderColor) },
                { nameof(this.PlaceholderFontFamily), () => this.OnPlaceholderFontFamilyChanged(this.PlaceholderFontFamily) },
                { nameof(this.HelperText), () => this.OnHelperTextChanged(this.HelperText) },
                { nameof(this.HelperTextFontFamily), () => this.OnHelpertTextFontFamilyChanged(this.HelperTextFontFamily) },
                { nameof(this.HelperTextColor), () => this.OnHelperTextColorChanged(this.HelperTextColor) },
                { nameof(this.InputType), () => this.OnInputTypeChanged(this.InputType) },
                { nameof(this.IsEnabled), () => this.OnEnabledChanged(this.IsEnabled) },
                { nameof(this.BackgroundColor), () => this.OnBackgroundColorChanged(this.BackgroundColor) },
                { nameof(this.AlwaysShowUnderline), () => this.OnAlwaysShowUnderlineChanged(this.AlwaysShowUnderline) },
                { nameof(this.MaxLength), () => this.OnMaxLengthChanged(this.MaxLength, this.IsMaxLengthCounterVisible) },
                { nameof(this.ReturnCommand), () => this.OnReturnCommandChanged(this.ReturnCommand) },
                { nameof(this.ReturnCommandParameter), () => this.OnReturnCommandParameterChanged(this.ReturnCommandParameter) },
                { nameof(this.ReturnType), () => this.OnReturnTypeChangedd(this.ReturnType) },
                { nameof(this.ErrorColor), () => this.OnErrorColorChanged(this.ErrorColor) },
                { nameof(this.UnderlineColor), () => this.OnUnderlineColorChanged(this.UnderlineColor) },
                { nameof(this.HasError), () => this.OnHasErrorChanged() },
                { nameof(this.FloatingPlaceholderEnabled), () => this.OnFloatingPlaceholderEnabledChanged(this.FloatingPlaceholderEnabled) },
                { nameof(this.Choices), () => this.OnChoicesChanged(this.Choices) },
                { nameof(this.LeadingIcon), () => this.OnLeadingIconChanged(this.LeadingIcon) },
                { nameof(this.LeadingIconTintColor), () => this.OnLeadingIconTintColorChanged(this.LeadingIconTintColor) },
                { nameof(this.TrailingIcon), () => this.OnTrailingIconChanged(this.TrailingIcon) },
                { nameof(this.TrailingIconTintColor), () => this.OnTrailingIconTintColorChanged(this.TrailingIconTintColor) },
                { nameof(this.IsSpellCheckEnabled), () => this.OnKeyboardFlagsChanged(this.IsAutoCapitalizationEnabled, this.IsSpellCheckEnabled, this.IsTextPredictionEnabled) },
                { nameof(this.IsTextPredictionEnabled), () => this.OnKeyboardFlagsChanged(this.IsAutoCapitalizationEnabled, this.IsSpellCheckEnabled, this.IsTextPredictionEnabled) },
                { nameof(this.IsAutoCapitalizationEnabled), () => this.OnKeyboardFlagsChanged(this.IsAutoCapitalizationEnabled, this.IsSpellCheckEnabled, this.IsTextPredictionEnabled) },
                { nameof(this.TextFontSize), () => this.OnTextFontSizeChanged(this.TextFontSize) },
                { nameof(this.ErrorText), () => this.OnErrorTextChanged() }
            };
        }

        private void UpdateCounter()
        {
            if (!_counterEnabled) return;
            var count = InputControl.Text?.Length ?? 0;
            counter.Text = this.IsFocusedInternal ? $"{count}/{this.MaxLength}" : string.Empty;
        }
    }
}