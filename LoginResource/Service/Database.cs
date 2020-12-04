using GTANetworkAPI;
using LoginResource.Config;
using LoginResource.Domain;
using NHibernate;
using System;

namespace LoginResource.Service
{
    class Database
    {
        public static bool RegisterAccount(string name, string passwort, string socialName)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    User user = new User { Name = name, Passwort = passwort, SocialID = socialName };
                    session.Save(user);
                    tx.Commit();
                    return true;
                }
            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput(e.ToString());
                return false;
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }
    }
}
