
using FitnessClub;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

DataService data = new DataService();
data.LoadData();


Console.WriteLine("Welcome to Pizza Hut Gym!");

//check if user is already registered here, if not call CreateMember method below

Console.WriteLine("Enter 'new' to create a new user or enter 'view' to see a list of all members.");
string userChoice = Console.ReadLine().ToLower();
switch (userChoice)
{
    case "new":
        CreateMember();
        break;
    case "view":
        ViewMemberList();
        break;
}


void ViewMemberList()
{
    foreach (var member in data.AllMembers)
    {
        Console.WriteLine(member.FullName);
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
        foreach (var memberEntry in data.AllMembers)
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

        //STILL TO FINISH
            //SingleMember member = new SingleMember()
            //{
            //    Id = maxId + 1,
            //    FullName = userName,
            //    DateOfBirth = dateOfBirth,
            //    JoinDate = DateTime.Now,
            //    Club = clubInput
            //};
        //data.AddData(member);
    }


    void CreateMultiMember(string userName, DateTime dateOfBirth)
    {
        int maxId = 0;
        foreach (var memberEntry in data.AllMembers)
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
        data.AddData(member);
    }
  

    

    Console.WriteLine("complete");

}
