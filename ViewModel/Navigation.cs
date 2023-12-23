using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TwitchBot.Model;
using System.Windows.Input;
using TwitchBot.Pages;

namespace TwitchBot.ViewModel
{
    class Navigation : ObservableObject
    {
        private RelayCommand closeWin;
        public RelayCommand CloseWin
        {
            get
            {
                return closeWin ?? (closeWin = new RelayCommand(obj =>
                {
                    Application.Current.Shutdown();
                }));
            }
        }

        //Cвернуть окно
        private RelayCommand collapseWin;
        public RelayCommand CollapseWin
        {
            get
            {
                return collapseWin ?? (collapseWin = new RelayCommand(obj =>
                {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                }));
            }
        }

        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        public ICommand DashboardCommand { get; set; }
        public ICommand CommandsCommand { get; set; }
        public ICommand GiveawayCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand HelpCommand { get; set; }

        private void Commands(object obj) => CurrentView = new Commands();
        private void Dashboard(object obj) => CurrentView = new Dashboard();
        private void Giveaway(object obj) => CurrentView = new Giveaway();
        private void Settings(object obj) => CurrentView = new Settings((MainWindow)obj);
        private void Help(object obj) => CurrentView = new Help ();

        public Navigation()
        {
            CommandsCommand = new RelayCommand(Commands);
            DashboardCommand = new RelayCommand(Dashboard);
            GiveawayCommand = new RelayCommand(Giveaway);
            SettingsCommand = new RelayCommand(Settings);
            HelpCommand = new RelayCommand(Help);

            CurrentView = new Dashboard();
        }
    }
}
