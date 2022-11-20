using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    // enum to act as pointer to SingleMember(), MultiMember() or Club(),
    // for dealing with casting to and from IWriteable,
    // each of those Classes is automatically assigned their,
    // corresponding Types in Type field upon instantiation
    public enum Types
    {
        single,
        multi,
        club,
        checkin
    }
}
