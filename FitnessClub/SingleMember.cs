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
        public SingleMember(Club club)
        {
            Club = club;
        }
        // see parent for notes...
        public override CheckIn CheckIn(Club club)
        {
            if(club.Name == Club.Name)
            {
                return new CheckIn(Club, this, DateTime.Now, true); //this is where the currentDateTime gets set for checkIn()
            }
            throw new Exception("You don't belong to this club bruh...");
        }
       

        public override string DataToString()
        {// taking off the last |
            return $"{Type}|{Id}|{FullName}|{DateOfBirth}|{JoinDate}|{Club.DataToString()}";
        }
        public override Type GetBase()
        {
            return typeof(SingleMember);
        }
    }
}