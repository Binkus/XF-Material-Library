using System;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    public class MaterialTimePicker : Xamarin.Forms.TimePicker
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialTimePicker), Material.Color.Secondary);

        public static readonly BindableProperty NullableTimeProperty = BindableProperty.Create(nameof(NullableTime), typeof(TimeSpan?), typeof(MaterialTimePicker), null);

        internal MaterialTimePicker()
        {
        }

        public Color TintColor
        {
            get
            {
                return (Color)GetValue(TintColorProperty);
            }

            set
            {
                SetValue(TintColorProperty, value);
            }
        }

        private Color? _color = null;

        public string Text
        {
            get
            {
                if (NullableTime.HasValue)
                    return NullableTime.Value.ToString();
                else
                    return "";
            }
            set
            {
                if (DateTime.TryParse(value, out DateTime dateTime))
                {
                    NullableTime = dateTime.TimeOfDay;
                }
            }
        }

        public TimeSpan? NullableTime
        {
            get { return (TimeSpan?)GetValue(NullableTimeProperty); }

            set
            {
                if (NullableTime != value)
                {
                    SetValue(NullableTimeProperty, value);
                    UpdateTime();
                }
            }
        }

        private void UpdateTime()
        {
            if (NullableTime.HasValue)
            {
                if (_color.HasValue)
                {
                    TextColor = _color.Value;
                    _color = null;
                }

                Time = NullableTime.Value;
            }
            else
            {
                _color = TextColor;
                TextColor = Color.Transparent;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateTime();
        }

        public new EventHandler<NullableTimeChangedEventArgs> TimeSelected;

        public EventHandler<TextChangedEventArgs> TextChanged;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsFocused) && !IsFocused)
            {
                var old = NullableTime;
                NullableTime = Time;
                TimeSelected?.Invoke(this, new NullableTimeChangedEventArgs(old, NullableTime));
                TextChanged?.Invoke(this, new TextChangedEventArgs(old?.ToString(), Text));
            }
        }
    }

    public class NullableTimeChangedEventArgs : EventArgs
    {
        public NullableTimeChangedEventArgs(TimeSpan? oldTime, TimeSpan? newTime)
        {
            OldTime = oldTime;
            NewTime = newTime;
        }

        public TimeSpan? OldTime { get; set; }

        public TimeSpan? NewTime { get; set; }
    }
}