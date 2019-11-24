using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers.Internals;
using XF.Material.Forms.UI.Internals;
using System.ComponentModel;
using System.Drawing;

[assembly: ExportRenderer(typeof(MaterialTimePicker), typeof(MaterialTimePickerRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialTimePickerRenderer : TimePickerRenderer
    {
        private bool _returnButtonAdded;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                this.SetControl();
            }
        }

        private void SetControl()
        {
            if (this.Control == null)
            {
                return;
            }

            var heightConstraint = NSLayoutConstraint.Create(this.Control, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 20f);
            this.Control.AddConstraint(heightConstraint);
            this.Control.BackgroundColor = UIColor.Clear;
            this.Control.TintColor = (this.Element as MaterialTimePicker)?.TintColor.ToUIColor();
            this.Control.BorderStyle = UITextBorderStyle.None;
            this.Control.TranslatesAutoresizingMaskIntoConstraints = false;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            if (e?.PropertyName == nameof(MaterialEntry.TintColor))
            {
                this.Control.TintColor = (this.Element as MaterialTimePicker)?.TintColor.ToUIColor();
            }
        }
    }
}