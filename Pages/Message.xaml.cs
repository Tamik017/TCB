using Notification.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchBot.Model;
using TwitchBot.View;

namespace TwitchBot.Pages
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>

    public partial class Message : UserControl
    {
        int Index = -1;
        public Message(string MessageName = "Add command", string Name = "", string command = "", int Index = -1)
        {
            InitializeComponent();
            Caption.Content = MessageName;
            CommandText.Text = Name;
            CMD.Text  = command;
            this.Index = Index;
        }
 
        private void DoubleAnimationUsingKeyFrames_Completed(object sender, EventArgs e)
        {
           MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
           mainWindow.WorkingArea.Children.Remove(this);
        }
 
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (CMD.Text.Length > 0 && CommandText.Text.Length > 0)
            {
                Command command = new Command();
                
                command.Cmd = CMD.Text;
                command.CommandText = CommandText.Text;

                if(Index == -1)
                {
                    command.Number = mainWindow.commands.Count + 1;
                    mainWindow.cmd.Add(command);
                    mainWindow.commands.Add(command);
                    
                }
                else
                {
                    command.Number = Index + 1;
                    mainWindow.commands[Index] = command;
                    mainWindow.cmd[Index] = command;
                }


                FileManager fileManager = new FileManager();
                fileManager.WriteToFileCommands(mainWindow.commands);

                var animation = (Storyboard)FindResource("Storyboard3");
                animation.Begin();
            }
            else
            {
                Notifi notifications = new Notifi();
                notifications.ShowNotification("Command not added. Check data.", NotificationType.Error);
            }
        }
    }
}