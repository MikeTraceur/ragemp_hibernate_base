using GTANetworkAPI;
using LoginResource.Config;
using LoginResource.Domain;
using LoginResource.Repository;
using LoginResource.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LoginResource.Service
{
    class UserService
    {
        public static readonly UserService instance = new UserService();

        private UserService() {
        }

        public User RegisterUser(Player client, string name, string passwort)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    UserRepository userRepository = new UserRepository(session, tx);
                    List<User> userList = userRepository.GetUserWithName(name);
                    if (userList != null && userList.Count > 0)
                    {
                        NAPI.Util.ConsoleOutput("Benutzer existiert bereits");
                        client.SendChatMessage("Benutzer existiert bereits");
                        tx.Rollback();
                        return null;
                    }
                    string passwordHash = HashUtil.HashPassword(passwort);
                    User user = new User() { Name = name, Passwort = passwordHash, SocialID = client.SocialClubName };
                    userRepository.Save(user);
                    tx.Commit();
                    return user;
                }
                catch (Exception e)
                {
                    NAPI.Util.ConsoleOutput(e.ToString());
                }
                finally
                {
                    NHibernateHelper.CloseSession();
                }
            }
            return null;
        }

        public User LoginUser(Player client, string name, string passwort)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    UserRepository userRepository = new UserRepository(session, tx);
                    List<User> userList = userRepository.GetUserWithName(name);
                    if (userList == null || userList.Count == 0)
                    {
                        NAPI.Util.ConsoleOutput("Benutzer existiert nicht");
                        return null;
                    }
                    else if (userList.Count > 1)
                    {
                        NAPI.Util.ConsoleOutput("Benutzer existiert mehrmals");
                        return null;
                    }
                    User user = userList[0];
                    if (!HashUtil.CheckPassword(user, passwort)) {
                        NAPI.Util.ConsoleOutput("Passwort stimmt nicht überein");
                        client.SendChatMessage("Passwort stimmt nicht überein");
                        return null;
                    }
                    if(user.WhitelistStatus == 0)
                    {
                        NAPI.Util.ConsoleOutput("Account ist nicht whitelisted");
                        client.SendChatMessage("Account ist nicht whitelisted");
                        return null;
                    }
                    return user;
                }
                catch (Exception e)
                {
                    NAPI.Util.ConsoleOutput(e.ToString());
                }
                finally
                {
                    NHibernateHelper.CloseSession();
                }
            }
            return null;
        }
    }
}
