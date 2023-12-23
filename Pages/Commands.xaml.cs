using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static TwitchBot.MainWindow;
using TwitchBot.Model;
using TwitchBot.View;

namespace TwitchBot.Pages
{
    public partial class Commands : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public Commands()
        {
            InitializeComponent();
            LoadDataAsync();
        }
        
        private async void LoadDataAsync()
        {
            try
            {
                ObservableCollection<Command> commands = await GetDataAsync();
                Dispatcher.Invoke(() =>
                {
                    commandsDataGrid.ItemsSource = commands;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private async Task<ObservableCollection<Command>> GetDataAsync()
        {
            return mainWindow.cmd;
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            mainWindow.WorkingArea.Children.Add(message);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var commands = mainWindow.commands;
            Message message = new Message("Edit command", mainWindow.commands[commandsDataGrid.SelectedIndex].CommandText, mainWindow.commands[commandsDataGrid.SelectedIndex].Cmd, commandsDataGrid.SelectedIndex);

            mainWindow.WorkingArea.Children.Add(message);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int SelectedIndex = commandsDataGrid.SelectedIndex;

            mainWindow.commands.RemoveAt(SelectedIndex);
            mainWindow.cmd.RemoveAt(SelectedIndex);

            UpdateAllIndex();

            commandsDataGrid.ItemsSource = null;
            commandsDataGrid.ItemsSource = mainWindow.cmd;

            FileManager file = new FileManager();
            file.WriteToFileCommands(mainWindow.commands);
        }

        private void UpdateAllIndex()
        {
            for (int i = 1; i < mainWindow.commands.Count + 1; i++)
            {
                mainWindow.commands[i - 1].Number = i;
                mainWindow.cmd[i - 1].Number = i;
            }
        }
    }
}
