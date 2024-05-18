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
        
        private float _originalX;
        private float _originalY;

        public PointModel(float x, float y, float scale = 1)
        {
            X = x;
            Y = y;
            
            _originalX = X / scale;
            _originalY = Y / scale;
        }

        public void ChangeScale(float scale)
        {
            X = _originalX * scale;
            Y = _originalY * scale;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}