using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using TwitchBot.ViewModel;
using TwitchBot.Model;
using TwitchBot.View;
using TwitchBot.Pages;
using Notification.Wpf;

namespace TwitchBot
{
    public partial class MainWindow : Window
    {
        public BotAuth BotAuth;  

        public List<Command> commands = new List<Command>();
        public ObservableCollection<Command> cmd = new ObservableCollection<Command>();
        
        public Bot bot;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Navigation();
            //Bot bot = new Bot();
            LoadDataAsync();
            FileManager FM = new FileManager();
            BotAuth = FM.ReadAuthDate();
            Bot();
        }

        public void zapolnenie()
        {
            BotAuth.BotName = "tcbot1337";
            FileManager FM = new FileManager();
            FM.WriteAuthDate(BotAuth);
            if (BotAuth.Auth != null && BotAuth.ChannelName != null && BotAuth.Auth != string.Empty && BotAuth.ChannelName != string.Empty)
            {
                Bot();
            }
        }

        private async void Bot()
        {
            if (BotAuth.Auth != null && BotAuth.ChannelName != null && BotAuth.Auth != string.Empty && BotAuth.ChannelName != string.Empty)
            {
                bot = new Bot(BotAuth);
                if(bot != null)
                { 
                    Notifi notifications = new Notifi();
                    notifications.ShowNotification("Bot was connected", NotificationType.Success);
                }
            }
        }

        private async void LoadDataAsync()
        {
            FileManager file = new FileManager();
            commands = file.ReadFromFileCommands();

            foreach (var i in commands)
                cmd.Add(i); 
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
