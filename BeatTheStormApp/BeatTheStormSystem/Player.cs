namespace BeatTheStormSystem
{
    public class Player
    {
        public string PlayerName { get; set; } = "";
        public string PlayingPiece { get; set; } = "";
        public Spot SpotValue { get; set; } = new();
        public string PlayerDescription { get => $"{this.PlayerName} {this.PlayingPiece}"; }
    }
}
