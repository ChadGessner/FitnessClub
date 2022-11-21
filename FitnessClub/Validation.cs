using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    
    public static class Validation
    {
        public static bool IsDate(string userInput)
        {
            DateTime dateInput;
            try
            {
                dateInput = DateTime.Parse(userInput);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsInt(string userInput)
        {
            int intInput;
            try
            {
                intInput = int.Parse(userInput);
                return true;
            }        
            catch (FormatException)
            {
                return false;
            }
        }
    }
    
}
