using ASAP_Task.Models;

namespace ASAP_Task.Reposatory
{
    public interface IFilterRepo
    {
        IEnumerable<Clients> FilterByFname(string fname);
        IEnumerable<Clients> FilterByLname(string lname);

    }
}
