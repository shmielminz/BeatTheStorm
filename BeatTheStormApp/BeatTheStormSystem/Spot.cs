namespace BeatTheStormSystem
{
    public class Spot
    {
        public List<Player> SpotPlayers { get; private set; } = new();
        public void AddPlayerToSpot(Player player)
        {
            this.SpotPlayers.Add(player);
        }
        public void RemovePlayerFromSpot(Player player)
        {
            this.SpotPlayers.Remove(player);
        }
        public string AllPlayersInSpot()
        {
            string s = "";
            foreach (Player p in this.SpotPlayers)
            {
                s += p.PlayerName + "; ";
            }
            return s;
        }
    }
}
