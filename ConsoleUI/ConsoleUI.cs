using System;
using IDAL.DO;
namespace ConsoleUI
{
    public class ConsoleUI
    {
        static DalObject.DalObject dalObject = new DalObject.DalObject();

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

        static void Main()
        {
            Console.WriteLine("Welcome to the Drone Delivery Manager!\n\n");
            showHelp();

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
                                        addBaseStation();
                                        continue;

                                    case "d":
                                    case "drone":
                                        addDrone();
                                        continue;

                                    case "c":
                                    case "customer":
                                        addCustomer();
                                        continue;

                                    case "p":
                                    case "package":
                                        addPackage();
                                        continue;

                                    default:
                                        break;
                                }
                            break;

                        case "s":
                        case "assign-package":
                            assignPackage();
                            break;

                        case "c":
                        case "collect-package":
                            collectPackage();
                            break;

                        case "p":
                        case "provide-package":
                            providePackage();
                            break;

                        case "g":
                        case "charge-drone":
                            chargeDrone();
                            break;

                        case "r":
                        case "release-drone":
                            releaseDrone();
                            break;

                        case "d":
                        case "display":
                            if (components.Length > 2 && int.TryParse(components[2], out int id))
                                switch (components[1])
                                {
                                    case "b":
                                    case "base-station":
                                        displayBaseStation(id);
                                        continue;

                                    case "d":
                                    case "drone":
                                        displayDrone(id);
                                        continue;

                                    case "c":
                                    case "customer":
                                        displayCustomer(id);
                                        continue;

                                    case "p":
                                    case "package":
                                        displayPackage(id);
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
                                        listBaseStations();
                                        continue;

                                    case "d":
                                    case "drones":
                                        listDrones();
                                        continue;

                                    case "c":
                                    case "customers":
                                        listCustomers();
                                        continue;

                                    case "p":
                                    case "packages":
                                        listPackages();
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

                showHelp();
            } while (command != "x" && command != "exit");
        }

        static void showHelp() => Console.WriteLine(helpText);


        static void addBaseStation() => dalObject.AddStation(
            new Station(
                Utils.PromptInt("id: "),
                Utils.Prompt("name: "),
                Utils.PromptDouble("longitude: "),
                Utils.PromptDouble("latitude: "),
                Utils.PromptInt("# of charge spots: ")));

        static void addDrone() => dalObject.AddDrone(
            new Drone(
                Utils.PromptInt("id: "),
                Utils.Prompt("model: "),
                Utils.PromptEnum<WeightCategory>("weight category [heavy|medium|light]: "),
                Utils.PromptEnum<DroneStatus>("status [free|maintenance|delivery]: "),
                Utils.PromptDouble("battery level: ")));

        static void addCustomer() => dalObject.AddCustomer(
            new Customer(
                Utils.PromptInt("id: "),
                Utils.Prompt("name: "),
                Utils.Prompt("phone: "),
                Utils.PromptDouble("longitude: "),
                Utils.PromptDouble("latitude: ")));

        static void addPackage() => dalObject.AddPackage(
            Utils.PromptInt("sender id: "),
            Utils.PromptInt("target id: "),
            Utils.PromptEnum<WeightCategory>("weight category [heavy|medium|light]: "),
            Utils.PromptEnum<Priority>("priority [regular|fast|emergency]: "));


        static void assignPackage() { }

        static void collectPackage() { }

        static void providePackage() { }

        static void chargeDrone() { }

        static void releaseDrone() { }


        static void displayBaseStation(int id) { }

        static void displayDrone(int id) { }

        static void displayCustomer(int id) { }

        static void displayPackage(int id) { }


        static void listBaseStations() { }

        static void listDrones() { }

        static void listCustomers() { }

        static void listPackages() { }
    }
}
