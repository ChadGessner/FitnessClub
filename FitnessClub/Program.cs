﻿/*
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
data.LoadData();
Console.WriteLine("Welcome to Pizza Hut Gym!");
//check if user is already registered here, if not call CreateMember method below
CreateMember();

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
                //Choose single club method here
                //create single user method here
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


    void CreateMultiMember(string userName, DateTime dateOfBirth)
    {
        MultiMember member = new MultiMember()
        {
            //Id = How do we determine the next ID to use? Do we need to read the whole file into a list first?
            FullName = userName,
            DateOfBirth = dateOfBirth,
            JoinDate = DateTime.Now,

        };
        member.GetUniqueId();
        Console.WriteLine(member.Id);
        data.AddData(member);
    }


    Console.WriteLine("complete");
    Console.WriteLine(data.AllMembers.Count());
}








