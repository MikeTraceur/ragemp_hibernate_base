using System;
using System.Collections.Generic;
using System.Text;

namespace LoginResource.Domain
{
    public class User
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Passwort { get; set; }
        public virtual string SocialID { get; set; }
        public virtual int WhitelistStatus { get; set; }

        public virtual ICollection<UserCharacter> UserCharacter { get; set; }

        public User() { }

    }
}
