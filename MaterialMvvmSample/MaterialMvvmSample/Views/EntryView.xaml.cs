using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class EntryView : BaseEntryView
    {
        public EntryView()
        {
            InitializeComponent();
        }
    }

    public abstract class BaseEntryView : BaseView<EntryViewModel> { }
}
