using GTANetworkAPI;
using LoginResource.Domain;
using LoginResource.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginResource.Controller
{
    class UserCharacterController : Script
    {
        private UserCharacterService userCharacterService;

        public UserCharacterController()
        {
            this.userCharacterService = UserCharacterService.instance;
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            switch (type)
            {
                case DisconnectionType.Left:
                    NAPI.Chat.SendChatMessageToAll("~b~" + player.Name + "~w~ has quit the server.");
                    break;

                case DisconnectionType.Timeout:
                    NAPI.Chat.SendChatMessageToAll("~b~" + player.Name + "~w~ has timed out.");
                    break;

                case DisconnectionType.Kicked:
                    NAPI.Chat.SendChatMessageToAll("~b~" + player.Name + "~w~ has been kicked for " + reason);
                    break;
            }
            userCharacterService.saveUserCharacter(player);
        }

        [Command("char")]
        public void ChooseUserCharacterCommand(Player player, int index)
        {
            userCharacterService.saveUserCharacter(player);
            UserCharacter userCharacter = userCharacterService.loadUserCharacterById(index);
            applyChar(player, userCharacter);
        }
        [Command("newchar")]
        public void CreateUserCharacterCommand(Player player, int index)
        {
            userCharacterService.saveUserCharacter(player);
            UserCharacter userCharacter = userCharacterService.createNewUserCharacter(player, index);
            applyChar(player, userCharacter);
        }
        [Command("save")]
        public void SaveCommand(Player player)
        {
            NAPI.Util.ConsoleOutput("SaveCommand Invoked!");
            userCharacterService.saveUserCharacter(player);
        }

        private void applyChar(Player player, UserCharacter userCharacter)
        {
            if(userCharacter == null)
            {
                return;
            }
            player.SetData<int>("CHARID", userCharacter.ID);
            Vector3 spawnPos = new Vector3() { X = userCharacter.X, Y = userCharacter.Y, Z = userCharacter.Z, };
            Vector3 spawnRot = new Vector3() { X = userCharacter.RX, Y = userCharacter.RY, Z = userCharacter.RZ };
            NAPI.Entity.SetEntityPosition(player, spawnPos);
            NAPI.Entity.SetEntityRotation(player, spawnRot);
            player.SetClothes(1, userCharacter.CSlot1, 0);
        }
    }
}
