using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CityOrganisations.Models
{
    public class PointModel : INotifyPropertyChanged
    {
        public float X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public float Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }
        
        private float _x;
        private float _y;
        
        public PointModel(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}