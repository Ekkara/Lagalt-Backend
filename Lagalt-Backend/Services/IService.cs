using Lagalt_Backend.Models;

namespace Lagalt_Backend.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllElements();
        Task<T> GetElementById(int id);
        Task<T> UpdateElement(T project);
        Task<T> AddElement(T project);
        Task DeleteElement(int id);
    }
}
