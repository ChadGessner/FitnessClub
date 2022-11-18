using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Write
    {
        //private string clubConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataClubs.txt";
        //private string membersConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataMembers.txt";

        public void Writer(IWriteable data, string connectionString)
        {
            using (StreamWriter sw = new StreamWriter(connectionString, true))
            {
                sw.WriteLine(data.DataToString());
                sw.Flush();
                sw.Close();
            }
            
        }

    }
}
