using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeatTheStormSystem
{
    public class Player : INotifyPropertyChanged
    {
        private Spot _spotvalue = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        public string PlayerName { get; set; } = "";
        public string PlayingPiece { get; set; } = "";
        public Spot SpotValue
        {
            get => _spotvalue; set
            {
                _spotvalue = value;
                this.InvokePropertyChanged();
            }
        }
        public string PlayerDescription { get => $"{this.PlayerName} {this.PlayingPiece}"; }
        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
