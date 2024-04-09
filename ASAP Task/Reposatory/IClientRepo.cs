using ASAP_Task.Models;
using System.Numerics;

namespace ASAP_Task.Reposatory
{
    public interface IClientRepo
    {
        IEnumerable<Clients> GetAll();
        Task<PaginatedList<Clients>> GetClients(int pageIndex, int pageSize);
        Clients GetByID(int id);
        void Add(Clients client);   
        void Update(Clients client);
        void Delete(int id);
    }
}
