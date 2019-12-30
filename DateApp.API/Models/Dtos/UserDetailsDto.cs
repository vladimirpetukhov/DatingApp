using System;
using System.Collections.Generic;

namespace DateApp.API.Models.Dtos
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public ICollection<PhotoForDetailsDto> Photos { get; set; }
    }
}