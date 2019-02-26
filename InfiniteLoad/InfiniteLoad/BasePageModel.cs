using System.ComponentModel;
using System.Runtime.CompilerServices;
using InfiniteLoad.Annotations;

namespace InfiniteLoad
{
    public abstract class BasePageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}