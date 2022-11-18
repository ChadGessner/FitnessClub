using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    // Interface for tying Club() and Members() + children,
    // Together so they can all be returned by the Read() and Write() classes
    public interface IWriteable
    {
        public Types Type { get; set; }
        string DataToString();
        Type GetBase();
    }
}
