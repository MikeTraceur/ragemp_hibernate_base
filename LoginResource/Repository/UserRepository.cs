using LoginResource.Domain;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginResource.Repository
{
    public class UserRepository : NHibernateRepository<User>
    {

        public UserRepository() : base()
        {
        }

        public UserRepository(ISession Session, ITransaction Transaction) : base(Session, Transaction)
        {
        }

        public void DeleteUser(User User)
        {
            base.Delete(User);
        }

        public User GetUserById(int id)
        {
            return base.Retrieve(id);
        }

        public IQueryable<User> GetUserCollection()
        {
            return base.RetrieveAll();
        }

        public void SaveUser(User User)
        {
            base.Save(User);
        }
        public List<User> GetUserWithName(string name)
        {
            return RetrieveAll().Where(i => i.Name.Equals(name)).AsQueryable().ToList();
        }

    }
}
