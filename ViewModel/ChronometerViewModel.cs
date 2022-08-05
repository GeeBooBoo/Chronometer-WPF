using System;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;

namespace Cronometer_WPF
{
    public class ChronometerViewModel : INotifyPropertyChanged, ICommand
    {

        #region Constructors

        DispatcherTimer dt = new DispatcherTimer();

        public ChronometerViewModel()
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += IncreaseTimer;
        }

        private void IncreaseTimer(object sender, EventArgs e)
        {
            if (dt.IsEnabled)
                Seconds++;

            if (Seconds > 60)
            {
                Minutes++;
                Seconds = 0;
            }
            if (Minutes > 60)
            {
                Hours++;
                Minutes = 0;
            }
        }

        #endregion

        #region Properties

        private int _seconds;
        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged(nameof(Seconds));
            }
        }
        private int _minutes;
        public int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged(nameof(Minutes));
            }
        }
        private int _hours;
        public int Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        }

        #endregion

        #region Commands

        private ICommand _start;
        public ICommand Start
        {
            get 
            { 
                if(_start == null)
                    _start = new RelayCommand(ExecuteStartTimer, CanExecute);

                return _start;
            }               
        }

        private ICommand _stop;
        public ICommand Stop
        {
            get
            {
                if (_stop == null)
                    _stop = new RelayCommand(ExecuteStopTimer, CanExecute);

                return _stop;
            }
        }

        private ICommand _pause;
        public ICommand Pause
        {
            get
            {
                if (_pause == null)
                    _pause = new RelayCommand(ExecutePauseTimer, CanExecute);

                return _pause;
            }
            
        }

        private void ExecutePauseTimer() => dt.Stop();

        private void ExecuteStartTimer() => dt.Start();

        private void ExecuteStopTimer()
        {
            dt.Stop();
            Seconds = 0;
            Minutes = 0;
            Hours = 0;
        }

        #endregion

        private bool CanExecute() => true;       

        #region INPC & ICommand

        public event PropertyChangedEventHandler PropertyChanged; 

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
        }

        #endregion

    }
}
