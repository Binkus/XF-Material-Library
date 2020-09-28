using System;
using System.ComponentModel;
using Xamarin.Forms;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    public class MaterialTextArea : BaseMaterialInputView
    {
        public MaterialTextArea() : base(new MaterialEditor()
        {
            Margin = new Thickness(12, 24, 12, 0),
            FontFamily = Material.FontFamily.Body2,
            FontSize = 16,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            IsSpellCheckEnabled = false,
            IsTextPredictionEnabled = false,
            VerticalOptions = LayoutOptions.FillAndExpand
        })
        {
	        base.TextColor = Color.FromHex("#D0000000");
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ElementChanged(bool created)
        {
            if(InputControl.VisualElement is MaterialEditor entry)
            {
                if (created)
                {
                    entry.PropertyChanged += base.Entry_PropertyChanged;
                    entry.TextChanged += base.Entry_TextChanged;
                    entry.SizeChanged += base.Entry_SizeChanged;
                    entry.Focused += base.Entry_Focused;
                    entry.Unfocused += base.Entry_Unfocused;
                    entry.Completed += base.Entry_Completed;
                }
                else
                {
                    entry.PropertyChanged -= base.Entry_PropertyChanged;
                    entry.TextChanged -= base.Entry_TextChanged;
                    entry.SizeChanged -= base.Entry_SizeChanged;
                    entry.Focused -= base.Entry_Focused;
                    entry.Unfocused -= base.Entry_Unfocused;
                    entry.Completed -= base.Entry_Completed;
                }
                base.ElementChanged(created);
            }
        }
    }
}

