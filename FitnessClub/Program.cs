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

DataService data = new DataService();
Club clubOne = new Club("Club One", "123 Oak Street", 400);
Club clubTwo = new Club("Club Two", "133 Oak Street", 500);
List<Members> members = new()
{
    new SingleMember(clubOne)
    {
        Id = 1,
        FullName = "Timmy",
        DateOfBirth = DateTime.Now,
        JoinDate = DateTime.Now,
    },
    new SingleMember(clubTwo)
    {
        Id = 2,
        FullName = "Timmy",
        DateOfBirth = DateTime.Now,
        JoinDate = DateTime.Now,
    },
    new SingleMember(clubTwo)
    {
        Id = 3,
        FullName = "Timmy",
        DateOfBirth = DateTime.Now,
        JoinDate = DateTime.Now,
    },
    new SingleMember(clubOne)
    {
        Id = 4,
        FullName = "Timmy",
        DateOfBirth = DateTime.Now,
        JoinDate = DateTime.Now,
    }
};
foreach (SingleMember member in members)
{
    data.AddMember(member);
}

string clubOne1 = "Club One|123 Oak Street|400";
string clubTwo2 = "Club Two|133 Oak Street|500";


Console.WriteLine("Welcome to Pizza Hut Gym!");

