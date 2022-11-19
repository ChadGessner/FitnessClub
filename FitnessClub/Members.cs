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
        // Added simple Algorithm for getting unique ID,
        // The logic is altered depending on the child class
        public virtual int GetUniqueId()
        {
            int magicNumber = 4;
            string[] birthSplit = DateOfBirth
                .ToString()
                .Split('/', ':')
                .Select(s => new String(s
                .Trim()
                .ToCharArray()
                .Where(c => char
                .IsNumber(c))
                .ToArray()))
                .ToArray();
            string[] joinSplit = JoinDate
                .ToString()
                .Split('/', ':')
                .Select(s => new String(s
                .Trim()
                .ToCharArray()
                .Where(c => char
                .IsNumber(c))
                .ToArray()))
                .ToArray(); 
            for(int i = 0; i < birthSplit.Length; i++)
            {
                Console.Write(birthSplit[i]);
            }
            return Id = joinSplit
                .Select(c => int
                .Parse(c
                .Trim()))
                .Sum() - birthSplit
                .Select(c => int
                .Parse(c
                .Trim()))
                .Sum() + magicNumber;
        }
        // CheckIn() can be changed for implementing Membership Points logic
        public virtual string CheckIn(Club club)
        {
            DateTime dateTime = DateTime.Now;
            return $"{DataToString()}|{club.DataToString()}|{dateTime}";
        }
        // **** DataToString() method can be changed for saving points logic generated,
        // by check in method... 
        // But this will break the DataService() class,
        // So changes will need to be implemented in that class ****
        public virtual string DataToString()
        {
            return $"{Type}|{Id}|{FullName}|{DateOfBirth}|{JoinDate}";
        }
        // GetBase() method Added for dealing with IWriteable,
        // casting and recasting,
        // I don't think we need it anymore...
        public virtual Type GetBase()
        {
            return typeof(Members);
        }
    }

}