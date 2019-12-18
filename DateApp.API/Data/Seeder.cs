using System.Collections.Generic;
using System.IO;
namespace DateApp.API.Data {

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System;
    using DateApp.API.Models;
    using Newtonsoft.Json;

    public class Seeder {
        public static void SeedUsers (DataContext context) {

            if (!context.Users.Any ()) {
                var users = new List<User> ();
                try {
                    var userData = System.IO.File.ReadAllText ("Data/UserSeedData.json");
                    users = JsonConvert.DeserializeObject<List<User>> (userData);
                } catch (Exception ex) {
                    throw new FileNotFoundException (ex.Message);
                }

                foreach (var user in users) {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash ("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower ();
                    context.Users.Add (user);
                }

                context.SaveChanges ();
            }

        }

        private static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            try {
                if (string.IsNullOrEmpty (password)) {
                    throw new ArgumentException ($"password cant be null!");
                }
                using (var hmac = new HMACSHA512 ()) {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));

                }
            } catch (System.Exception) {
                throw;
            }

        }
    }
}