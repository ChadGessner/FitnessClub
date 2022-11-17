using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Club : IWriteable
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int BaseFee { get; set; }
        public List<Members> ClubMembers { get; set; }
        public Types Type { get; set; } = (Types)2;
        public Club(string name, string address, int baseFee)
        {
            Name = name;
            Address = address;
            BaseFee = baseFee;
        }
        public string DataToString()
        {
            return $"{Type}|{Name}|{Address}|{BaseFee}";
        }

    }
}


// create list of 4 clubs, plus a multi option

