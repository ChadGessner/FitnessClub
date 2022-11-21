


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
//foreach (MultiMember multi in data.GetAllMultiMembers())
//{
//    data.AddData(multi.CheckIn(data.GetClubByIndex(3)));
//}
//DateTime date = DateTime.Now;
//Console.WriteLine(data.GetCheckIns().Count);
//Console.WriteLine(date.ToShortDateString());
//Console.WriteLine(data.GetCheckInsByMember(data.GetMemberById(6))[0].DateTime.ToShortDateString());


Console.WriteLine("Welcome to the Pizza Hut Gym Member Management System!");
showMenu();


void showMenu()
{
    Console.WriteLine("Choose from the following options:");
    Console.WriteLine("1 - Check an existing member into a club");
    Console.WriteLine("2 - Create a new member");
    Console.WriteLine("3 - View details of all members");
    Console.WriteLine("4 - Delete an existing member");
    Console.WriteLine("5 - Exit application");

    string userChoice = Console.ReadLine().ToLower();
    switch (userChoice)
    {
        case "1":
            CheckInMember();
            break;
        case "2":
            CreateMember();
            break;
        case "3":
            ViewMemberList();
            showMenu();
            break;
        case "4":
            DeleteMember();
            break;
        case "5":
            Environment.Exit(0);
            break;
    }
}

void CheckInMember()
{
    Console.Write("Enter the ID for the member to check-in, type 'view' to display a list of all members or enter 'menu' to return to the main menu:");
    string userInput = Console.ReadLine().ToLower();
    switch (userInput)
    {
        case "menu":
            showMenu();
            break;
        case "view":
            ViewMemberList();
            CheckInMember();
            break;
        default:
            bool isClubInt = Validation.IsInt(userInput);
            if (isClubInt)
            {
                Console.Write("Enter the club ID number to check in to or type 'view' to display a list of clubs:");
                string clubInput = Console.ReadLine();
                if (clubInput.ToLower() == "view")
                {
                    listClubs();
                    CheckInMember();
                }
                else
                {
                    bool isMemberInt = Validation.IsInt(userInput);
                    if (isMemberInt)
                    {
                        Members member = data.GetMemberById(int.Parse(userInput));
                        Club club = data.GetClubByIndex(int.Parse(clubInput));
                        member.CheckIn(club);
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid club ID. Please try again");
                        CheckInMember();
                        break;
                    }
                }
                ChangesSavedMessage();
                showMenu();
                break;
            }
            else
            {
                Console.WriteLine("That is not a valid userID. Please try again");
                DeleteMember();
                break;
            }
    }
}

void listClubs()
{
    int clubIndex = 1;
    foreach (Club club in data.GetClubs())
    {
        Console.WriteLine($"{clubIndex} - {club.Name}");
        clubIndex++;
    }
}

void ChangesSavedMessage()
{
    Console.WriteLine("All changes saved.");
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
    Console.Write("Enter the ID for the member to delete, type 'view' to display a list of all members or enter 'menu' to return to the main menu:");
    string userInput = Console.ReadLine().ToLower();
    switch (userInput)
    {
        case "menu":
            showMenu();
            break;
        case "view":
            ViewMemberList();
            DeleteMember();
            break;
        default:
            bool isInt = Validation.IsInt(userInput);
            if (isInt)
            {
                Console.WriteLine("Are you sure you wish to delete this member? (y/n)");
                string userConfirm = Console.ReadLine().ToLower();
                if (userConfirm == "y")
                {
                    Members userToDelete = data.GetMemberById(int.Parse(userInput));

                    data.DeleteData(userToDelete);
                    ChangesSavedMessage();
                    showMenu();
                }
                else
                {
                    break;
                }
                break;
            }
            else
            {
                Console.WriteLine("That is not a valid userID. Please try again");
                DeleteMember();
                break;
            }
    }
}

void CreateMember()
{
    Console.Write("New member name:");
    string userName = Console.ReadLine();
    bool isValidDate = false;
    DateTime dateOfBirth = default(DateTime);

    QualifyForDiscount();

    while (!isValidDate)
    {
        Console.WriteLine("New member Date of Birth (mm/dd/yyyy):");
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
        Console.Write($"Enter membership type (single/multi):");
        memberType = Console.ReadLine().ToLower();
        switch (memberType)
        {
            case "single":
                memberTypeValid = true;
                CreateSingleMember(userName, dateOfBirth);
                showMenu();
                break;
            case "multi":
                memberTypeValid = true;
                CreateMultiMember(userName, dateOfBirth);
                showMenu();
                break;
            default:
                memberTypeValid = false;
                Console.WriteLine("That is not a valid input, please try again.");
                break;
        }

    }
}
bool QualifyForDiscount()
{

    DateTime dateStart = new DateTime(2022, 11, 01);
    DateTime dateEnd = new DateTime(2022, 11, 30);
    int discountPercent = 15;

    if (DateTime.Now >= dateStart && DateTime.Now <= dateEnd)
    {
        Console.WriteLine($"This new member qualifies for a discount of {discountPercent}%");
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
    Console.WriteLine("Enter the ID number of desired club from the list below");
    int clubId = 0;
    foreach (Club club in data.GetClubs())
    {
        Console.WriteLine($" {clubId + 1} - {club.Name}");
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
    ChangesSavedMessage();
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
    ChangesSavedMessage();
}










