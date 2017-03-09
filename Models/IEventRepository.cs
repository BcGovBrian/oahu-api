using System.Collections.Generic;

namespace oahu_api.Models
{
    public interface IEventRepository
    {
        void Add(Event item);

        IEnumerable<Event> GetAllFromDevice(long key);

        Event GetLatestEventFromDevice(long id);

        Event Find(long key);

    }
}