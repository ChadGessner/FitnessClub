


using FitnessClub;
using System.Dynamic;
using System.Net.Http.Headers;
// ---> **** Change Connection strings in DataService to correspond to your local repository **** <---
DataService data = new DataService();

// ---> **** Change Connection strings in DataService to correspond to your local repository **** <---
// data.LoadData() is now private and located in the DataService constructor so it never needs to be called again <--- Loads all data from Db
/*
    *** Data Query Methods ***
    data.GetClubs() <-- Returns list of all clubs
    data.GetCheckIns() <-- Returns List of all CheckIn objects
    data.GetCheckInsByMember(Members member) <-- Returns all CheckIn objects associate with that member, casts accordingly
    data.GetCheckInsByClub(Club club) <-- Returns all CheckIn objects associated with that club
    data.GetCheckinsByDate(DateTime dateTime) <-- Returns all CheckIn objects according to the Date, technically works but wont be very usefull yet
    data.GetClubByIndex(int index) <-- Returns single club within data.Clubs with that index
    data.GetClubByName(string name) <-- Returns single Club object from Clubs or an empty Club instance, will need to be validated or it will break the application
    data.GetAllMembers()  <-- Returns list of all Members
    data.GetAllSingleMembers() <-- Returns list of all SingleMembers
    data.GetAllMultiMembers() <-- Returns list of all MultiMembers
    data.GetMemberById(int id) <--  Returns the Member from AllMembers with corresponding Id value, will throw exception if Member with that id does not exist, needs a validation
    data.GetMembersByName(string name) <-- Returns ***List of Members*** with matching name
    data.GetMemberByNameAndDateOfBirth(string name, DateTime dob) <-- Returns member from AllMembers and casts it appropriately, throws exception if member does not exist
    data.CheckIfMemberAlreadyRegistered(Members member) <-- Returns true if the Member exists else false
    data.GetNextId() <-- Returns Max Id + 1
     
    
    
    *** Methods for database operation ***
    --> All these methods take an instance of any class that implements IWriteable <--
    --> This includes Club(), Members(), SingleMember(), MultiMember() and CheckIn() <--
    data.AddData(IWriteable data) <-- adds object to in memory datalists, then overwrites .txt with the appended list
    data.DeleteData(IWriteable data) <-- deletes object from memory, then overwrietes .txt file with the altered list
 */
foreach (MultiMember multi in data.GetAllMultiMembers())
{
    data.AddData(multi.CheckIn(data.GetClubByIndex(3)));
}
DateTime date = DateTime.Now;
Console.WriteLine(data.GetCheckIns().Count);
Console.WriteLine(date.ToShortDateString());
Console.WriteLine(data.GetCheckInsByMember(data.GetMemberById(6))[0].DateTime.ToShortDateString());


Console.WriteLine("Welcome to Pizza Hut Gym!");

//check if user is already registered here, if not call CreateMember method below

Console.WriteLine("What would you like to do today?");
Console.WriteLine("Enter 'check' to check a member into a club");
Console.WriteLine("Enter 'new' to create a new member");
Console.WriteLine("Enter 'view' to view all members");
Console.WriteLine("Enter 'delete' to delete a member");

string userChoice = Console.ReadLine().ToLower();
switch (userChoice)
{
    case "check":
    //call check-in method here
    case "new":
        CreateMember();
        break;
    case "view":
        ViewMemberList();
        break;
    case "delete":
        DeleteMember();
        break;
}


void ViewMemberList()
{
    Console.WriteLine($"{"ID",-5} {"Name",-15} {"Date of Birth",-15} {"Join Date",-15} {"Member Type",-11}");
    Console.WriteLine($"{"-----",-5} {"---------------",-15} {"--------------",-15} {"--------------",-15} {"-----------",-11}");
    foreach (var member in data.GetAllMembers())

    {
        Console.WriteLine($"{member.Id,-5} {member.FullName,-15} {member.DateOfBirth.ToString("MM/dd/yyyy"),-15} {member.JoinDate.ToString("MM/dd/yyyy"),-15} {member.Type,-10}");
    }
}

void DeleteMember()
{
    Console.Write("Enter the ID for the member to delete, type 'view' to display a list of all members:");
    string userInput = Console.ReadLine().ToLower();
    switch (userInput)
    {
        case "view":
            ViewMemberList();
            Console.Write("Enter the ID for the member to delete:");
            break;
        default:
            //will need validation to check if INT has been entered.
            Console.WriteLine("Are you sure you wish to delete this member? (y/n)");
            string userConfirm = Console.ReadLine().ToLower();
            if (userConfirm == "y")
            {
                // will need to remove member from List, clear txt file and re-write List here
            }
            else
            {
                break;
            }
            break;
    }
}

void CreateMember()
{
    Console.Write("Please enter your name:");
    string userName = Console.ReadLine();
    bool isValidDate = false;
    DateTime dateOfBirth = default(DateTime);

    QualifyForDiscount();

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

    bool QualifyForDiscount()
    {

        DateTime dateStart = new DateTime(2022, 11, 01);
        DateTime dateEnd = new DateTime(2022, 11, 30);
        int discountPercent = 15;

        if (DateTime.Now >= dateStart && DateTime.Now <= dateEnd)
        {
            Console.WriteLine($"Congratulations, we are currently offering a discount for signing up this month. Save {discountPercent}%");
            return true;
        }
        else return false;
    }

    void CreateSingleMember(string userName, DateTime dateOfBirth)
    {
        int maxId = 0;

        foreach (var memberEntry in data.GetAllMembers())

        {
            if (memberEntry.Id > maxId)
            {
                maxId = memberEntry.Id;
            }
        }
        string clubInput = "";
        Console.WriteLine("Please enter the ID number of desired club from the list below");
        int clubId = 0;
        foreach (Club club in data.GetClubs())
        {
            Console.WriteLine($" {clubId} - {club.Name}");
            clubId++;
        }

        clubInput = Console.ReadLine().ToLower();
        Club selectedClub = data.GetClubs()[int.Parse(clubInput) - 1];

        SingleMember member = new SingleMember(selectedClub)
        {
            Id = maxId + 1,
            FullName = userName,
            DateOfBirth = dateOfBirth,
            JoinDate = DateTime.Now,
            Club = selectedClub
        };
        data.AddData(member);
    }

    void CreateMultiMember(string userName, DateTime dateOfBirth)
    {
        int maxId = data.GetNextId();
        foreach (var memberEntry in data.GetAllMembers())

        {
            if (memberEntry.Id > maxId)
            {
                maxId = memberEntry.Id;
            }
        }
        MultiMember member = new MultiMember()
        {
            Id = maxId,
            FullName = userName,
            DateOfBirth = dateOfBirth,
            JoinDate = DateTime.Now,
        };
        data.AddData(member);
    }




    Console.WriteLine("All chamnges saved.");


}

