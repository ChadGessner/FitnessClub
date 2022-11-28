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
        public DateTime DateTime { get; set; }
        public Types Type { get; set; } = Types.checkin;
        public CheckIn(Club club, Members member, DateTime dateTime)
        {
            Club = club;
            Member = member;
            DateTime = dateTime;
        }
        public CheckIn(Club club, Members member, DateTime dateTime, bool menuLoaded)
        {
            Club = club;
            Member = member;
            DateTime = dateTime;
            AddToCurrentPoints();
            //var foo = 
        }
        public string DataToString()
        {// verified that this current points stores to the right location
            return $"{Club.DataToString()}|{Member.DataToString()}|{DateTime}|{Member.CurrentPoints}";
        }
        public int AddToCurrentPoints()
        {
            bool isBday = IsItMemberBday();
            if (isBday)
            {
                Member.CurrentPoints = Member.CurrentPoints + 15;
            }
            else
            {
                Member.CurrentPoints = Member.CurrentPoints + 10;
            }
            return Member.CurrentPoints;
        }

        public bool IsItMemberBday()
        {
            DateTime today = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime shortBdayOfMember = DateTime.Parse(Member.DateOfBirth.ToShortDateString());
            if (today == shortBdayOfMember)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

