using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Forms;

namespace InfiniteLoad
{
    public class MainPageModel : BasePageModel, IPageable
    {
        private const string _pageTitle = "Infinite Scroll List";
        private const int _pageSize = 50;
        private readonly ItemService _itemService;
        private bool _isLoading;
        private string _title;
        
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

        public MainPageModel(ItemService itemService) // please actually code against an interface and use Dependency Injection
        {
            _itemService = itemService;
            LoadNextPage();
            Title = _pageTitle;
        }

        public Command ResetListCommand => new Command(() =>
        {
            Items.Clear();
            LoadNextPage();
        });

        
        public async void LoadNextPage()
        {
            IsLoadingPage = true;
            Title = "Loading...";
            
            var newItems = await _itemService.ListItems(Items.Count, _pageSize, CancellationToken.None);
            foreach(var item in newItems)
                Items.Add(item);

            Title = _pageTitle;
            IsLoadingPage = false;
        }

        public bool IsLoadingPage
        {
            get => _isLoading;
            private set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
            
        }
    }

    public class Item
    {
        public string Text { get; set; }
    }
}