using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace NamorokaV2
{
    public static class DatabaseService
    {
        private const string data = @"G:\source\NamorokaV2\NamorokaV2\NamorokaV2\database.json";

        public static void AddToDatabase(SocketGuildUser user, [Remainder] string reason)
        {
            string initializeJson = File.ReadAllText(data);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(initializeJson);
            ulong userId = user.Id;

            IEnumerable<User> filteredUsers = users.Where(u => u.Id == userId);
            if(filteredUsers.Count() != 0)
            {   
                User selectedUser = filteredUsers.First();
                if(user != null)
                {
                    users.Remove(selectedUser);
                    selectedUser.Reason.Add(reason);
                    users.Add(selectedUser);
                }
            }
            else
            {
                users.Add(new User { Id = userId, Reason = new List<string>() { reason } });
            }
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(data, json);
        }
        public static void AddToDatabase(SocketGuildUser user)
        {
            string initializeJson = File.ReadAllText(data);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(initializeJson);
            ulong userId = user.Id;
            IEnumerable<User> filteredUsers = users.Where(u => u.Id == userId);
            if(filteredUsers.Count() != 0)
            {   
                User selectedUser = filteredUsers.First();
                if(user != null)
                {
                    users.Remove(selectedUser);
                    selectedUser.Reason.Add("There was no given reason for this warning");
                    users.Add(selectedUser);
                }
            }
            else
            {
                users.Add(new User { Id = userId, Reason = new List<string>() { "There was no given reason for this warning" } });
            }
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(data, json);
        }

        public static List<string> RetrieveFromDatabase(SocketGuildUser user)
        {
            string initializeJson = File.ReadAllText(data);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(initializeJson);
            ulong userId = user.Id;
            List<string> reasonList = new List<string>();
            User locateUser = users.Find(u => u.Id == userId);
            if (locateUser == null) return reasonList;
            reasonList.AddRange(locateUser.Reason);
            return reasonList;
        }
    }
}