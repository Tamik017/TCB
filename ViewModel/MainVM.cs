using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TwitchBot.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace TwitchBot.ViewModel
{
    class MainVM : ObservableObject
    {
        //Закрыть окно
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

        //Отображаемое окно
        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        public ICommand DashboardCommand;
        public ICommand CommandsCommand;
        public ICommand GiveawayCommand;
        public ICommand SettingsCommand;
        public ICommand HelpCommand;

        private void Commands(object obj) => CurrentView = new CommandsVM();
        private void Dashboard(object obj) => CurrentView = new DashboardVM();
        private void Giveaway(object obj) => CurrentView = new GiveawayVM();
        private void Settings(object obj) => CurrentView = new SettingsVM();
        private void Help(object obj) => CurrentView = new HelpVM();

        public MainVM()
        {
            CommandsCommand = new RelayCommand(Commands);
            DashboardCommand = new RelayCommand(Dashboard);
            GiveawayCommand = new RelayCommand(Giveaway);
            SettingsCommand = new RelayCommand(Settings);
            HelpCommand = new RelayCommand(Help);

            CurrentView = new CommandsVM();
        }
    }
}
