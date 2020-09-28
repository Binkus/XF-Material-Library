using System;
using System.Collections.Generic;
using System.Linq;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class CalendarView : BaseCalendarView
    {
        public CalendarView()
        {
            this.InitializeComponent();
        }
    }

    public abstract class BaseCalendarView : BaseView<CalendarViewModel> { }
}
