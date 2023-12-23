using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchBot.Model;

namespace TwitchBot.View
{
    public class FileManager
    {
        public FileManager() 
        {
            if(!Directory.Exists("Settings"))
            {
                Directory.CreateDirectory("Settings");
            }
        }

        public BotAuth ReadAuthDate()
        {
            BotAuth auth = new BotAuth();
            if (File.Exists("Settings\\Auth.json"))
            {
                using (FileStream stream = new FileStream("Settings\\Auth.json", FileMode.Open))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    auth = JsonSerializer.Deserialize<BotAuth>(json);
                }
                return auth;
            }
            return auth;
        }

        public void WriteAuthDate(BotAuth commands)
        {
            using (FileStream stream = new FileStream("Settings\\Auth.json", FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string json = JsonSerializer.Serialize(commands);
                writer.Write(json);
            }
        }

        public void WriteToFileCommands(List<Command> commands)
        {
            using (FileStream stream = new FileStream("Settings\\Commands.json", FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string json = JsonSerializer.Serialize(commands);
                writer.Write(json);
            }
        }

        public List<Command> ReadFromFileCommands()
        {
            List<Command> command = new List<Command>();
            if (File.Exists("Settings\\Commands.json"))
            {
                using (FileStream stream = new FileStream("Settings\\Commands.json", FileMode.Open))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    command = JsonSerializer.Deserialize<List<Command>>(json);
                }
                return command;
            }
            return command;
        }
    }
}
