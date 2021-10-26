using System;
namespace ConsoleUI
{
    public class Utils
    {
        public static string Prompt(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static int PromptInt(string prompt)
        {
            int val;
            while (!int.TryParse(Prompt(prompt), out val))
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            return val;
        }

        public static double PromptDouble(string prompt)
        {
            double val;
            while (!double.TryParse(Prompt(prompt), out val))
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            return val;
        }

        public static T PromptEnum<T>(string prompt) where T : struct
        {
            T val;
            while (!Enum.TryParse(Prompt(prompt), out val))
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            return val;
        }
    }
}
