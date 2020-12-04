using GTANetworkAPI;
using LoginResource.Config;
using LoginResource.Domain;
using LoginResource.Repository;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginResource.Service
{
    class UserCharacterService
    {
        public static readonly UserCharacterService instance = new UserCharacterService();

        private UserCharacterService() { }

        public UserCharacter loadUserCharacterById(int id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    return new UserCharacterRepository(session, tx).GetUserCharacterById(id);
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

        public UserCharacter createNewUserCharacter(Player player, int mask)
        {
            int id = player.GetData<int>("ID");
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    UserRepository userRepository = new UserRepository(session, tx);
                    UserCharacterRepository userCharacterRepository = new UserCharacterRepository(session, tx);
                    User lUser = userRepository.GetUserById(id);
                    if(lUser == null) {
                        throw new Exception("Not logged In");
                    }
                    UserCharacter userCharacter = new UserCharacter() { user = lUser, CSlot1 = mask };
                    userCharacterRepository.SaveUserCharacter(userCharacter);
                    tx.Commit();
                    return userCharacter;
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

        public UserCharacter saveUserCharacter(Player player)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    UserRepository userRepository = new UserRepository(session, tx);
                    UserCharacterRepository userCharacterRepository = new UserCharacterRepository(session, tx);
                    int id = player.GetData<int>("ID");
                    User lUser = userRepository.GetUserById(id);
                    if (lUser == null)
                    {
                        throw new Exception("Not logged In");
                    }
                    int charid = player.GetData<int>("CHARID");
                    UserCharacter lcUserCharacter = userCharacterRepository.GetUserCharacterById(charid);
                    if (lcUserCharacter == null)
                    {
                        throw new Exception("Not logged In");
                    }
                    Vector3 currentPosition = NAPI.Entity.GetEntityPosition(player);
                    Vector3 currentRotation = NAPI.Entity.GetEntityRotation(player);

                    lcUserCharacter.X = currentPosition.X;
                    lcUserCharacter.Y = currentPosition.Y;
                    lcUserCharacter.Z = currentPosition.Z;
                    lcUserCharacter.RX = currentRotation.X;
                    lcUserCharacter.RY = currentRotation.Y;
                    lcUserCharacter.RZ = currentRotation.Z;
                    userCharacterRepository.SaveUserCharacter(lcUserCharacter);
                    tx.Commit();
                    return lcUserCharacter;
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
