using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeatTheStormSystem
{
    public class Spot : INotifyPropertyChanged
    {
        public List<Player> SpotPlayers { get; private set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddPlayerToSpot(Player player)
        {
            this.SpotPlayers.Add(player);
            this.InvokePropertyChanged("SpotPlayerDescription");
        }
        public void RemovePlayerFromSpot(Player player)
        {
            this.SpotPlayers.Remove(player);
            this.InvokePropertyChanged("SpotPlayerDescription");
        }
        public string AllPlayersInSpot()
        {
            string s = "";
            foreach (Player p in this.SpotPlayers)
            {
                s += p.PlayingPiece;
            }
            return s;
        }
        public string SpotPlayerDescription
        {
            get => this.AllPlayersInSpot();
        }
        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
