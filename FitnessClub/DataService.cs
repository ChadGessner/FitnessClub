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
        // ---> **** Change Connection strings to correspond to your local repository **** <---
        private string singleMemberConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataSingleMember.txt";
        private string multiMemberConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataMultiMembers.txt";
        private string clubsConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\dataClubs.txt";
        // ---> **** Change Connection strings to correspond to your local repository **** <---
        public List<SingleMember> SingleMembers { get; set; } = new List<SingleMember>();
        public List<MultiMember> MultiMembers { get; set; } = new List<MultiMember>();
        public List<Members> AllMembers { get; set; } = new List<Members>();   
        public List<Club> Clubs { get; set; } = new List<Club>();
        public Read Reader { get; set; } = new Read();
        public Write Writer { get; set; } = new Write();
        public DataService()
        {
            
        }
        // Algorithm for merging SingleMember() and MultiMember() to one Array for fetch methods etc...
        private void GetAllMembers()
        {
            AllMembers = SingleMembers
                    .Select(s => (Members)s)
                    .Concat(MultiMembers
                    .Select(m => (Members)m))
                    .ToList();
        }
        // Gets proper connection string according to type
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
        // Loads all data from .txt file, this should be the first method called in program.cs after the DataService class is constructed
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
        // Method for adding data, first loads it in local memory, the lists at the top,
        // then overwrites the corresponding text file with the new data,
        // then it calls the GetAllMembers() method to update that in memory list if necessary
        // ---> *** we need good validation on the front end for this to work properly *** <---
        public void AddData(IWriteable data) // <--- *** we need good validation on the front end for this to work properly *** <---
        {
            switch (data.Type)
            {
                case Types.single:
                    SingleMembers.Add((SingleMember)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
                case Types.multi:
                    MultiMembers.Add((MultiMember)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
                case Types.club:
                    Clubs.Add((Club)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
            }
            GetAllMembers();
        }
        // Helper method for above AddData() Method, I may simplify this later...
        private void WriteData(IWriteable data, string connectionString)
        {
            Writer.Writer(data, connectionString);
        }
        // I will write DeleteData() today...
        public void DeleteData()
        {

        }
        // ReadData() method works with Load data, recasts the IWriteable into the proper return type SingleMember(), MultiMember() or Club()
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
        // ToListIWriteable() method casts list from SingleMember(), MultiMember() or Club() to IWriteable for the read and write methods...
        // Something something polymorphism, something something...
        private List<IWriteable> ToListIWriteable<T>(List<T> list) where T : class
        {
            return list.Select(i => (IWriteable)i).ToList();
        }
        // All Helper methods I didn't need,
        // this is due to using the Types enum and initializing,
        // the SingleMembers(), MultiMembers() and Club() classes with thier,
        // own Type enum value, this acts as a pointer to their type for
        // the data service class for casting to and from IWriteable...
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
