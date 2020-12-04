using System;
using GTANetworkAPI;
using LoginResource.Config;
using LoginResource.Domain;
using NHibernate;
using System.Linq;

namespace LoginResource
{
    public class Main: Script
    {
        public Main()
        {
        }


        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            /*ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    User user = new User { Name = "A", Passwort = "Test", SocialID = "bla" };
                    session.Save(user);
                    tx.Commit();
                }
            }catch(Exception e)
            {
                NAPI.Util.ConsoleOutput(e.ToString());
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }*/
            DisplayAllUser();
            NAPI.Util.ConsoleOutput("Login resource loaded!");
        }

        public void DisplayAllUser()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var users = from user in session.Query<User>()
                                    select user;

                    foreach (var f in users)
                    {
                        NAPI.Util.ConsoleOutput(String.Format("{0} {1} {2}", f.ID, f.Name, f.SocialID));
                    }
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }
    }
}
