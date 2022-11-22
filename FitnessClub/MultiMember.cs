using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class MultiMember : Members
    {
        public override Types Type { get; set; } = (Types)1;
        public int Points { get; set; }
        public MultiMember(int points)
        {
            Points = points;
        }
        public override CheckIn CheckIn(Club club)
        {
            Points += 10;
            DateTime today = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime shortBday = DateTime.Parse(DateOfBirth.ToShortDateString());
            if(today == shortBday)
            {
                Points += 15;
            }
            return new CheckIn(club, this, DateTime.Now);
        }
        public override string DataToString()
        {
            return $"{Type}|{Id}|{FullName}|{DateOfBirth}|{JoinDate}|{Points}";
        }
        public override Type GetBase()
        {
            return typeof(MultiMember);
        }
    }
}