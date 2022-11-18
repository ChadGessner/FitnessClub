using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public abstract class Members : IWriteable
    {
        public virtual int Id { get; set; } // normal naming convention instead of ID
        public virtual string FullName { get; set; }
        public virtual DateTime DateOfBirth { get; set; } //date time
        public virtual DateTime JoinDate { get; set; }
        public virtual Types Type { get; set; }
        public Members()
        {

        }
        public virtual string CheckIn(Club club)
        {
            DateTime dateTime = DateTime.Now;
            return $"{DataToString()}|{club.DataToString()}|{dateTime}";
        }
        public virtual string DataToString()
        {
            return $"{Type}|{Id}|{FullName}|{DateOfBirth}|{JoinDate}";
        }
        public virtual Type GetBase()
        {
            return typeof(Members);
        }
    }

}