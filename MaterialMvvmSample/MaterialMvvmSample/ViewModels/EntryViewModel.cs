using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialMvvmSample.Views;
using Xamarin.Forms;

namespace MaterialMvvmSample.ViewModels
{
    public class EntryViewModel : BaseViewModel
    {
        private ICommand _jobsCommand;
        private ICommand _calendarCommand;
        private ICommand _datePickerCommand;

        public ICommand JobsCommand
        {
            get => _jobsCommand;
            set
            {
                _jobsCommand = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand CalendarCommand
        {
            get => _calendarCommand;
            set
            {
                _calendarCommand = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand DatePickerCommand
        {
            get => _datePickerCommand;
            set
            {
                _datePickerCommand = value;
                this.OnPropertyChanged();
            }
        }

        public EntryViewModel()
        {
            this.JobsCommand = new Command(async () => await this.OnJobsCommand());
            this.CalendarCommand = new Command(async () => await this.OnCalendarCommand());
            this.DatePickerCommand = new Command(async () => await this.OnDatePickerCommand());
        }

        private async Task OnDatePickerCommand()
        {
            await XF.Material.Forms.UI.Dialogs.MaterialDatePicker.Show();
        }

        private async Task OnCalendarCommand()
        {
            await this.Navigation.PushAsync(ViewNames.CalendarView);
        }

        private async Task OnJobsCommand()
        {
            await this.Navigation.PushAsync(ViewNames.MainView);
        }
    }
}
