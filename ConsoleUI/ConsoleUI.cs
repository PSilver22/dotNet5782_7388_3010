using System;
using DO;
namespace ConsoleUI
{
    public class ConsoleUI
    {
        static DalApi.IDAL dalObject;

        const string helpText =
@"Available commands:
command:                        description:                                    options:
[a]dd [b]ase-station            Add a base station to the list of stations
[a]dd [d]rone                   Add a drone to the list of drones
[a]dd [c]ustomer                Add a customer to the list of customers
[a]dd [p]ackage                 Receive a package to deliver

a[s]sign-package                Assign a package to a drone
[c]ollect-package               Have a drone collect a package
[p]rovide-package               Provide a package to a customer

char[g]e-drone                  Send a drone to the charging station
[r]elease-drone                 Release a drone from the charging station

[d]isplay [b]ase-station <id>   Display the base station with ID <id>
[d]isplay [d]rone <id>          Display the drone with ID <id>
[d]isplay [c]ustomer <id>       Display the customer with ID <id>
[d]isplay [p]ackage <id>        Display the package with ID <id>

[l]ist [b]ase-stations          Display the list of base stations               -u/--unoccupied: only unoccupied base stations
[l]ist [d]rones                 Display the list of drones
[l]ist [c]ustomers              Display the list of customers
[l]ist [p]ackages               Display the list of packages                    -u/--unassigned: only unassigned packages

[h]elp                          Show this message
e[x]it                          Quit the program
";

        #region Main
        static void Main()
        {
            dalObject = DalApi.DalFactory.GetDAL("xml");

            Console.WriteLine("Welcome to the Drone Delivery Manager!\n\n");
            ShowHelp();

            string command;
            do
            {
                command = Console.ReadLine()?.Trim() ?? "";

                var components = command.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

                if (components.Length > 0)
                    switch (components[0])
                    {
                        case "a":
                        case "add":
                            if (components.Length > 1) switch (components[1])
                                {
                                    case "b":
                                    case "base-station":
                                        AddBaseStation();
                                        continue;

                                    case "d":
                                    case "drone":
                                        AddDrone();
                                        continue;

                                    case "c":
                                    case "customer":
                                        AddCustomer();
                                        continue;

                                    case "p":
                                    case "package":
                                        AddPackage();
                                        continue;

                                    default:
                                        break;
                                }
                            break;

                        case "s":
                        case "assign-package":
                            AssignPackage();
                            continue;

                        case "c":
                        case "collect-package":
                            CollectPackage();
                            continue;

                        case "p":
                        case "provide-package":
                            ProvidePackage();
                            continue;

                        case "g":
                        case "charge-drone":
                            ChargeDrone();
                            continue;

                        case "r":
                        case "release-drone":
                            ReleaseDrone();
                            break;

                        case "d":
                        case "display":
                            if (components.Length > 2 && int.TryParse(components[2], out int id))
                                switch (components[1])
                                {
                                    case "b":
                                    case "base-station":
                                        DisplayBaseStation(id);
                                        continue;

                                    case "d":
                                    case "drone":
                                        DisplayDrone(id);
                                        continue;

                                    case "c":
                                    case "customer":
                                        DisplayCustomer(id);
                                        continue;

                                    case "p":
                                    case "package":
                                        DisplayPackage(id);
                                        continue;

                                    default:
                                        break;
                                }
                            break;

                        case "l":
                        case "list":
                            if (components.Length > 1) switch (components[1])
                                {
                                    case "b":
                                    case "base-stations":
                                        if (components.Length > 2 && (components[2] == "-u" || components[2] == "--unoccupied"))
                                            ListUnoccupiedBaseStations();
                                        else
                                            ListBaseStations();
                                        continue;

                                    case "d":
                                    case "drones":
                                        ListDrones();
                                        continue;

                                    case "c":
                                    case "customers":
                                        ListCustomers();
                                        continue;

                                    case "p":
                                    case "packages":
                                        if (components.Length > 2 && (components[2] == "-u" || components[2] == "--unassigned"))
                                            ListUnassignedPackages();
                                        else
                                            ListPackages();
                                        continue;

                                    default:
                                        break;
                                }
                            break;

                        case "x":
                        case "exit":
                            continue;

                        default:
                            break;
                    }

                ShowHelp();
            } while (command != "x" && command != "exit");
        }

        #endregion

        #region HelperFunctions
        static void ShowHelp() => Console.WriteLine(helpText);

        static void AddBaseStation()
        {
            dalObject.AddStation(
                new Station(
                    Utils.PromptInt("id: "),
                    Utils.Prompt("name: "),
                    Utils.PromptDouble("longitude: "),
                    Utils.PromptDouble("latitude: "),
                    Utils.PromptInt("# of charge spots: ")));
        }

        static void AddDrone()
        {
            dalObject.AddDrone(
                new Drone(
                    Utils.PromptInt("id: "),
                    Utils.Prompt("model: "),
                    Utils.PromptEnum<WeightCategory>("weight category [heavy|medium|light]: "),
                    Utils.PromptDouble("battery level: ")));
        }

        static void AddCustomer()
        {
            dalObject.AddCustomer(
                new Customer(
                    Utils.PromptInt("id: "),
                    Utils.Prompt("name: "),
                    Utils.Prompt("phone: "),
                    Utils.PromptDouble("longitude: "),
                    Utils.PromptDouble("latitude: ")));
        }

        static void AddPackage()
        {
            dalObject.AddPackage(
                Utils.PromptInt("sender id: "),
                Utils.PromptInt("target id: "),
                Utils.PromptEnum<WeightCategory>("weight category [heavy|medium|light]: "),
                Utils.PromptEnum<Priority>("priority [regular|fast|emergency]: "));
        }

        static void AssignPackage()
        {
            var id = Utils.PromptInt("package id: ");
            dalObject.AssignPackage(id);
        }

        static void CollectPackage() => dalObject.CollectPackage(
            Utils.PromptInt("package id: "));

        static void ProvidePackage() => dalObject.ProvidePackage(
            Utils.PromptInt("package id: "));

        static void ChargeDrone()
        {
            var droneId = Utils.PromptInt("drone id: ");

            Console.WriteLine("Choose a charging station:");
            Console.WriteLine(dalObject.GetStationList(x => x.ChargeSlots > 0));

            dalObject.ChargeDrone(droneId, Utils.PromptInt("station id: "));
        }

        static void ReleaseDrone() => dalObject.ReleaseDrone(
            Utils.PromptInt("drone id: "));


        static void DisplayBaseStation(int id) =>
            Console.WriteLine(dalObject.GetStation(id));

        static void DisplayDrone(int id) =>
            Console.WriteLine(dalObject.GetDrone(id));

        static void DisplayCustomer(int id) =>
            Console.WriteLine(dalObject.GetCustomer(id));

        static void DisplayPackage(int id) =>
            Console.WriteLine(dalObject.GetPackage(id));


        static void ListBaseStations() =>
            Console.WriteLine(dalObject.GetStationList());

        static void ListUnoccupiedBaseStations() =>
            Console.WriteLine(dalObject.GetStationList(x => x.ChargeSlots > 0));

        static void ListDrones() =>
            Console.WriteLine(dalObject.GetDroneList());

        static void ListCustomers() =>
            Console.WriteLine(dalObject.GetCustomerList());

        static void ListPackages() =>
            Console.WriteLine(dalObject.GetPackageList());

        static void ListUnassignedPackages() =>
            Console.WriteLine(dalObject.GetPackageList(x => x.DroneId == 0));

        #endregion
    }
}