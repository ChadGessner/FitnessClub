using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Write
    {
        // Takes objects passed from program.cs,
        // To the DataService() class, which passes them,
        // To this class with a corresponding connection string,
        // and Writer() method writes them to txt,
        // this is going to need good validation in the program.cs,
        // to work properly...
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
