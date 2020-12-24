using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord;
using Newtonsoft.Json;

// TODO: Fix this heavily retarded class so there's at least some modularity or readability, everything is chunked together, and looks absolutely horrendous, and its entirely inefficient

// What do I want to do? 
// 1: be able to search for a user
// 2: be able to store located a user
// 3: be able to retrieve located a user

namespace NamorokaV2
{
    public static class DatabaseService
    {
        private const string data = @"..\..\..\database.json";

        /*public static async Task AddToDatabase(SocketGuildUser user, string reason)
        {
            await SearchForUser(user, reason);
        }

        public static async Task AddToDatabase(SocketGuildUser user)
        {
            await SearchForUser(user);
        }*/

        /*public static IEnumerable<string> RetrieveFromDatabase(SocketGuildUser user)
        {
            string initializeJson = File.ReadAllText(data);
            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(initializeJson);
            ulong userId = user.Id;
            List<string> reasonList = new List<string>();
            UserModel locateUser = users.Find(u => u.Id == userId);
            if (locateUser == null) return reasonList;
            reasonList.AddRange(locateUser.Reason);
            return reasonList;
        }*/

        public static void AddUserToDatabase(IGuildUser user)
        {
            List<UserModel> users = DeserializeUsersFromDatabase();
            
            UserModel selectedUser = FindUserInDatabase(user);
            
            selectedUser.Id = user.Id;
            selectedUser.Reason.Add(" ");
            users.Add(selectedUser);
            SerializeUserToDatabase(users);
            
            
            //string initializeJson = await File.ReadAllTextAsync(data);
            
            //ulong userId = user.Id;
            /*IEnumerable<UserModel> filteredUsers = users.Where(u => u.Id == userId);
            IEnumerable<UserModel> enumerable = filteredUsers.ToList();
            if (enumerable.Any())
            {
                UserModel selectedUser = enumerable.FirstOrDefault();
                users.Remove(selectedUser);
                selectedUser.Reason.Add("There was no given reason for this warning");
                users.Add(selectedUser);
            }
            else
            {
                users.Add(new UserModel
                    {Id = userId, Reason = new List<string>() {"There was no given reason for this warning"}});
            }*/
        }

        private static void SerializeUserToDatabase(List<UserModel> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(data, json);
        }
        
        // Overload for reason given
        /*private static async Task SearchForUser(SocketGuildUser user, string reason)
        {
            string initializeJson = await File.ReadAllTextAsync(data);
            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(initializeJson);
            ulong userId = user.Id;
            IEnumerable<UserModel> filteredUsers = users.Where(u => u.Id == userId);
            IEnumerable<UserModel> enumerable = filteredUsers.ToList();
            if (enumerable.Any())
            {
                UserModel selectedUser = enumerable.FirstOrDefault();
                users.Remove(selectedUser);
                selectedUser.Reason.Add(reason);
                users.Add(selectedUser);
            }
            else
            {
                users.Add(new UserModel {Id = userId, Reason = new List<string>() {reason}});
            }

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            await File.WriteAllTextAsync(data, json);
        }

        /*public static async Task RemoveUserReasonsAsync(SocketGuildUser user/*, int infractionId)
        {
            //string initializeJson = await File.ReadAllTextAsync(data);
            //List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(initializeJson);
            //ulong userId = user.Id;
            //IEnumerable<UserModel> filteredUsers = users.Where(u => u.Id == userId);
            //IEnumerable<UserModel> enumerable = filteredUsers.ToList();
            //if (enumerable.Any())
            //{
            //    UserModel selectedUser = enumerable.First();
            //    users.Remove(selectedUser);
            //}
            UserModel users = FindUserInDatabase(user);

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            await File.WriteAllTextAsync(data, json);
        }*/
        
        // what do I want to happen: 
        // when I type warn @User
        // I want the database to search the list for a user
        // if that user exists, append a warning
        // if that user does not exist, create a new one and add a warning

        public static UserModel SearchDatabaseForUser(IGuildUser user)
        {
            // deserialize the json to a list 
            var users = DeserializeUsersFromDatabase();
            
            // find the user 
            var selectUser = users.Where(u => u.Id == user.Id);
            
            // does the user exist? 

            // no the user does not exists 
            IEnumerable<UserModel> userModels = selectUser.ToList();
            var locatedUser = userModels.First();
            
            if (locatedUser.Equals(null))
            {
                // So lets create a new user and add it, since we'll need it there anyways
                // We only need an id, because we just want the user to exist 
                users.Add(new UserModel { Id = user.Id });
                
                // since we've now added the user, we can recheck the database to return a user
                Console.WriteLine(userModels.First());

                return userModels.First();
            }
    
            // yes the user exists so lets return it 
            Console.WriteLine(locatedUser);
            return locatedUser;
        }
        

        private static UserModel FindUserInDatabase(IGuildUser user)
        {
            //string initializeJson = File.ReadAllText(data);
            //List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(initializeJson);
            List<UserModel> users = DeserializeUsersFromDatabase();
            
            ulong userId = user.Id;

            IEnumerable<UserModel> filteredUsers = users.Where(u => u.Id == userId);
            IEnumerable<UserModel> enumerableUsers = filteredUsers.ToList();
            return enumerableUsers.FirstOrDefault();
        }

        private static List<UserModel> DeserializeUsersFromDatabase()
        {
            string initializeJson = File.ReadAllText(data);
            return JsonConvert.DeserializeObject<List<UserModel>>(initializeJson);
        }
    }
}