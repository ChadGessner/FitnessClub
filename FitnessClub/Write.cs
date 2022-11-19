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
        // Takes data from DeleteData() method in DataService class,
        // and a temp connection string, opens a temp file and writes,
        // all lines from current applicable .txt file except the line,
        // that matches the string given by the DataToString() method,
        // of that object. Then deletes the current file and moves the temp,
        // file to the path of the original file. The data has already been,
        // removed from memory.
        public void Eraser(IWriteable data, string connectionString, string tempConnectionString)
        {
            FileStream temp = File.Create(tempConnectionString);
            temp.Close();
            var linesToKeep = File.ReadLines(connectionString).Where(l => l != data.DataToString());
            File.WriteAllLines(tempConnectionString, linesToKeep);
            File.Delete(connectionString);
            File.Move(tempConnectionString, connectionString);
        }
    }
}
