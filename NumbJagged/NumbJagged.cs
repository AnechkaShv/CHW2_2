namespace ClassLibrary
{
    public class NumbJagged
    {
        readonly int[][]? jagArr = null;
        // Randomizer.
        readonly Random rnd = new Random((int)DateTime.Now.Ticks);
        public NumbJagged() { }
        /// <summary>
        /// This constructor initializes jagged array with random numbers.
        /// </summary>
        /// <param name="N"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public NumbJagged(int N)
        {
            // Checking correctness of array's size.
            if (N > 0)
            {
                jagArr = new int[N][];
                for (int i = 0; i < N; i++)
                {
                    int j = 0, a;
                    // Initializing array until random number is not 0.
                    do
                    {
                        a = rnd.Next(0, 6);

                        // Adding number to array using resizing.
                        Array.Resize(ref jagArr[i], j + 1);
                        jagArr[i][j] = a;

                        // Increasing counter.
                        j += 1;
                    } while (a != 0);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// This method represents jagged array as an array of strings and returns it.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string[] StringOut()
        {
            // Checking array.
            if (jagArr is null || jagArr.Length <= 0)
                throw new ArgumentOutOfRangeException();
            else
            {
                // Creating and initializing output string array.
                string[] stringArr = new string[jagArr.Length];

                for (int i = 0; i < jagArr.Length; i++)
                {
                    for (int j = 0; j < jagArr[i].Length; j++)
                    {
                        // Dividing elements with spaces except last element in a row.
                        if (j != jagArr[i].Length - 1)
                            stringArr[i] += jagArr[i][j] + " ";
                        else
                            stringArr[i] += jagArr[i][j];
                    }
                }
                return stringArr;
            }
        }
        /// <summary>
        /// This method returns numbers, which are had by triangle with maximal area.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string MinSquareNumb(string str)
        {
            // Checking input data.
            if(str is null || str.Length <= 0)
                throw new ArgumentException();

            // Splitting string and extract values to the array. 
            string[] strArr = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // Creating and initiaizing int array with values from string array.
            int[] jagArr = new int[strArr.Length];

            for (int i = 0; i < strArr.Length; i++)
            {
                // Checking numbers in a string.
                if (!int.TryParse(strArr[i], out jagArr[i]))
                    throw new ArgumentException();
            }

            // Variable for writing maximal area there.
            double maxArea = 0d;

            // Tuple of triangle sides.
            (int a, int b, int c) res = (a: 0, b: 0, c: 0);

            // Searching 3 suitable numbers for triangle sides.
            for (int i = 0; i < jagArr.Length - 2; i++)
            {
                for (int j = i+1; j < jagArr.Length-1; j++)
                {
                    for (int k = j + 1; k < jagArr.Length; k++)
                    {
                        // Checking existence of a triangle with these sides.
                        if (jagArr[i] + jagArr[j] > jagArr[k] && jagArr[j] + jagArr[k] > jagArr[i] && jagArr[i] + jagArr[k] > jagArr[j])
                        {
                            // Searching semiperimetr.
                            double p = (jagArr[i] + jagArr[j] + jagArr[k]) / 2.0;

                            // Searching area.
                            double area = Math.Sqrt(p * (p - jagArr[i]) * (p - jagArr[j]) * (p - jagArr[k]));

                            // Comparison current maximal area and new area. 
                            if(area > maxArea)
                            {
                                maxArea = area;
                                res.a = jagArr[i];
                                res.b = jagArr[j];
                                res.c = jagArr[k];
                            }
                        }
                    }
                }
            }
            // Returning 3 sides of triangle divided by spaces.
            return (res.a + " " +res.b + " "+ res.c).ToString();
        }
    }
}