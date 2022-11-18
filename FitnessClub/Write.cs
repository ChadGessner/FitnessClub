using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Write
    {


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