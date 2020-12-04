using GTANetworkAPI;
using LoginResource.Domain;
using LoginResource.Service;

namespace LoginResource.Controller
{
    class UserController : Script
    {
        private UserService userService;

        public UserController()
        {
            this.userService = UserService.instance;
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            player.SendChatMessage("Welcome to our server!");
        }

        [Command("register")]
        public void RegisterCommand(Player player, string name, string passwort)
        {
            NAPI.Util.ConsoleOutput("TestCommand Invoked!");
            User user = userService.RegisterUser(player, name, passwort);
            if(user != null)
            {
                player.SendChatMessage($"Hi {name} with passwort {passwort}");
            }
        }

        [Command("login")]
        public void LoginCommand(Player player, string name, string passwort)
        {
            if(player.GetData<int>("ID") != 0)
            {
                NAPI.Util.ConsoleOutput("Benutzer bereits angemeldet");
                player.SendChatMessage($"Du bist bereits angemeldet");
            }
            NAPI.Util.ConsoleOutput("TestCommand Invoked!");
            User user = userService.LoginUser(player, name, passwort);
            if (user == null)
            {
                return;
            }
            player.SendChatMessage($"Hi {name} with passwort {passwort}");
            player.SetData<int>("ID", user.ID);

            string ids = "";
            foreach (UserCharacter userCharacter in user.UserCharacter)
            {
                ids += userCharacter.ID + " ";
            }
            player.SendChatMessage($"Available Characters: {ids}");
            player.SendChatMessage($"Choose one with /char [id], create one with /newchar");

        }
        [Command("getpos")]
        public void GetPosition(Player player)
        {
            Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
            NAPI.Chat.SendChatMessageToPlayer(player, "X: " + PlayerPos.X + " Y: " + PlayerPos.Y + " Z: " + PlayerPos.Z);
        }
    }
}
