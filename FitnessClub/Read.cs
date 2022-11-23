using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{

    // Fairly straight forward, Reads data from .txt,
    // and instantiates objects corresponding to the defined type (Types types),
    // in the Method overload, then passes the objects to the DataService() class,
    // which then can be accessed in program.cs,
    // from the DataService() instance in program.cs,
    // using the get methods I will write,
    // for the DataService() class, 
    // maybe didn't need its own class, oh well... Idk DataService() already does a lot...
    
    public class Read
    {

        public List<IWriteable> ReadData(List<IWriteable> readables, Types types, string connectionString)        


        {
            string line = string.Empty;
            using (StreamReader sr = new StreamReader(connectionString))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] rawData = line.Split('|');
                    switch (types)
                    {
                        case Types.single:
                            Club singleClub = new Club(rawData[6], rawData[7], int.Parse(rawData[8]));
                            SingleMember singleMember = new SingleMember(singleClub)
                            {
                                Id = int.Parse(rawData[1]),
                                FullName = rawData[2],
                                DateOfBirth = DateTime.Parse(rawData[3]),
                                JoinDate = DateTime.Parse(rawData[4])
                            };
                            readables.Add(singleMember);
                                
                                 
                            break;
                        case Types.multi:
                            MultiMember multiMember = new MultiMember(int.Parse(rawData[5]))
                            {
                                Id = int.Parse(rawData[1]),
                                FullName = rawData[2],
                                DateOfBirth = DateTime.Parse(rawData[3]),
                                JoinDate = DateTime.Parse(rawData[4]),
                            };
                            readables.Add(multiMember);
                            break;
                        case Types.club:
                            Club justClub = new Club(rawData[1], rawData[2], int.Parse(rawData[3]));
                            readables.Add(justClub);
                            break;
                        case Types.checkin:
                            Club club = new Club(rawData[1], rawData[2], int.Parse(rawData[3]));
                            Members member = rawData[4] == "single" ? new SingleMember(club) : new MultiMember(int.Parse(rawData[9]));
                            DateTime date = member.Type == Types.single ? DateTime.Parse(rawData[13]) : DateTime.Parse(rawData[10]); //read in errored out because the points were not at the end of the line
                            member.Id = int.Parse(rawData[5]);
                            member.FullName = rawData[6];
                            member.DateOfBirth = DateTime.Parse(rawData[7]);
                            member.JoinDate = DateTime.Parse(rawData[8]);
                            
                            CheckIn check = new CheckIn(club, member, date);
                            readables.Add(check);
                            break;
                    }
                }
                sr.Dispose();
                sr.Close();
                return readables;
            }
        }
    }
}