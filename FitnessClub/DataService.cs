using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class DataService // Controls flow of data from back to front
    {
        public List<Members> Members { get; set; } = new List<Members>();
        public List<Club> Clubs { get; set; } = new List<Club>();
        public Read Reader { get; set; } = new Read();
        public Write Writer { get; set; } = new Write();
        public void AddMember(Members member)
        {
            Members.Add(member);
            List<IWriteable> data = Members.Select(m => (IWriteable)m).ToList();
            Console.WriteLine(data[0].GetType());
            Writer.Writer(data);
        }
        
        public void ReadData(List<IWriteable> list)
        {
            Reader.ReadData(list);
        }
    }
}
