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
 
using FitnessClub;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


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








=======
string clubOne1 = "Club One|123 Oak Street|400";
string clubTwo2 = "Club Two|133 Oak Street|500";
*/



Console.WriteLine("Welcome to Pizza Hut Gym!");
//check if user is already registered here, if not call CreateMember method below

//CreateMember();
ViewMemberList();

void ViewMemberList()
{
    Console.WriteLine(data.Members.Count());
    foreach (var member in data.Members)
    {
        Console.WriteLine();
    }
}

void CreateMember()
{
    Console.Write("Please enter your name:");
    string userName = Console.ReadLine();
    bool isValidDate = false;
    DateTime dateOfBirth = default(DateTime);
    while (!isValidDate)
    {
        Console.WriteLine("Please enter your date of birth (mm/dd/yyyy):");
        string dateInput = Console.ReadLine();
        if (!Validation.IsDate(dateInput))
        {
            isValidDate = false;
            Console.WriteLine("Please enter a valid date format.");
        }
        else
        {
            isValidDate = true;
            dateOfBirth = DateTime.Parse(dateInput);
            break;
        }
    }
    bool memberTypeValid = false;
    string memberType = "";
    while (!memberTypeValid)
    {
        Console.Write($"Which membership option would you prefer? Enter 'single' for access to one club or 'multi' for access to all clubs.");
        memberType = Console.ReadLine().ToLower();
        switch (memberType)
        {
            case "single":
                memberTypeValid = true;
                CreateSingleMember(userName, dateOfBirth);
                break;
            case "multi":
                memberTypeValid = true;
                CreateMultiMember(userName, dateOfBirth);
                break;
            default:
                memberTypeValid = false;
                Console.WriteLine("That is not a valid input, please try again.");
                break;
        }
    }
    void CreateSingleMember(string userName, DateTime dateOfBirth)
    {
        int maxId = 0;
        foreach (var memberEntry in data.Members)
        {
            if (memberEntry.Id > maxId)
            {
                maxId = memberEntry.Id;
            }
        }
        string clubInput = "";
        Console.WriteLine("Please select a club from the list below");
        foreach (var club in data.Clubs)
        {
            Console.WriteLine(club.Name);
        }
        clubInput = Console.ReadLine().ToLower();

        //Still to do
        //SingleMember member = new SingleMember()
        //{
        //    Id = maxId + 1,
        //    FullName = userName,
        //    DateOfBirth = dateOfBirth,
        //    JoinDate = DateTime.Now,
        //    Club =
        //};
        //data.AddMember(member);
    }



    void CreateMultiMember(string userName, DateTime dateOfBirth)
    {
        int maxId = 0;
        foreach (var memberEntry in data.Members)
        {
            if (memberEntry.Id > maxId)
            {
                maxId = memberEntry.Id;
            }
        }
        MultiMember member = new MultiMember()
        {
            Id = maxId + 1,
            FullName = userName,
            DateOfBirth = dateOfBirth,
            JoinDate = DateTime.Now,
        };
        data.AddMember(member);
    }
  

    

    Console.WriteLine("complete");

}
*/