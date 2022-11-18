/*
 Allow users to:
Add members (both kinds), remove members or display member information.
Check a particular member in at a particular club. (Call the CheckIn method). Display a friendly error message if there is an exception. Don’t let it crash the program.
Select a member and generate a bill of fees. Include membership points for Multi-Club Members.
A main class which takes input from the user:
Asks a user if they want to select a club
Added members should be given the option to select from at least 4 fitness center locations or have the option to be a multi-club member.
Optional enhancements:
(Easy/Medium) Allow new members to receive discounts if they sign up during certain time periods, explore the DateTime library for help with date and time.
(Medium) Store clubs and members in text files.
(Hard) Out Pizza the hut 
 */
using FitnessClub;
Console.WriteLine("Welcome to Pizza Hut Gym!");
DataService data = new DataService();
//List<Members> members = new()
//{
//    new MultiMember()
//    {
//        Id = 1,
//        FullName = "Sam",
//        DateOfBirth = DateTime.Now,
//        JoinDate = DateTime.Now,
//    },
//    new MultiMember()
//    {
//        Id = 2,
//        FullName = "Sal",
//        DateOfBirth = DateTime.Now,
//        JoinDate = DateTime.Now,
//    },
//    new MultiMember()
//    {
//        Id = 3,
//        FullName = "Sara",
//        DateOfBirth = DateTime.Now,
//        JoinDate = DateTime.Now,
//    },
//    new MultiMember()
//    {
//        Id = 4,
//        FullName = "Scott",
//        DateOfBirth = DateTime.Now,
//        JoinDate = DateTime.Now,
//    }
//};


//List<Club> clubList = new List<Club>
//{
//    new Club("Club One", "123 Oak Street", 400),
//    new Club("Club Two", "222 Oak Street", 300),
//    new Club("Club Tree", "333 Tree Street", 333),
//    new Club("Four of Club", "444 Fourth Street", 444)
//};
//foreach (Club club in clubList)
//{
//    data.AddData(club);
//}
//foreach (Members member in members)
//{
//    data.AddData(member);
//}




data.LoadData();
foreach (var member in data.AllMembers)
{
    Console.WriteLine(member.FullName);
}
foreach (var club in data.Clubs)
{
    Console.WriteLine(club.Name);
}
foreach(var member in data.SingleMembers)
{
    Console.WriteLine(member.FullName);
}
foreach(var member in data.MultiMembers)
{
    Console.WriteLine(member.FullName);
}
Console.WriteLine(data.Clubs.Count + " " + data.AllMembers.Count + " " + data.MultiMembers.Count + " " + data.SingleMembers.Count);
Club clubFive = new Club("Club Five", "12345 Oak Street", 420);
SingleMember dan = new(clubFive)
{
    FullName = "Dan",
    DateOfBirth = DateTime.Now,
    JoinDate = DateTime.Now,
};
data.AddData(clubFive);
Console.WriteLine(data.Clubs.Count);








