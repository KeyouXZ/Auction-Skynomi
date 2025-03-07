using TShockAPI;

namespace Skynomi.AuctionSystem
{
    public class Database
    {

        public static Skynomi.Database.Database db;
        private static string _databaseType = Skynomi.Database.Database._databaseType;
        public static void Initialize()
        {
            db = new Skynomi.Database.Database();

            if (_databaseType == "mysql")
            {
                db.CustomVoid(@"
                    CREATE TABLE IF NOT EXISTS Auctions (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Username VARCHAR(255) NOT NULL,
                        ItemId INT NOT NULL,
                        Price INT NOT NULL,
                        Amount INT NOT NULL
                    )
                ");
            }
            else if (_databaseType == "sqlite")
            {
                db.CustomVoid(@"
                    CREATE TABLE IF NOT EXISTS Auctions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username VARCHAR(255) NOT NULL,
                        ItemId INTEGER NOT NULL,
                        Price INTEGER NOT NULL,
                        Amount INTEGER NOT NULL
                    )
                ");
            }
        }

        public static bool AddAuction(string username, int itemId, int price, int amount)
        {
            try
            {
                db.CustomVoid("INSERT INTO Auctions (Username, ItemId, Price, Amount) VALUES (@username, @itemId, @price, @amount)",
                new { username, itemId, price, amount });

                return true;
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError(ex.ToString());
                return false;
            }
        }

        public static bool RemoveAuction(string username, int itemId)
        {
            try
            {
                db.CustomVoid("DELETE FROM Auctions WHERE Username = @username AND ItemId = @itemId", new { username, itemId });
                return true;
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError(ex.ToString());
                return false;
            }
        }

        public static bool UpdateAuction(string playername, int itemId, int amount)
        {
            try
            {
                if (amount <= 0)
                    return RemoveAuction(playername, itemId);

                db.CustomVoid("UPDATE Auctions SET Amount = @amount WHERE Username = @playername AND ItemId = @itemId", new { playername, itemId, amount });
                return true;
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError(ex.ToString());
                return false;
            }
        }

        public static dynamic listAuction(string playername, bool username = false)
        {
            if (username)
                return db.CustomVoid("SELECT ItemId, Price, Amount FROM Auctions WHERE Amount > 0 AND Username = @playername", new { playername }, output: true);

            return db.CustomVoid("SELECT Username, ItemId FROM Auctions WHERE Amount > 0", output: true);
        }
    }
}