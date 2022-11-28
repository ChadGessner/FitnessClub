

using FitnessClub;
using System;
using System.Dynamic;
using System.Linq;
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
	data.GetSingleMembersByClub(Club club) <-- Returns a list of all SingleMembers who belong to the given club
	data.GetMemberById(int id) <--  Returns the Member from AllMembers with corresponding Id value, will throw exception if Member with that id does not exist, needs a validation
	data.GetMembersByName(string name) <-- Returns ***List of Members*** with matching name
	data.GetMemberByNameAndDateOfBirth(string name, DateTime dob) <-- Returns member from AllMembers and casts it appropriately, throws exception if member does not exist
	data.CheckIfMemberAlreadyRegistered(Members member) <-- Returns true if the Member exists else false
	data.GetNextId() <-- Returns Max Id + 1
	data.GetSingleMemberOrDefault(member) <-- returns SingleMember() if the member is a SingleMember() else throws exception
	data.GetMultiMemberOrDefault(member) <-- returns MultiMember() if the member is a MultiMember() else throws exception 
	
	
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
void ViewInvoice()
{
	Members user;
	SingleMember sm;
	MultiMember mm;
	Console.WriteLine("Please select member ID: ");
	string userChoice = Console.ReadLine().ToLower();
	
	var memberResult = data.GetAllMembers();
	try
	{
		data.GetMemberById(int.Parse(userChoice));
	}
	catch
	{
		Console.Clear();
		Console.WriteLine("That is not a valid member Id, here is a list of all members with Id's...\n");
		ViewMemberList();
		ViewInvoice();
	}
	user = data.GetMemberById(int.Parse(userChoice));
	bool isSingle = user.Type == Types.single;
	sm = isSingle ? data.GetSingleMemberOrDefault(user) : null;
	mm = !isSingle ? data.GetMultiMemberOrDefault(user) : null;
	int baseFee = isSingle ? sm.Club.BaseFee : mm.BaseMultiMemberFee;
	string points = isSingle ? "N/A" : mm.Points.ToString();



	var clubResult = data.GetClubs();
	if (!isSingle)
	{
		Console.WriteLine($"{"ID",-5} {"Name",-15} {"Current Points",-15} {"Fee",-11}");
		Console.WriteLine($"{"-----",-5} {"---------------",-15} {"--------------",-15} {"-----------",-11}");
		Console.WriteLine($"{mm.Id,-5} {mm.FullName,-15} {mm.Points,-15} {baseFee,-11}");
	}
	else
	{
		Console.WriteLine($"{"ID",-5} {"Name",-15} {"Current Points",-15} {"Fee",-11}");
		Console.WriteLine($"{"-----",-5} {"---------------",-15} {"--------------",-15} {"-----------",-11}");
		Console.WriteLine($"{sm.Id,-5} {sm.FullName,-15} {points,-15} {baseFee,-11}");
	}
	showMenu();
}

