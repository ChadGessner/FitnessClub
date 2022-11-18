using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public interface IWriteable
    {
        public Types Type { get; set; }
        string DataToString();
        Type GetBase();
    }
}