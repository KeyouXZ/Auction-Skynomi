using Skynomi.Utils;
using TShockAPI;
using Terraria;

namespace Skynomi.AuctionSystem
{
    public class Auction : Loader.ISkynomiExtension, Loader.ISkynomiExtensionPostInit
    {
        public string Name => "Auction";
        public string Description => "Auction extension for Skynomi.";
        public string Version => "1.0.0";
        public string Author => "Keyou";

        public void Initialize()
        {
            if (!Main.ServerSideCharacter)
            {
                return;
            }
            AuctionSystem.Database.Initialize();
            AuctionSystem.Commands.Initialize();
        }

        public void PostInitialize(EventArgs args)
        {
            if (!Main.ServerSideCharacter)
            {
                TShock.Log.ConsoleError($"{Skynomi.Utils.Messages.Name} Auction has been disabled because Server Side Character is not enabled.");
                return;
            }
        }
    }
}
