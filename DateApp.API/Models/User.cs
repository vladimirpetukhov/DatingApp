using System.Collections.Generic;
namespace DateApp.API.Models {
    using System.Collections.Generic;
    using System;
    public class User {

        public User () {
            this.Photos = new List<Photo> ();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}