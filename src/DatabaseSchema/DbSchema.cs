using System.Collections.Generic;
using System.Linq;

namespace DatabaseSchema
{
    public class DbSchema
    {
        private IEnumerable<DbColumn> _columns;
        private IEnumerable<DbIndex> _indexes;

        public IEnumerable<DbColumn> Columns
        {
            get { return _columns ?? Enumerable.Empty<DbColumn>(); } 
            set { _columns = value; }
        }

        public IEnumerable<DbIndex> Indexes
        {
            get { return _indexes ?? Enumerable.Empty<DbIndex>(); }
            set { _indexes = value; }
        }
    }
}