void showMenu()
{

    Console.WriteLine("Choose from the following options:");
    Console.WriteLine("1 - Check an existing member into a club");
    Console.WriteLine("2 - Create a new member");
    Console.WriteLine("3 - View details of all members");
    Console.WriteLine("4 - Delete an existing member");
    Console.WriteLine("5 - View member invoice");
    Console.WriteLine("6 - Exit application");

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
            ViewInvoice();
            break;
        case "6":
            Environment.Exit(0);
            break;
    }

}
void DisplayClubMembers(Club club)
{
	foreach (SingleMember singleMember in data.GetSingleMembersByClub(club))
	{
		Console.WriteLine($"Id: {singleMember.Id} Name: {singleMember.FullName} Join Date: {singleMember.JoinDate}");
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
			try
			{
				data.GetMemberById(int.Parse(userInput));
			}
			catch
			{
				Console.Clear();
				Console.WriteLine($"{userInput} is not a valid member ID, try again...");
				CheckInMember();
			}
			bool isClubInt = Validation.IsInt(userInput);
			if (isClubInt)
			{
				
				Console.WriteLine("Enter the club ID number to check in to:");
				listClubs();
				string clubInput = Console.ReadLine();
				bool isMemberInt = Validation.IsInt(userInput);
				if (isMemberInt)
				{


					Members member = data.GetMemberById(int.Parse(userInput));
					try
					{
						 data.GetClubByIndex(int.Parse(clubInput) - 1);
					}
					catch
					{
						Console.Clear();
						Console.WriteLine($"{clubInput} is not a valid club Id, try again...");
						CheckInMember();
					}
					Club club = data.GetClubByIndex(int.Parse(clubInput) - 1);
					try
					{
						data.AddData(member.CheckIn(club));
						data.UpdateData(member);
					}
					catch
					{
						//DisplayClubMembers(club); <--not sure this added anything to this process
						Console.WriteLine("You do not appear to be a member of this club. Would you like to upgrade your membership to allow access to multiple clubs? (y/n)");
						string userChoice = Console.ReadLine().ToLower();
						switch (userChoice)
						{
							case "y":
								Members userToChange = data.GetMemberById(int.Parse(userInput));
								int userId = userToChange.Id;
								string userName = userToChange.FullName;
								DateTime dateOfBirth = userToChange.DateOfBirth;
								DateTime joinDate = userToChange.JoinDate;
								data.DeleteData(userToChange);
								CreateMultiMember(userId, userName, dateOfBirth, joinDate);
								Console.WriteLine("Member status changed to multi-club");
								Console.WriteLine("Check-in successful");
								showMenu();
								break;
							default:
								Console.WriteLine("Check-in unsuccessful");
								showMenu();
								break;
						}

					}
				}
				else
				{
					Console.WriteLine("That is not a valid club ID. Please try again");
					CheckInMember();
					break;
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

void AddPoints(Members _member)
{
    try
    {
       // _member.CurrentPoints=
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error in AddPoints()" + ex.ToString());
    }
} 



void ViewMemberList()
{
	string clubName = "";
	string clubID = "";
	int nameLength = 0;

	Console.WriteLine($"{"ID",-5} {"Name",-20} {"Date of Birth",-15} {"Join Date",-15} {"Member Type",-11} {"Assigned Club",-15}");
	Console.WriteLine($"{"-----",-5} {"--------------------",-20} {"--------------",-15} {"--------------",-15} {"-----------",-11} {"--------------",-15}");
	foreach (var member in data.GetAllMembers())
	{
		try
		{
			clubName = data.GetSingleMemberOrDefault(member).Club.Name;
		}
		catch
		{
			clubName = "";
		}
		
		Console.WriteLine($"{member.Id,-5} {member.FullName,-20} {member.DateOfBirth.ToString("MM/dd/yyyy"),-15} {member.JoinDate.ToString("MM/dd/yyyy"),-15} {member.Type,-11} {clubName, -15}");
	}
	Console.WriteLine();
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
					try
					{
						Members userToDelete = data.GetMemberById(int.Parse(userInput));

						data.DeleteData(userToDelete);
						ChangesSavedMessage();
						showMenu();
					}
					catch
					{
						Console.Clear();
						Console.WriteLine($"{userInput} isn't a valid member Id, try again!");
						DeleteMember();
					}
					
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
	int userId = data.GetNextId();
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
	DateTime joinDate = DateTime.Now;
	while (!memberTypeValid)
	{
		Console.Write($"Enter membership type (single/multi):");
		memberType = Console.ReadLine().ToLower();
		switch (memberType)
		{
			case "single":
				memberTypeValid = true;
				CreateSingleMember(userId, userName, dateOfBirth);
				showMenu();
				break;
			case "multi":
				memberTypeValid = true;
				CreateMultiMember(userId, userName, dateOfBirth, joinDate);
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

void CreateSingleMember(int userId, string userName, DateTime dateOfBirth)
{
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
		Id = userId,
		FullName = userName,
		DateOfBirth = dateOfBirth,
		JoinDate = DateTime.Now,
		Club = selectedClub
	};
	data.AddData(member);
	ChangesSavedMessage();
}

void CreateMultiMember(int userId, string userName, DateTime dateOfBirth, DateTime joinDate)
{
	MultiMember member = new MultiMember(20)

	{
		Id = userId,
		FullName = userName,
		DateOfBirth = dateOfBirth,
		JoinDate = joinDate,
	};
	data.AddData(member);
	ChangesSavedMessage();
}

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