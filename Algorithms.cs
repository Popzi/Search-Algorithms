using System;
using System.Linq;
using System.IO;

/// <summary>
/// 
/// <TODO
/// b) https://justin.abrah.ms/computer-science/how-to-calculate-big-o.html -> Linjär sök är O(n) se e)
/// c) Alla typer av specialfall -> olika stora arrayer.. begränsad av int32
/// e) Att rita O(n), vilket är en linjär funktion är y = x (x = n i detta fall) ex. y = 1 och x = 1 och så fortsätter det linjärt uppåt
/// f) -> TODO Spara till .CSV
/// ></TODO>
/// </summary>
namespace Uppgift1
{
    class Program
    {
        public static int[] Numbers { get; set; }
        static TimeSpan Elapsed { get; set; }
        static void Main()
        {
            Random r = new Random();
                try
                {
                Console.WriteLine("Välj storlek på din array: ");
                int arraysize = Convert.ToInt32(Console.ReadLine());
                string gethow = "";

                    while(true)
                    {
                        Console.WriteLine("Vill du fylla arrayen slumpmässigt eller sorterat?: [R/S]: ");
                        gethow = Console.ReadLine().ToLower();
                        if (gethow == "r" || gethow == "s")
                        {
                            break;
                        }
                    } 

                        if (gethow.ToLower() == "r")
                        {
                            Numbers = Enumerable.Range(0, arraysize)
                                .Select(_ => Convert.ToInt32(r.Next(0, 2147483647)))
                                .ToArray();
                        }
                        else if (gethow.ToLower() == "s")
                        {
                            Numbers = Enumerable.Range(0, arraysize)
                                .Select(x => Convert.ToInt32(x))
                                .ToArray();
                        }


                    Console.WriteLine("Ange ett nummer att söka efter i arrayen: ");
                    int search = Convert.ToInt32(Console.ReadLine());
                    int result = Countoccurence(search);
                    Console.WriteLine("{0} hittades {1} gång(er) i arrayen. Förfluten tid: {2}. Array storlek: {3} - Angivelse i ms: {4}", search, result, Elapsed, arraysize, Elapsed.TotalMilliseconds);
                    WriteCSVFile("countoccurence",arraysize, Elapsed);

                    /// Uppgift 2 - d)
                    DateTime bruteStart = DateTime.Now;
                    int bruteforce = Brute(Numbers);
                    DateTime bruteEnd = DateTime.Now;
                    TimeSpan bruteElapsed = bruteEnd - bruteStart;
                    Console.WriteLine($"\nBruteforce time: {bruteElapsed.TotalMilliseconds} ms, max diff in array: {bruteforce} \n");
                    WriteCSVFile("brute", arraysize, bruteElapsed);
                    DateTime h4xStart = DateTime.Now;
                    int h4x = H4x(Numbers);
                    DateTime h4xEnd = DateTime.Now;
                    TimeSpan h4xElapsed = h4xEnd - h4xStart;
                    Console.WriteLine($"\nH4x time: {h4xElapsed.TotalMilliseconds} ms, maxx diff in array: {h4x}");
                    WriteCSVFile("h4x", arraysize, h4xElapsed);
                    Console.ReadLine();
                }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.ReadLine();
                    }
        }
        public static int Countoccurence(int search)
        {
            /// O(n) 
            int counter = 0; // O(1)
            DateTime startTime = DateTime.Now;

            foreach (int i in Numbers) // O(n)
            {
                if (i == search) // O(1)
                {
                    counter++; // O(1)
                }
            }
            DateTime stopTime = DateTime.Now;
            Elapsed = stopTime - startTime;
            return counter; // O(1)
        }


        // Uppgift 2 -> d)
        private static int Brute(int[] arr)
        {
            int diff = 0; // O(1)

            /// O(n^2)
            foreach (int index in arr) // O(n)
                for (int i = 0; i < arr.Length; i++) // O(n)
                {
                    int sub = index - arr[i]; // O(1)
                    if (sub > diff) // O(1)
                    {
                        diff = sub; // O(1)
                    }
                }

            return diff; // O(1)
        }

        private static int H4x(int[] arr)
        {
            /// O(n)
          return arr.Max() - arr.Min();
        }

        private static void WriteCSVFile(string filename, int arraySize, TimeSpan elapsed)
        {
            using (StreamWriter csv = File.AppendText($"{filename}.csv"))
            {
                string toWrite = string.Format("{0};{1}", arraySize, elapsed.TotalMilliseconds);
                csv.WriteLine(toWrite);
            }
        }
    }
}
