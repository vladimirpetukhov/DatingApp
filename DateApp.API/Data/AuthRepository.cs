namespace DateApp.API.Data
{
    using Data;
    using System.Threading.Tasks;
    using DateApp.API.Models;
    using System.Security.Cryptography;
    using System.Text.Encodings;
    using System.Text.Unicode;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _db;
        public AuthRepository(DataContext context)
        {
            this._db = context;
        }
        public async Task<User> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;
            var user = await this._db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;
            if (!VerifyUserPassword(password, user.PasswordHash, user.PasswordSalt)) return null;

            return user;
        }

        private bool VerifyUserPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int b = 0; b < computeHash.Length; b++)
                {
                    if (computeHash[b] != passwordHash[b]) return false;

                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await this._db.AddAsync(user);
            await this._db.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _db.Users.AnyAsync(u => u.Username == username)) return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException($"password cant be null!");
                }
                using (var hmac = new HMACSHA512())
                {
                    passwordHash = hmac.Key;
                    passwordSalt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}