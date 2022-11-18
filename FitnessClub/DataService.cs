using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class DataService // Controls flow of data from back to front
    {
        private string singleMemberConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataSingleMember.txt";
        private string multiMemberConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataMultiMembers.txt";
        private string clubsConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataClubs.txt";
        public List<SingleMember> SingleMembers { get; set; } = new List<SingleMember>();
        public List<MultiMember> MultiMembers { get; set; } = new List<MultiMember>();
        public List<Members> AllMembers { get; set; } = new List<Members>();   
        public List<Club> Clubs { get; set; } = new List<Club>();
        public Read Reader { get; set; } = new Read();
        public Write Writer { get; set; } = new Write();
        public DataService()
        {
            
        }
        private string GetConnectionString(Types types)
        {
            switch (types)
            {
                case Types.single:
                    return singleMemberConnectionString;
                case Types.multi:
                    return multiMemberConnectionString;
                case Types.club:
                    return clubsConnectionString;
                default:
                    return "Contact Customer support immediately! 1-800-WAIT-ON-HOLD-FOR-A-LONG-TIME...";
            }
        }
        public void LoadData()
        {
            foreach(Types type in (Types[]) Enum.GetValues(typeof(Types)))
            {
                string connectionString = GetConnectionString(type);
                switch (type)
                {
                    case Types.single:
                        
                        ReadData(ToListIWriteable(SingleMembers), type, connectionString);
                        break;
                    case Types.multi:
                        
                        ReadData(ToListIWriteable(MultiMembers), type, connectionString);
                        break;
                    case Types.club:
                        
                        ReadData(ToListIWriteable(Clubs), type, connectionString);
                        break;
                    default:
                        break;
                }
            }
        }
        public void AddData(IWriteable data)
        {
            switch (data.Type)
            {
                case Types.single:
                    SingleMembers.Add((SingleMember)data);
                    AllMembers.Add((SingleMember)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
                case Types.multi:
                    MultiMembers.Add((MultiMember)data);
                    AllMembers.Add((MultiMember)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
                case Types.club:
                    Clubs.Add((Club)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
            }
            
        }
        private void WriteData(IWriteable data, string connectionString)
        {
            
            Writer.Writer(data, connectionString);
        }
        public void ReadData(List<IWriteable> list, Types types, string connectionString)
        {
            switch (types)
            {
                case Types.single:
                    SingleMembers = Reader.ReadData(list, types, connectionString).Select(m => (SingleMember)m).ToList();
                    break;
                case Types.multi:
                    MultiMembers = Reader.ReadData(list, types, connectionString).Select(m => (MultiMember)m).ToList();
                    break;
                case Types.club:
                    Clubs = Reader.ReadData(list, types, connectionString).Select(c => (Club)c).ToList();
                    break;
            }
            
        }
        private List<IWriteable> ToListIWriteable<T>(List<T> list) where T : class
        {
            return list.Select(i => (IWriteable)i).ToList();
        }
        private bool IsSingleMember(IWriteable data)
        {
            return typeof(SingleMember) == data.GetBase();
        }
        private bool IsMultiMember(IWriteable data)
        {
            return typeof(MultiMember) == data.GetBase();
        }
        private bool IsClub(IWriteable data)
        {
            return typeof(Club) == data.GetBase();
        }
        private bool IsMembers(IWriteable data)
        {
            return data.GetBase() == typeof(Members) || data.GetBase() == typeof(SingleMember) || data.GetBase() == typeof(MultiMember);
        }
    }
}
