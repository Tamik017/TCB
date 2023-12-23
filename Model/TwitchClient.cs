using System;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Events;
using System.Windows;
using TwitchBot.View;
using TwitchBot.Model;
using Notification.Wpf;

namespace TwitchBot.Model
{
    public class Bot
    {
        TwitchClient client;
        BotAuth botAuth;

        public Bot(BotAuth botAuth)
        {
            this.botAuth = botAuth;
            ConnectionCredentials credentials = new ConnectionCredentials(botAuth.BotName, botAuth.Auth);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, botAuth.ChannelName);

            client.OnLog += Client_OnLog;
            client.OnError += Client_OnError;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }
        
        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnError(object sender, OnErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            foreach (Command comm in mainWindow.cmd)
            {
                if(comm.Cmd.ToLower() == e.ChatMessage.Message.ToLower())
                {
                    client.SendMessage(e.ChatMessage.Channel, comm.CommandText);
                    break;
                }
            }
            //MessageBox.Show($"{e.ChatMessage.Username}: {e.ChatMessage.Message}");
        }

        public void disconnect()
        {
            if (client != null)
            {
                client.Disconnect();
            }
        }
    }
}