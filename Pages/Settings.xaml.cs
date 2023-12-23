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
using TwitchBot.View;
using TwitchBot.Model;
using Notification.Wpf;

namespace TwitchBot.Pages
{
    public partial class Settings : UserControl
    {
        public BotAuth BotAuth;
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        FileManager FM = new FileManager();

        public Settings(MainWindow mainWindow)
        {
            InitializeComponent();
            
            BotAuth = FM.ReadAuthDate();
            txtoauthtxt.Text = BotAuth.Auth;
            txtchanneltxt.Text = BotAuth.ChannelName;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (txtOauth.Text != string.Empty && txtChannel.Text != string.Empty)
            {
                string Oauthuser = txtOauth.Text;
                string Channeluser = txtChannel.Text;
                txtoauthtxt.Text = Oauthuser;
                txtchanneltxt.Text = Channeluser;
                mainWindow.BotAuth.Auth = Oauthuser;
                mainWindow.BotAuth.ChannelName = Channeluser;

                mainWindow.zapolnenie();
                txtChannel.Text = string.Empty;
                txtOauth.Text = string.Empty;
            }
            else
            {
                Notifi notifications = new Notifi();
                notifications.ShowNotification("Enter all data", NotificationType.Error);
            }
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            txtoauthtxt.Text = null;
            txtchanneltxt.Text = null;
            mainWindow.BotAuth.Auth = null;
            mainWindow.BotAuth.ChannelName = null;

            mainWindow.zapolnenie();
            mainWindow.bot.disconnect();

            Notifi notifications = new Notifi();
            notifications.ShowNotification("Bot was disconnected", NotificationType.Warning);
        }
    }
}
