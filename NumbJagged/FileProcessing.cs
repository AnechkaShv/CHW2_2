
namespace ClassLibrary
{
    public class FileProcessing
    {
        readonly char[] invalidChars = Path.GetInvalidPathChars();

        private string? fPath;
        private string? nPath;
        /// <summary>
        /// This property sets value to path variable and checks its correctness.
        /// </summary>
        public string FPath
        {
            set
            {
                if (File.Exists(value) && value is not null && value.Length > 0 && value.IndexOfAny(invalidChars) == -1 && value != " ")
                    fPath = value;
                else
                    throw new ArgumentNullException();
            }
        }
        /// <summary>
        /// This property initializes new path and checks its correctness.
        /// </summary>
        public string? NPath
        {
            set
            {
                // A path can't include separator's symbols.
                if (value is not null && value.Length > 0 && value.IndexOfAny(invalidChars) == -1 && value != " " && !value.Contains(Path.PathSeparator) && !value.Contains("..\\\\") && !value.Contains("..\\") && !value.Contains("../"))
                    nPath = value;
                else
                    nPath = null;
            }
        }
        /// <summary>
        /// This method reads numbers(array sizes) from the file to int array and returns information about correctness of the process.
        /// </summary>
        /// <param name="jagNumbs"></param>
        /// <returns></returns>
        public bool FileReader(out int[] jagNumbs)
        {
            jagNumbs = new int[1];
            // String array of values from the file.
            string[] str;
            try
            {
                // Reading file, dividing data by spaces and line translations.
                using (StreamReader sr = new StreamReader(fPath))
                {
                    str = sr.ReadToEnd().Split('\n', ' ');
                }
            }
            // Catching exceptions of streamreader from documentation.
            catch (OutOfMemoryException)
            {
                PrintColor("The data in the file has too big size. Please try again.", ConsoleColor.Red);
                return false;
            }
            catch (IOException)
            {
                PrintColor("Error while opening the file. Please try again.", ConsoleColor.Red);
                return false;
            }
            catch(ArgumentNullException)
            {
                PrintColor("Wrong file name. Please try again.", ConsoleColor.Red);
                return false;
            }
            if (str is null || str.Length <= 0)
            {
                PrintColor("Wrong file data. Please try again.", ConsoleColor.Red);
                return false;
            }
            jagNumbs = new int[str.Length];
            // this variavle checks correctness of numbers in the file.
            bool flag = true;
            // Initializing int array with parsing values from string array from the file.
            for (int i = 0; i < str.Length; i++)
            {
                if (!int.TryParse(str[i], out jagNumbs[i]) || jagNumbs[i] < 0)
                {
                    flag = false;
                    break;
                }
            }
            // If at least one value is incorrect, return false.
            if (!flag)
            {
                PrintColor("Wrong numbers in the file. Please try again.", ConsoleColor.Red);
                return false;
            }
            return true;
        }
        /// <summary>
        /// This method writes jagged array and square values in the file and return information about correctness of the process.
        /// </summary>
        /// <param name="jagArray"></param>
        /// <param name="maxSquares"></param>
        /// <returns></returns>
        public bool FileWriter(string[][] jagArray, string[][]maxSquares)
        {
            try
            {
                // Writing data to the file with name in nPath.
                using (StreamWriter sw = new StreamWriter(nPath))
                {
                    // Wring every array on new line.
                    for (int i = 0; i < jagArray.Length; i++, sw.WriteLine())
                    {
                        for (int j = 0; j < jagArray[i].Length; j++)
                        {
                            sw.WriteLine(jagArray[i][j]);
                            // If values in a string are incorrect for creating a triange the message about it will be written. 
                            if (maxSquares[i][j].Split(' ')[0] == "0" || maxSquares[i][j].Split(' ')[1] == "0" || maxSquares[i][j].Split(' ')[2] == "0")
                                sw.WriteLine("There are no triangle with these numbers.");
                            else
                                sw.WriteLine("The triangle with maximum area has sides: " + maxSquares[i][j]);
                        }
                    }
                }
                return true;
            }
            // Catching exceptions of streamwriter from the documentation.
            catch (IOException)
            {
                PrintColor("Something went wrong while writing data in the file. Please try again.", ConsoleColor.Red);
                return false;
            }
            catch(ArgumentNullException)
            {
                PrintColor("Wrong file name. Please try again.", ConsoleColor.Red);
                return false;
            }
            catch(ArgumentOutOfRangeException)
            {
                PrintColor("Error while working with array. Please try again.",ConsoleColor.Red);
                return false;
            }
            catch(ObjectDisposedException)
            {
                PrintColor("Error while opening the file. Please try again.", ConsoleColor.Red);
                return false;
            }
            catch(NotSupportedException)
            {
                PrintColor("Error while opening the file. Please try again.", ConsoleColor.Red);
                return false;
            }
            catch(ArgumentException)
            {
                PrintColor("Wrong path. Please try again.", ConsoleColor.Red);
                return false;
            }
        }
        /// <summary>
        /// This method prints jagged array and square values.
        /// </summary>
        /// <param name="jagArray"></param>
        /// <param name="maxSquares"></param>
        public void PrintData(string[][] jagArray, string[][] maxSquares)
        {
            // Printing every array on new line.
            for (int i = 0; i < jagArray.Length; i++, Console.WriteLine())
            {
                for (int j = 0; j < jagArray[i].Length; j++)
                {
                    Console.WriteLine(jagArray[i][j]);
                    // If values in a string are incorrect for creating a triange the message about it will be printed.
                    if (maxSquares[i][j].Split(' ')[0] == "0" || maxSquares[i][j].Split(' ')[1] == "0" || maxSquares[i][j].Split(' ')[2] == "0")
                        PrintColor("There are no triangle with these numbers.", ConsoleColor.Yellow);
                    else
                        Console.WriteLine("The triangle with maximum area has sides: " + maxSquares[i][j]);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// This method prints colourful text.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        public static void PrintColor(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}
