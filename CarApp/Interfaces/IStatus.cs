using CarApp.Entities;

namespace CarApp.Interfaces
{
    public interface IStatus
    {
        Task<List<Status>> GetAll();
        Task AddNew(string name);
        Task Update(Status status);
        Task Delete(Status status);
        Task<Status> GetById(int id);
    }
}
