
namespace InfiniteLoad
{
    public interface IPageable
    {
        void LoadNextPage();
        bool IsLoadingPage { get;  }
    }
}