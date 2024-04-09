using ASAP_Task.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ASAP_Task.Reposatory
{
    public class ClientRepo : IClientRepo
    {
        private readonly Context context;

        public ClientRepo(Context context)
        {
            this.context = context;
        }
        public IEnumerable<Clients> GetAll()
        {
            return context.Clients.ToList();
        }
        public Clients GetByID(int id)
        {
            return context.Clients.SingleOrDefault(c=>c.ClientId==id);  
        }
        public void Add(Clients client)
        {
            context.Clients.Add(client);
            context.SaveChanges();
        }
        public void Update(Clients client)
        {
            context.Entry(client).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Clients.Remove(GetByID(id));
            context.SaveChanges();
        }
        public async Task<PaginatedList<Clients>> GetClients(int pageIndex, int pageSize)
        {
            var clients = await context.Clients
                .OrderBy(b => b.ClientId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await context.Clients.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginatedList<Clients>(clients, pageIndex, totalPages);
        }
    }
}
