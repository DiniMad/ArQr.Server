using System.Collections.Generic;

namespace ArQr.Controllers.Resources
{
    public class PaginationData<T>
    {
        public IEnumerable<T> Collection { get; }
        public string         Next       { get; }
        public string         Previous   { get; }

        public PaginationData(IEnumerable<T> collection, string next, string previous)
        {
            Collection = collection;
            Next       = next;
            Previous   = previous;
        }
    }
}