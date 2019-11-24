using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that let users enter and edit a date.
    /// </summary>
    public class MaterialDateField : BaseMaterialInputView
    {
        private DateTime? _date;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialDateField"/>.
        /// </summary>
        public MaterialDateField() : base(new MaterialEntry()
        {
            Margin = new Thickness(12, 24, 12, 0),
            FontFamily = Material.FontFamily.Body2,
            FontSize = 16,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            IsSpellCheckEnabled = false,
            IsTextPredictionEnabled = false,
            TextColor = Color.FromHex("#D0000000"),
            VerticalOptions = LayoutOptions.FillAndExpand,
        })
        {
            this.InputType = MaterialTextFieldInputType.Date;
            base.TrailingIcon = "xf_arrow_dropdown";
            //base.entry.SetBinding(MaterialTextField.TextProperty, "Date", BindingMode.OneWay);
        }

        private async Task MaterialDateField_Focused()
        {
            base.Focus();
            await this.PickDate();
            base.Unfocus();
        }

        public DateTime? Date
        {
            get => this._date;
            set
            {
                this._date = value;
                base.InputControl.Text = value != null ? value.Value.ToShortDateString() : string.Empty;
                base.OnPropertyChanged(nameof(this.Date));
            }
        }

        protected override async Task OnPartcipatingInNonUserInteractiveInput()
        {
            await base.OnPartcipatingInNonUserInteractiveInput();

            await this.MaterialDateField_Focused();
        }

        private async Task PickDate()
        {
            string title = MaterialConfirmationDialog.GetDialogTitle(this);
            string confirmingText = MaterialConfirmationDialog.GetDialogConfirmingText(this);
            string dismissiveText = MaterialConfirmationDialog.GetDialogDismissiveText(this);
            Dialogs.Configurations.MaterialConfirmationDialogConfiguration configuration = MaterialConfirmationDialog.GetDialogConfiguration(this);

            this.Date = await XF.Material.Forms.UI.Dialogs.MaterialDatePicker.Show(title, confirmingText, dismissiveText, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ElementChanged(bool created)
        {
            if (InputControl.VisualElement is MaterialEntry entry)
            {
                if (created)
                {

                    entry.PropertyChanged += this.Entry_PropertyChanged;
                    entry.TextChanged += this.Entry_TextChanged;
                    entry.SizeChanged += base.Entry_SizeChanged;
                    entry.Focused += base.Entry_Focused;
                    entry.Unfocused += this.Entry_Unfocused;
                    entry.Completed += base.Entry_Completed;
                }
                else
                {
                    entry.PropertyChanged -= this.Entry_PropertyChanged;
                    entry.TextChanged -= this.Entry_TextChanged;
                    entry.SizeChanged -= base.Entry_SizeChanged;
                    entry.Focused -= base.Entry_Focused;
                    entry.Unfocused -= this.Entry_Unfocused;
                    entry.Completed -= base.Entry_Completed;
                }
                base.ElementChanged(created);
            }
        }
    }
}