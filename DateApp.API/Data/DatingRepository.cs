using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DateApp.API.Data {
    public class DatingRepository : IDatingRepository {
        private readonly DataContext _context;
        public DatingRepository (DataContext context) {
            this._context = context;
        }
        public void Add<T> (T entity) where T : class {
            try {
                this._context.Add (entity);
            } catch (Exception Ex) {

            }
        }

        public void Delete<T> (T entity) where T : class {
            this._context.Remove (entity);
        }

        public async Task<User> GetUser (int id) {
            var user = await this._context.Users.FirstOrDefaultAsync (u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers () {
            var users = await this._context.Users.Include (p => p.Photos)
                .ToListAsync ();
            return users;
        }

        public async Task<bool> SaveAll () {
            return await this._context.SaveChangesAsync () > 0;
        }
    }
}