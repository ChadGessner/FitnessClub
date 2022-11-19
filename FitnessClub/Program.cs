


using FitnessClub;
// ---> **** Change Connection strings in DataService to correspond to your local repository **** <---
DataService data = new DataService();
// ---> **** Change Connection strings in DataService to correspond to your local repository **** <---
data.LoadData(); // <--- Loads all data from Db
/*
    *** Data Query Methods ***
    data.GetClubs() <-- Returns list of all clubs
    data.GetClubByIndex(int index) <-- Returns single club within data.Clubs with that index
    data.GetClubByName(string name) <-- Returns single Club object from Clubs or an empty Club instance, will need to be validated or it will break the application
    data.GetAllMembers()  <-- Returns list of all Members
    data.GetAllSingleMembers() <-- Returns list of all SingleMembers
    data.GetAllMultiMembers() <-- Returns list of all MultiMembers
    data.GetMemberByNameAndDateOfBirth(string name, DateTime dob) <-- Returns member from AllMembers and casts it appropriately, throws exception if member does not exist
    data.CheckIfMemberAlreadyRegistered(Members member) <-- Returns true if the Member exists else false
    *** Method for adding Data ***
    data.AddData()
 */



Console.WriteLine("Welcome to Pizza Hut Gym!");

//check if user is already registered here, if not call CreateMember method below

Console.WriteLine("Enter 'check' to check into a gym, enter 'new' to create a new user or enter 'view' to see a list of all members.");
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

        DateTime dateStart = new DateTime (2022, 11, 01);
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
        Console.WriteLine("Please select a club from the list below");

        foreach (Club club in data.GetClubs())
        {
            Console.WriteLine(club.Name);
        }

        clubInput = Console.ReadLine().ToLower();
        Club selectedClub = data.GetClubs()[int.Parse(clubInput) - 1];

            SingleMember member = new SingleMember(selectedClub)
            {
                Id = maxId + 1,
                FullName = userName,
                DateOfBirth = dateOfBirth,
                JoinDate = DateTime.Now,
                //Club = clubInput
            };
        data.AddData(member);

    }


    void CreateMultiMember(string userName, DateTime dateOfBirth)
    {
        int maxId = 0;

        foreach (var memberEntry in data.GetAllMembers())

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

