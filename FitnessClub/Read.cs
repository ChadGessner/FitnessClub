using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Read
    {


        public List<IWriteable> ReadData(List<IWriteable> readables)

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
                        case Types.multi:
                            readables.Add(new MultiMember()
                            {
                                Id = int.Parse(rawData[1]),
                                FullName = rawData[2],
                                DateOfBirth = DateTime.Parse(rawData[3]),
                                JoinDate = DateTime.Parse(rawData[4])
                            });
                            break;
                        case Types.club:
                            readables.Add(new Club(rawData[1], rawData[2], int.Parse(rawData[3])));
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
