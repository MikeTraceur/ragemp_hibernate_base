using LoginResource.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginResource.Repository
{
    class UserCharacterRepository : NHibernateRepository<UserCharacter>
    {

        public UserCharacterRepository() : base()
        {
        }

        public UserCharacterRepository(ISession Session, ITransaction Transaction) : base(Session, Transaction)
        {
        }

        public void DeleteUserCharacter(UserCharacter UserCharacter)
        {
            base.Delete(UserCharacter);
        }

        public UserCharacter GetUserCharacterById(int id)
        {
            return base.Retrieve(id);
        }

        public IQueryable<UserCharacter> GetUserCharacterCollection()
        {
            return base.RetrieveAll();
        }

        public void SaveUserCharacter(UserCharacter UserCharacter)
        {
            base.Save(UserCharacter);
        }
        public List<UserCharacter> GetUserCharacterWithName(string name)
        {
            return RetrieveAll().Where(i => i.user.Name.Equals(name)).AsQueryable().ToList();
        }

    }
}
