using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public abstract class Members
    {
        public abstract int ID { get; set; }
        public abstract string FullName { get; set; }
        public abstract int DateOfBirth { get; set; } //date time
        public abstract int JoinDate { get; set; }

        public abstract void CheckIn(Club club);
    }

}
