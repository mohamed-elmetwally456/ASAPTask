using ASAP_Task.Models;

namespace ASAP_Task.Reposatory
{
    public class FilterRepo : IFilterRepo
    {
        private readonly Context context;

        public FilterRepo(Context context)
        {
            this.context = context;
        }
        public IEnumerable<Clients> FilterByFname(string fname)
        {
            return context.Clients.Where(c => c.FirstName == fname).ToList();
        }
        public IEnumerable<Clients> FilterByLname(string lname)
        {
            return context.Clients.Where(c => c.LastName == lname).ToList();
        }
    }
}
