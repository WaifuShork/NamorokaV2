using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Newtonsoft.Json;

namespace NamorokaV2.NamorokaCore.Services
{
    public static class DatabaseService
    {
        private static readonly List<UserModel> _users = DeserializeUsersFromDatabase();
        private const string data = @"..\..\..\database.json";
        
        private static async Task SerializeUserToDatabaseAsync(List<UserModel> users)
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            await File.WriteAllTextAsync(data, json);
        }

        public static async Task AddInfractionAsync(IGuildUser user, string reason)
        {
            await PushUserToDatabaseAsync(user, reason);
        }
        
        public static async Task AddInfractionAsync(IGuildUser user)
        { 
            await PushUserToDatabaseAsync(user);
        }

        private static async Task PushUserToDatabaseAsync(IGuildUser user, string reason = " ")
        {
            var pushableUser = FindUserInDatabase(user);
            _users.Remove(pushableUser);
            pushableUser.Reason.Add(reason);
            
            _users.Add(pushableUser);
            await SerializeUserToDatabaseAsync(_users);
        }

        public static UserModel FindUserInDatabase(IGuildUser user, string reason = " ")
        {
            var userId = user.Id;
            var filteredUsers = _users.Where(u => u.Id == userId).ToList();

            if (filteredUsers.Any())
            {
                var locatedUser = filteredUsers.First();
                return locatedUser;
            }
            return CreateNewUserInDatabase(user, reason);
        }
        
        private static UserModel CreateNewUserInDatabase(IGuildUser user, string reason)
        {
            Console.WriteLine("Lets create a new user!");
            var newUser = new UserModel
            {
                Id = user.Id,
                Reason = new List<string> { reason }
            };
            
            _users.Add(newUser);
            
            Console.WriteLine($"{newUser} has been created!");
            return newUser;
        }

        private static List<UserModel> DeserializeUsersFromDatabase()
        {
            var initializeJson = File.ReadAllText(data);
            var users = JsonConvert.DeserializeObject<List<UserModel>>(initializeJson);
            return users;
        }
        
        public static IEnumerable<string> RetrieveFromDatabase(IGuildUser user)
        {
            var reasonList = new List<string>();
            var locateUser = _users.Find(u => u.Id == user.Id);
            if (locateUser == null) return reasonList;
            reasonList.AddRange(locateUser.Reason);
            return reasonList;
        }


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

        /*public static void AddUserToDatabase(IGuildUser user)
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
            }
        }*/
        
        
        
        
        
        
        
        
        
        
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


        /*public static UserModel SearchDatabaseForUser(IGuildUser user)
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
        }*/
    }
}