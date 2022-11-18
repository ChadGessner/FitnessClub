﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class MultiMember : Members
    {
        public override Types Type { get; set; } = (Types)1;
        //public List<Club> Club { get; set; }

        public override string CheckIn(Club club) => base.CheckIn(club);
        public override string DataToString()
        {
            return $"{Type}|{Id}|{FullName}|{DateOfBirth}|{JoinDate}";
        }
        public override Type GetBase()
        {
            return typeof(MultiMember);
        }
    }
}
