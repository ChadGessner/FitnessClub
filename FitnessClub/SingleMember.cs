using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class SingleMember : Members
    {
        public override Types Type { get; set; } = (Types)0;
        public Club Club { get; set; }
        public override CheckIn CheckIn(Club club)
        {
            return new CheckIn(Club,this, DateTime.Now);
        }
        public SingleMember(Club club)
        {
            Club = club;
        }
        // see parent for notes...
        
        public override string DataToString()
        {
            return $"{Type}|{Id}|{FullName}|{DateOfBirth}|{JoinDate}|{Club.DataToString()}";
        }
        public override Type GetBase()
        {
            return typeof(SingleMember);
        }
    }
}
