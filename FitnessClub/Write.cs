using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Write
    {
        private string clubConnectionString = @"dataClubs.txt";
        private string membersConnectionString = @"dataMembers.txt";
        //private string clubConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataClubs.txt";
        //private string membersConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataMembers.txt";

        public void Writer(List<IWriteable> writeables)
        {
            string connectionString = typeof(Members) == writeables[0].GetType() || typeof(SingleMember) == writeables[0].GetType() ? membersConnectionString : clubConnectionString;
            using (StreamWriter sw = new StreamWriter(connectionString))
            {
                foreach (IWriteable writeable in writeables)
                {
                    sw.WriteLine(writeable.DataToString());
                }
            }
        }

    }
}
