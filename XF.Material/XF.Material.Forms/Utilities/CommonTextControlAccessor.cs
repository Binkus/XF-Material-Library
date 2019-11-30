using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.Utilities
{
    public class CommonTextControlAccessor
    {
        public View VisualElement { get; }

        public CommonTextControlAccessor(View visualElement)
        {
            VisualElement = visualElement;
        }

        public bool Focus() => this.VisualElement.Focus();

        public void Unfocus() => this.VisualElement.Unfocus();

        public bool IsFocused => this.VisualElement.IsFocused;

        public bool IsVisible => this.VisualElement.IsVisible;

        public double Height => this.VisualElement.Height;

        public double Width => this.VisualElement.Width;

        public Thickness Margin
        {
            get
            {
                return (this.VisualElement as View)?.Margin ?? new Thickness();
            }
            set
            {
                if(this.VisualElement is View view)
                {
                    view.Margin = value;
                }
            }
        }

        public bool IsNumericKeyboard
        {
            get
            {
                return (this.VisualElement as UI.Internals.MaterialEntry)?.IsNumericKeyboard ?? false;
            }
            set
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    entry.IsNumericKeyboard = value;
                }
            }
        }

        public bool IsPassword
        {
            get
            {
                return (this.VisualElement as UI.Internals.MaterialEntry)?.IsPassword ?? false;
            }
            set
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    entry.IsPassword = value;
                }
            }
        }

        public Color TintColor
        {
            get
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    return entry.TintColor;
                }
                else if (this.VisualElement is UI.Internals.MaterialEditor editor)
                {
                    return editor.TintColor;
                }
                return Color.Black;
            }
            set
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    entry.TintColor = value;
                }
                else if (this.VisualElement is UI.Internals.MaterialEditor editor)
                {
                    editor.TintColor = value;
                }
            }
        }

        public int MaxLength
        {
            get
            {
                if (this.VisualElement is InputView entry)
                {
                    return entry.MaxLength;
                }

                return 0;
            }
            set
            {
                if (this.VisualElement is InputView entry)
                {
                    entry.MaxLength = value;
                }
            }
        }

        public EditorAutoSizeOption AutoSize
        {
            get
            {
                return (this.VisualElement as Editor)?.AutoSize ?? EditorAutoSizeOption.Disabled;
            }
            set
            {
                if (this.VisualElement is Editor editor)
                {
                    editor.AutoSize = value;
                }
            }
        }

        public double FontSize
        {
            get
            {
                if (this.VisualElement is Xamarin.Forms.Internals.IFontElement fontElement)
                {
                    return fontElement.FontSize;
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (this.VisualElement is Entry entry)
                {
                    entry.FontSize = value;
                }
                else if (this.VisualElement is Editor editor)
                {
                    editor.FontSize = value;
                }
                else if (this.VisualElement is DatePicker datePicker)
                {
                    datePicker.FontSize = value;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    timePicker.FontSize = value;
                }
                else //Fallback
                {
                    this.TrySet(this.VisualElement, nameof(Xamarin.Forms.Internals.IFontElement.FontSize), value);
                }
            }
        }

        public Keyboard Keyboard
        {
            get
            {
                if (this.VisualElement is InputView inputView)
                {
                    return inputView.Keyboard;
                }
                else
                {
                    return Keyboard.Default;
                }
            }

            set
            {
                if (this.VisualElement is InputView inputView)
                {
                    inputView.Keyboard = value;
                }
            }
        }

        public string Text
        {
            get
            {
                if (this.VisualElement is Entry entry)
                {
                    return entry.Text;
                }
                else if (this.VisualElement is Editor editor)
                {
                    return editor.Text;
                }
                else if (this.VisualElement is DatePicker datePicker)
                {
                    return datePicker.Date.ToShortDateString();
                }
                else if(this.VisualElement is MaterialTimePicker matTimePicker)
                {
                    return matTimePicker.Text;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    return timePicker.Time.ToString();
                }
                else //Fallback
                {
                    if (this.TryGet(this.VisualElement, "Text", out object value) && value is string text)
                    {
                        return text;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            set
            {
                if (this.VisualElement is Entry entry)
                {
                    entry.Text = value;
                }
                else if (this.VisualElement is Editor editor)
                {
                    editor.Text = value;
                }
                else if (this.VisualElement is DatePicker datePicker)
                {
                    if (DateTime.TryParse(value, out DateTime date))
                    {
                        datePicker.Date = date;
                    }
                }
                else if (this.VisualElement is MaterialTimePicker timePicker)
                {
                    if (DateTime.TryParse(value, out DateTime time))
                    {
                        timePicker.Time = time.TimeOfDay;
                        timePicker.Text = value;
                    }
                }
                else //Fallback
                {
                    this.TrySet(this.VisualElement, "Text", value);
                }
            }
        }

        public TimeSpan Time
        {
            get
            {
                if (this.VisualElement is DatePicker datePicker)
                {
                    return datePicker.Date.TimeOfDay;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    return timePicker.Time;
                }
                else //Fallback
                {
                    if (DateTime.TryParse(this.Text, out DateTime time))
                    {
                        return time.TimeOfDay;
                    }
                    else
                    {
                        return TimeSpan.MinValue;
                    }
                }
            }

            set
            {
                if (this.VisualElement is TimePicker timePicker)
                {
                    timePicker.Time = value;
                }
                else
                {
                    this.Text = value.ToString();
                }
            }
        }

        public DateTime Date
        {
            get
            {
                if (this.VisualElement is DatePicker datePicker)
                {
                    return datePicker.Date;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    return DateTime.MinValue.Add(timePicker.Time);
                }
                else //Fallback
                {
                    if (DateTime.TryParse(this.Text, out DateTime date))
                    {
                        return date;
                    }
                    else
                    {
                        return DateTime.MinValue;
                    }
                }
            }

            set
            {
                if (this.VisualElement is DatePicker datePicker)
                {
                    datePicker.Date = value;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    timePicker.Time = value.TimeOfDay;
                }
                else
                {
                    this.Text = value.ToString();
                }
            }
        }

        public double Opacity
        {
            get
            {
                return this.VisualElement.Opacity;
            }
            set
            {
                this.VisualElement.Opacity = value;
            }
        }

        public Color TextColor
        {
            get
            {
                if (this.VisualElement is Entry entry)
                {
                    return entry.TextColor;
                }
                else if (this.VisualElement is Editor editor)
                {
                    return editor.TextColor;
                }
                else if (this.VisualElement is DatePicker datePicker)
                {
                    return datePicker.TextColor;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    return timePicker.TextColor;
                }
                else //Fallback
                {
                    if (this.TryGet(this.VisualElement, "TextColor", out object value) && value is Color color)
                    {
                        return color;
                    }
                    else
                    {
                        return Color.Black;
                    }
                }
            }

            set
            {
                if (this.VisualElement is Entry entry)
                {
                    entry.TextColor = value;
                }
                else if (this.VisualElement is Editor editor)
                {
                    editor.TextColor = value;
                }
                else if (this.VisualElement is DatePicker datePicker)
                {
                    datePicker.TextColor = value;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    timePicker.TextColor = value;
                }
                else //Fallback
                {
                    this.TrySet(this.VisualElement, "TextColor", value);
                }
            }
        }

        public string FontFamily
        {
            get
            {
                if (this.VisualElement is Entry entry)
                {
                    return entry.FontFamily;
                }
                else if (this.VisualElement is Editor editor)
                {
                    return editor.FontFamily;
                }
                else if (this.VisualElement is DatePicker datePicker)
                {
                    return datePicker.FontFamily;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    return timePicker.FontFamily;
                }
                else //Fallback
                {
                    if (this.TryGet(this.VisualElement, "FontFamily", out object value) && value is string fontFamily)
                    {
                        return fontFamily;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            set
            {
                if (this.VisualElement is Entry entry)
                {
                    entry.FontFamily = value;
                }
                else if (this.VisualElement is Editor editor)
                {
                    editor.FontFamily = value;
                }
                else if (this.VisualElement is DatePicker datePicker)
                {
                    datePicker.FontFamily = value;
                }
                else if (this.VisualElement is TimePicker timePicker)
                {
                    timePicker.FontFamily = value;
                }
                else //Fallback
                {
                    this.TrySet(this.VisualElement, "FontFamily", value);
                }
            }
        }

        public ICommand ReturnCommand
        {
            get
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    return entry.ReturnCommand;
                }
                return null;
            }
            set
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    entry.ReturnCommand = value;
                }
            }
        }

        public object ReturnCommandParameter
        {
            get
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    return entry.ReturnCommandParameter;
                }
                return null;
            }
            set
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    entry.ReturnCommandParameter = value;
                }
            }
        }

        public ReturnType ReturnType
        {
            get
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    return entry.ReturnType;
                }
                return ReturnType.Default;
            }
            set
            {
                if (this.VisualElement is UI.Internals.MaterialEntry entry)
                {
                    entry.ReturnCommandParameter = value;
                }
            }
        }

        bool TrySet(object d, string propertyName, object value)
        {
            Type type = d.GetType();

            var prop = Array.Find(type.GetProperties(), p => p.Name.Equals(propertyName));

            if(prop != null && prop.CanWrite)
            {
                prop.SetValue(d, value);
                return true;
            }
            else
            {
                return false;
            }
        }

        bool TryGet(object d, string propertyName, out object value)
        {
            Type type = d.GetType();

            var prop = Array.Find(type.GetProperties(), p => p.Name.Equals(propertyName));

            if(prop != null && prop.CanRead)
            {
                value = prop.GetValue(d);
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
