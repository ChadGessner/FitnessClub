using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Read
    {
        private string membersConnectionString = @"dataMembers.txt";
        private string clubsConnectionString = @"dataClubs.txt";
        //private string membersConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataMembers.txt";
        //private string clubsConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataClubs.txt";

        public List<IWriteable> ReadData(List<IWriteable> readables)
        {
            string connectionString = typeof(Club) == readables[0].GetType() ? clubsConnectionString : membersConnectionString;
            string line = string.Empty;
            using (StreamReader sr = new StreamReader(connectionString))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] rawData = line.Split('|');
                    switch (rawData[0])
                    {
                        case "single":
                            readables.Add(
                                new SingleMember(
                                    new Club(rawData[6], rawData[7], int.Parse(rawData[8])))
                                {
                                    Id = int.Parse(rawData[1]),
                                    FullName = rawData[2],
                                    DateOfBirth = DateTime.Parse(rawData[3]),
                                    JoinDate = DateTime.Parse(rawData[4])
                                });
                            break;
                        case "multi":
                            readables.Add(new MultiMember()
                            {
                                Id = int.Parse(rawData[1]),
                                FullName = rawData[2],
                                DateOfBirth = DateTime.Parse(rawData[3]),
                                JoinDate = DateTime.Parse(rawData[4])
                            });
                            break;
                        case $"club":
                            readables.Add(new Club(rawData[1], rawData[2], int.Parse(rawData[3])));
                            break;
                    }
                }
                sr.Close();
                return readables;
            }
        }
    }
}
