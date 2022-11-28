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
        public int BirthdayBonus { get;private set; }
        public int BaseMultiMemberFee { get; private set; } = 500;
        public MultiMember(int points)
        {
            Points = points;
            BirthdayBonus = BdayPointsModifier();
        }
        private int BdayPointsModifier()
        {
            DateTime today = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime shortBday = DateTime.Parse(DateOfBirth.ToShortDateString());
            DateTime joinDate = DateTime.Parse(JoinDate.ToShortDateString());
            DateTime bDayAfterJoin = new(joinDate.Year, shortBday.Month, shortBday.Day);
            TimeSpan difference = bDayAfterJoin - today;
            return difference.Days >= 0 ? 15 + (bDayAfterJoin.Year - today.Year) * 15: 0;
        }
        public override CheckIn CheckIn(Club club)
        {
            Points += 10;
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