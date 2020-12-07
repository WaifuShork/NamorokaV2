using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord.WebSocket;
using Newtonsoft.Json;

// TODO: Fix this heavily retarded class so there's at least some modularity or readability, everything is chunked together, and looks absolutely horrendous, and its entirely inefficient

namespace NamorokaV2
{
    public static class DatabaseService
    {
        private const string data = @"G:\source\NamorokaV2\NamorokaV2\NamorokaV2\database.json";

        public static void AddToDatabase(SocketGuildUser user, string reason)
        {
            SearchForUser(user, reason);
        }
        public static void AddToDatabase(SocketGuildUser user)
        {
            SearchForUser(user);
        }

        public static IEnumerable<string> RetrieveFromDatabase(SocketGuildUser user)
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

        // Overload for no reason given
        private static void SearchForUser(SocketGuildUser user)
        {
            string initializeJson = File.ReadAllText(data);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(initializeJson);
            ulong userId = user.Id;
            IEnumerable<User> filteredUsers = users.Where(u => u.Id == userId);
            if(filteredUsers.Count() != 0)
            {   
                User selectedUser = filteredUsers.First();
                
                users.Remove(selectedUser);
                selectedUser.Reason.Add("There was no given reason for this warning");
                users.Add(selectedUser);
            }
            else
            {
                users.Add(new User { Id = userId, Reason = new List<string>() { "There was no given reason for this warning" } });
            }
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(data, json);
        }
        // Overload for reason given
        private static void SearchForUser(SocketGuildUser user, string reason)
        {
            string initializeJson = File.ReadAllText(data);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(initializeJson);
            ulong userId = user.Id;
            IEnumerable<User> filteredUsers = users.Where(u => u.Id == userId);
            if(filteredUsers.Count() != 0)
            {   
                User selectedUser = filteredUsers.First();
                users.Remove(selectedUser);
                selectedUser.Reason.Add(reason); 
                users.Add(selectedUser);
            }
            else
            {
                users.Add(new User { Id = userId, Reason = new List<string>() { reason } });
            }
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(data, json);
        }
    }
}