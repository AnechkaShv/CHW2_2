using ClassLibrary;

namespace KHW2_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Repeating cycle until Escape.
            do
            {
                // Corectness checking cycle.
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter your file's path.");

                        // Creating an object.
                        FileProcessing file = new FileProcessing();

                        // Getting file path from user.
                        file.FPath = Console.ReadLine();

                        // Checking data in the file and writing it to int array.
                        // If there are several values of N in the file and we should create an array for every one, program will also work.
                        if (!file.FileReader(out int[] nums))
                            continue;

                        FileProcessing.PrintColor("Congratulations! Your file is correct!", ConsoleColor.Green);

                        NumbJagged[] numbJagged = new NumbJagged[nums.Length];
                        
                        // Initializing jagged array using class' constructor.
                        for (int i = 0; i < nums.Length; i++)
                        {
                            numbJagged[i] = new NumbJagged(nums[i]);
                        }
                        // Jagged array of string arrays with random numbers. 
                        string[][] resArr = new string[nums.Length][];

                        // Jagged array of elements of triangle with maximal area.
                        string[][] maxAreas = new string[nums.Length][];

                        // Frormatting values from jagged array with random numbers and sides of triangles with maximal areas to string array.
                        for (int i = 0; i < resArr.Length; i++)
                        {
                            // An array of array contains strings with joined values.
                            resArr[i] = numbJagged[i].StringOut();

                            // An array of array contains strings with maximal elements of triangle for each row.
                            maxAreas[i] = new string[resArr[i].Length];

                            // Initializing the array of maximal sides values.
                            for (int j = 0; j < resArr[i].Length; j++)
                            {
                                maxAreas[i][j] = numbJagged[i].MinSquareNumb(resArr[i][j]);
                            }
                        }
                        // Printing obtained data.
                        file.PrintData(resArr, maxAreas);
                        
                        // Interface of saving data.
                        Console.WriteLine("This data is going to be saved. Press Escape to avoid saving");

                        if (Console.ReadKey().Key == ConsoleKey.Escape)
                            break;
                        
                        Console.WriteLine("Enter a file's name to save data there.");

                        // Getting path from user to save data and checking its correctness. 
                        // Impossible to d next step until right path is entered.
                        while ((file.NPath = Console.ReadLine()) == null || !file.FileWriter(resArr, maxAreas))
                            Console.WriteLine("Enter a file's name to save data there.");

                        FileProcessing.PrintColor("Your file has been successfully saved!", ConsoleColor.Green);
                        break;
                    }
                    // Catching exceptions from classes, which have been thrown.
                    catch(ArgumentNullException)
                    {
                        FileProcessing.PrintColor("Wrong file path. Please try again.", ConsoleColor.Red);
                        continue;
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        FileProcessing.PrintColor("Wrong array size.Please try again.", ConsoleColor.Red);
                        continue;
                    }
                    catch(ArgumentException)
                    {
                        FileProcessing.PrintColor("Wrong numbers in a string. Please try againg.", ConsoleColor.Red);
                        continue;
                    }
                }
                Console.WriteLine("Press Escape to exit program. To continue press another key.");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}