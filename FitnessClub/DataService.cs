using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
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
        private string tempConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\temp.txt";
        private string checkinConnectionString = @"C:\Users\Chad\Source\Repos\FitnessClub\FitnessClub\Data\checkIn.txt";

        //private string singleMemberConnectionString = @"/Users/christinaballard/Documents/GitHub/FitnessClub/FitnessClub/Data/dataSingleMember.txt";
        //private string multiMemberConnectionString = @"/Users/christinaballard/Documents/GitHub/FitnessClub/FitnessClub/Data/dataMultiMembers.txt";
        //private string clubsConnectionString = @"/Users/christinaballard/Documents/GitHub/FitnessClub/FitnessClub/Data/dataClubs.txt";
        //private string tempConnectionString = @"/Users/christinaballard/Documents/GitHub/FitnessClub/FitnessClub/Data/temp.txt";
        //private string checkinConnectionString = @"/Users/christinaballard/Documents/GitHub/FitnessClub/FitnessClub/Data/checkIn.txt";


        /*
        private string singleMemberConnectionString = @"C:\Users\danin\Source\Repos\FitnessClub\FitnessClub\Data\dataSingleMember.txt";
        private string multiMemberConnectionString = @"C:\Users\danin\Source\Repos\FitnessClub\FitnessClub\Data\dataMultiMembers.txt";
        private string clubsConnectionString = @"C:\Users\danin\Source\Repos\FitnessClub\FitnessClub\Data\dataClubs.txt";
        private string tempConnectionString = @"C:\Users\danin\Source\Repos\FitnessClub\FitnessClub\Data\temp.txt";
        private string checkinConnectionString = @"C:\Users\danin\Source\Repos\FitnessClub\FitnessClub\Data\checkIn.txt";
        */
        // ---> **** Change Connection strings to correspond to your local repository **** <---
        private List<SingleMember> SingleMembers { get; set; } = new List<SingleMember>();
        private List<MultiMember> MultiMembers { get; set; } = new List<MultiMember>();
        private List<Members> AllMembers { get; set; } = new List<Members>();   
        private List<Club> Clubs { get; set; } = new List<Club>();
        private List<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
        private Read Reader { get; set; } = new Read();
        private Write Writer { get; set; } = new Write();
        public DataService()
        {
            LoadData();
        }
        // Methods added for querying in memory data
        public List<Club> GetClubs() => Clubs;
        public List<CheckIn> GetCheckIns() => CheckIns;
        public List<CheckIn> GetCheckInsByMember(Members member)
        {
            return CheckIns
                .Where(m => m.Member.FullName == member.FullName && m.Member.JoinDate == member.JoinDate)
                .ToList();
        }
        public List<CheckIn> GetCheckInsByClub(Club club)
        {
            return CheckIns
                .Where(c => c.Club.Name == club.Name && c.Club.Address == club.Address)
                .ToList();
        }
        public List<CheckIn> GetCheckInsByDate(DateTime dateTime)
        {
            return CheckIns
                .Where(c => c.DateTime.ToShortDateString() == dateTime.ToShortDateString())
                .ToList();
        }
        public Club GetClubByIndex(int index)
        {
            return Clubs[index];
        }
        public Club GetClubByName(string name)
        {
            return Clubs
                .SingleOrDefault(c => c.Name
                .ToLower() == name
                .ToLower());
        }
        public SingleMember GetSingleMemberOrDefault(Members member) => GetAllSingleMembers()
            .Where(s => s
            .DataToString() == member
            .DataToString())
            .SingleOrDefault(s => s == member);
        public MultiMember GetMultiMemberOrDefault(Members member) => GetAllMultiMembers()
            .Where(m => m
            .DataToString() == member
            .DataToString())
            .SingleOrDefault(m => m == member);
        
        public List<Members> GetAllMembers() => AllMembers;
        public List<SingleMember> GetAllSingleMembers() => SingleMembers;
        public List<MultiMember> GetAllMultiMembers() => MultiMembers;
        public Members GetMemberById(int id) => AllMembers
            .Single(m => m.Id == id);
        public List<Members> GetMembersByName(string name) => AllMembers
            .Where(m => m.FullName
            .ToLower() == name
            .ToLower()
            .Trim())
            .ToList();
        public List<SingleMember> GetSingleMembersByClub(Club club) => SingleMembers
            .Where(m => m.Club.Name == club.Name)
            .ToList();
        public Members GetMemberByNameAndDateOfBirth(string name, DateTime dob)
        {
            Members member = AllMembers
            .Single(m => m.FullName.ToLower().Trim() == name.ToLower().Trim() && m.DateOfBirth == dob);
            return member.Type == Types.single ? (SingleMember)member : (MultiMember)member;
        }
        public bool CheckIfMemberAlreadyRegistered(Members member)
        {
            try
            {
                GetMemberByNameAndDateOfBirth(member.FullName, member.DateOfBirth);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int GetNextId() => AllMembers
                .Select(m => m.Id)
                .Max() + 1;
        // Algorithm for merging SingleMember() and MultiMember() to one Array for fetch methods etc...
        private void LoadAllMembers()
        {
            AllMembers = SingleMembers
                    .Select(s => (Members)s)
                    .Concat(MultiMembers
                    .Select(m => (Members)m))
                    .OrderBy(m => m.Id)
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
                case Types.checkin:
                    return checkinConnectionString;
                default:
                    return "Contact Customer support immediately! 1-800-WAIT-ON-HOLD-FOR-A-LONG-TIME...";
            }
        }
        // Loads all data from .txt file, this should be the first method called in program.cs after the DataService class is constructed
        private void LoadData()
        {
            foreach (Types type in (Types[])Enum.GetValues(typeof(Types)))
            {
                string connectionString = GetConnectionString(type);
                switch (type)
                {
                    case Types.single:
                        ReadData(ToListIWriteable(SingleMembers), type, connectionString);
                        break;
                    case Types.multi:
                        ReadData(ToListIWriteable(MultiMembers), type, connectionString);
                        LoadAllMembers();
                        break;
                    case Types.club:

                        ReadData(ToListIWriteable(Clubs), type, connectionString);
                        break;
                    case Types.checkin:
                        ReadData(ToListIWriteable(CheckIns), type, connectionString);
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
                    LoadAllMembers();
                    WriteData(data, GetConnectionString(data.Type));
                    break;
                case Types.multi:
                    MultiMembers.Add((MultiMember)data);
                    LoadAllMembers();
                    WriteData(data, GetConnectionString(data.Type));
                    break;
                case Types.club:
                    Clubs.Add((Club)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
                case Types.checkin:
                    CheckIns.Add((CheckIn)data);
                    WriteData(data, GetConnectionString(data.Type));
                    break;
            }
        }
        // Helper method for above AddData() Method, I may simplify this later...
        private void WriteData(IWriteable data, string connectionString)
        {
            Writer.Writer(data, connectionString);
        }
        public void DeleteData(IWriteable data)
        {
            switch (data.Type)
            {
                case Types.single:
                    SingleMembers.Remove((SingleMember)data);
                    LoadAllMembers();
                    break;
                case Types.multi:
                    MultiMembers.Remove((MultiMember)data);
                    LoadAllMembers();
                    break;
                case Types.club:
                    Clubs.Remove((Club)data);
                    break;
                case Types.checkin:
                    CheckIns.Remove((CheckIn)data);
                    break;
            }
            Writer.Eraser(data, GetConnectionString(data.Type), tempConnectionString);
            
        }
        // ReadData() method works with Load data, recasts the IWriteable into the proper return type SingleMember(), MultiMember() or Club()
        private void ReadData(List<IWriteable> list, Types types, string connectionString)
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
                case Types.checkin:
                    CheckIns = Reader.ReadData(list, types, connectionString).Select(ch => (CheckIn)ch).ToList();
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
        // the SingleMembers(), MultiMembers(), Club() and CheckIn() classes with thier,
        // own Type enum value, this acts as a pointer to their type for
        // the data service class for casting to and from IWriteable...
        //private bool IsSingleMember(IWriteable data)
        //{
        //    return typeof(SingleMember) == data.GetBase();
        //}
        //private bool IsMultiMember(IWriteable data)
        //{
        //    return typeof(MultiMember) == data.GetBase();
        //}
        //private bool IsClub(IWriteable data)
        //{
        //    return typeof(Club) == data.GetBase();
        //}
        //private bool IsMembers(IWriteable data)
        //{
        //    return data.GetBase() == typeof(Members) || data.GetBase() == typeof(SingleMember) || data.GetBase() == typeof(MultiMember);
        //}
    }
}