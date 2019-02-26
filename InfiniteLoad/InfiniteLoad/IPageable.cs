
using System.Threading.Tasks;

namespace InfiniteLoad
{
    public interface IPageable
    {
        Task LoadNextPage();
        bool IsLoadingPage { get; set; }
    }
}