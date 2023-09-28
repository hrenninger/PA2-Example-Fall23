//start main here
int userChoice = GetUserChoice(); 

while(userChoice != 3){
    Route(userChoice);
    userChoice = GetUserChoice();
}

//end main


//menu methods
static int GetUserChoice(){
    DisplayMenu(); 
    string userChoice = Console.ReadLine();
    if (IsValidChoice(userChoice)) {
        return int.Parse(userChoice);
    }
    else return 0;
}

static void DisplayMenu(){
    Console.Clear();
    Console.WriteLine("Enter 1 to display compass\nEnter 2 to calculate park fees\nEnter 3 to exit");
}

static bool IsValidChoice(string userChoice) {
    if (userChoice == "1" || userChoice == "2" || userChoice == "3") {
        return true;
    }
    return false;
}

static void Route(int userChoice) {
    if (userChoice == 1){
        GetCompass();
    }
    else if (userChoice == 2){
        GetParkFees();
    }
    else if (userChoice != 3){
        SayInvalid();
        PauseAction();
    }
}

//error handling
static void SayInvalid(){
    Console.WriteLine("Invalid. Please enter \"1\", \"2\", or \"3\"");
}

static void PauseAction(){
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}


//compass methods
static void GetCompass(){
    int userChoice = GetUserDirection(); 
    int rightCount = 0;
    int leftCount = 0;
    while(userChoice != 3){
        Directions(userChoice, ref rightCount, ref leftCount);
        userChoice = GetUserDirection();
    }
    FinalDirection(rightCount, leftCount);
}

static int GetUserDirection(){
    DirectionMenu(); 
    string userChoice = Console.ReadLine();
    if (IsValidChoice(userChoice)) {
        return int.Parse(userChoice);
    }
    else return 0;
}

static void DirectionMenu(){
    Console.Clear(); //facing north at the start
    Console.WriteLine("Enter 1 for a RIGHT turn\nEnter 2 for a LEFT turn\nEnter 3 to finish");
}

static void Directions(int userChoice, ref int rightCount, ref int leftCount) {
    if (userChoice == 1){
        rightCount++;
    }
    else if (userChoice == 2){
        leftCount++;
    }
    else if (userChoice != 3){
        SayInvalid();
        PauseAction();
    }
}

static void FinalDirection(int rightCount, int leftCount){
    int difference = rightCount - leftCount;
    int remainder = difference%4;
    string direction = "";
    if (remainder == 0){
        direction = "North";
    }
    else if (remainder == 1 || remainder == -3){
        direction = "East";
    }
    else if (remainder == 2 || remainder == -2){
        direction = "South";
    }
    else if (remainder == 3 || remainder == -1){
        direction = "West";
    }
    //Console.WriteLine(remainder);
    Console.WriteLine($"You are facing {direction}!");
    PauseAction();
}

//park fee methods
static void GetParkFees(){
    int vehicleExpense = GetVehicle();
    int attendees = GetAttendees();
    double ticketExpense = GetTickets(attendees);
    int highOccupancy = GetOccupancy(attendees);
    double totalExpense = GetTotal(vehicleExpense, ticketExpense, highOccupancy);
    DisplayFee(totalExpense);
}

static int GetVehicle(){  //get the fees related to the vehicle
    Console.WriteLine("Are you driving an RV? Y/N");
    string vehicle = Console.ReadLine().ToUpper();
    return GetExpense(vehicle);
}

static int GetExpense(string vehicle){
    int expense = 0;
    if (vehicle[0] == 'Y'){
        expense = 20;
    }
    else if (vehicle[0]== 'N'){
        expense = 10;
    }
    return expense;
}

static int GetAttendees(){
    Console.WriteLine("How many people are attending the park?");
    return int.Parse(Console.ReadLine());
}

static double GetTickets(int attendeeCount){  //return the ticket expense
    int children = GetChildCount();
    int adults = attendeeCount - children;
    double ticketExpense = (12*adults) + (9.6*children);
    return ticketExpense;
}

static int GetChildCount(){
    Console.WriteLine("Are any of the attendees children? If yes, please enter how many, if no please enter 0");
    return int.Parse(Console.ReadLine());
}
static int GetOccupancy(int attendeeCount){  //occupancy fee if attendee count is 6 or more
    int occupancyFee = 0;
    if (attendeeCount > 5){
        occupancyFee = 5;
    }
    return occupancyFee;
}

static double GetTotal(int vehicle, double tickets, int occupancyFee){  //occupancy fee is not taxed
    double total = 1.09*(vehicle + tickets) + occupancyFee;
    return total;
}

//display fees and check how much user has paid
static void DisplayFee(double totalFees){
    string fees = totalFees.ToString("C"); //format output as currency with two decimal points
    System.Console.WriteLine($"You owe {fees} in park fees");
    double amountPaid = GetAmountPaid();
    BillStatus(totalFees, amountPaid);
}

static double GetAmountPaid(){
    System.Console.Write("Enter the amount you are paying: ");
    return double.Parse(Console.ReadLine());
}

static void BillStatus(double totalFees, double amountPaid){ //this part could be looped until the user pays the full amount
    totalFees = Math.Round(totalFees, 2);
    amountPaid = Math.Round(amountPaid, 2);
    if (totalFees == amountPaid){
        System.Console.WriteLine("Thanks for paying the full bill!");
    }
    else if(totalFees > amountPaid){
         double amountOwed = totalFees - amountPaid;
        string owed = amountOwed.ToString("C") ;
        System.Console.WriteLine($"Oh no! You still owe {owed}");
    }
    else {
        double difference = amountPaid - totalFees;
        string change = difference.ToString("C");
        System.Console.WriteLine($"Thanks for paying the full bill! We owe you {change} in change.");
    }
    PauseAction();
}