using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs.Internals;
using XF.Material.Forms.UI.Internals;


namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that let users enter and edit a date.
    /// </summary>
    public class MaterialTimeField : BaseMaterialInputView
    {
        public MaterialTimeField() : base(new MaterialTimePicker()
        {
            Margin = new Thickness(12, 24, 12, 0),
            FontFamily = Material.FontFamily.Body2,
            FontSize = 16,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.FromHex("#D0000000"),
            VerticalOptions = LayoutOptions.FillAndExpand
        })
        {
        }

        protected override async Task OnPartcipatingInNonUserInteractiveInput()
        {
            await base.OnPartcipatingInNonUserInteractiveInput();

            base.Focus();
            this.InputControl.Focus();
        }

        protected override void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            base.Unfocus();
            base.Entry_Unfocused(sender, e);
        }

        protected override void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Text = e.NewTextValue;
            base.Entry_TextChanged(sender, e);
        }

        protected new void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.IsFocused) when string.IsNullOrEmpty(this.InputControl.Text):
                    this.IsFocusedInternal = InputControl.IsFocused;
                    //this.AnimateToInactiveOrFocusedState();
                    break;

                case nameof(this.IsFocused) when !string.IsNullOrEmpty(this.InputControl.Text):
                    this.IsFocusedInternal = InputControl.IsFocused;
                    //this.AnimateToActivatedState();
                    break;

                //case nameof(this.Text):
                    //this.Text = this.InputControl.Text;
                    //this.UpdateCounter();
                    //break;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ElementChanged(bool created)
        {
            if (InputControl.VisualElement is MaterialTimePicker entry)
            {
                if (created)
                {

                    entry.PropertyChanged += this.Entry_PropertyChanged;
                    entry.TextChanged += this.Entry_TextChanged;
                    entry.SizeChanged += base.Entry_SizeChanged;
                    entry.Focused += base.Entry_Focused;
                    entry.Unfocused += this.Entry_Unfocused;
                    //entry.Completed += base.Entry_Completed;
                }
                else
                {
                    entry.PropertyChanged -= this.Entry_PropertyChanged;
                    entry.TextChanged -= this.Entry_TextChanged;
                    entry.SizeChanged -= base.Entry_SizeChanged;
                    entry.Focused -= base.Entry_Focused;
                    entry.Unfocused -= this.Entry_Unfocused;
                    //entry.Completed -= base.Entry_Completed;
                }
                base.ElementChanged(created);
            }
        }
    }
}

