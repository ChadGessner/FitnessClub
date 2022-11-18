using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class SingleMember : Members
    {
        public override Types Type { get; set; } = (Types)0;
        public Club Club { get; set; }
        public override string CheckIn(Club club) => base.CheckIn(Club);
        public SingleMember(Club club)
        {
            Club = club;
        }
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