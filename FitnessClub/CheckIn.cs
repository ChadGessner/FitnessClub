using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class CheckIn : IWriteable
    {
        public Club Club { get; set; }
        public Members Member { get; set; }
        public DateTime DateTime { get; set;  } 
        public Types Type { get; set; } = Types.checkin;
        public CheckIn(Club club, Members member, DateTime dateTime)
        {
            Club = club;
            Member = member;
            DateTime = dateTime;
        }
        public string DataToString()
        {
            return $"{Club.DataToString()}|{Member.DataToString()}|{DateTime}";
        }
    }
}
