using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class SingleMember : Members
    {

        public override int ID { get; set; }
        public override string FullName { get; set; }
        public override int DateOfBirth { get; set; } //date time
        public override int JoinDate { get; set; }
        public Club Club { get; set; }

        public override void CheckIn(Club club)
        {
            //throw exception if its not their club
        }
    }
}
