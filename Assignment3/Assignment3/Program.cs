namespace Assignment3
{
    internal class Program
    {
        //Made by Micael Koule - Github: mkoule1 :last change November 15th
       
        static void Main(string[] args)
        {
            bool continueProgram = true;

            // TODO: 
            // declare a constant to represent the maximum size of the arrays
            // arrays must be large enough to store data for an entire month 
            const int max_days = 31;
            // TODO:
            // create a string array named dates, using the max size constant you created above to specify the physical size of the array
            string[] dates = new string[max_days];
            // TODO:
            // create a double array named minutes, using the max size constant you created above to specify the physical size of the array
            double[] minutes = new double[max_days];
            // TODO:
            // create a variable to represent the logical size of the array
            int count = 0;
            DisplayProgramIntro();

            // TODO: call DisplayMainMenu()
            DisplayMainMenu();
            while (continueProgram)
            {
                string mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
                Console.WriteLine();

                //MAIN MENU Switch statement
                switch (mainMenuChoice)
                {
                    case "N": //[N]ew Daily Entries

                        if (AcceptNewEntryDisclaimer())
                        {
                            // TODO: call EnterDailyValues & assign its return value
                            count = EnterDailyValues(dates, minutes);
                            Console.WriteLine($"\nEntries completed. {count} records in temporary memory.\n");
                        }
                        else
                        {
                            Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
                        }
                        break;
                    case "S": //[S]ave Entries to File
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
                        }
                        else if (AcceptSaveEntryDisclaimer())
                        {
                            string filename = PromptForFilename();
                            // TODO: call SaveToFile()
                            SaveToFile(filename, dates, minutes, count);

                        }
                        else
                        {
                            Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
                        }

                        break;
                    case "E": //[E]dit Entries
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
                        }
                        else if (AcceptEditEntryDisclaimer())
                        {
                            //TODO: call EditEntries()
                            EditEntries(dates, minutes, count);
                        }
                        else
                        {
                            Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "L": //[L]oad  File
                        if (AcceptLoadEntryDisclaimer())
                        {
                            string filename = Prompt("Enter name of file to load: ");
                            // TODO: call LoadFromFile() and assign its return value
                            count = LoadFromFile(filename, dates, minutes);
                            Console.WriteLine($"{count} records were loaded.\n");
                        }
                        else
                        {
                            Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "V":
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
                        }
                        else
                        {
                            // TODO: call DisplayEntries()
                            DisplayEntries(dates, minutes, count);
                        }
                        break;
                    case "M": //[M]onthly Statistics
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
                        }
                        else
                        {
                            RunAnalysisMenu(dates, minutes, count);
                        }
                        break;
                    case "D": //[D]isplay Main Menu
                        //TODO: call DisplayMainMenu()
                        DisplayMainMenu();
                        break;
                    case "Q": //[Q]uit Program
                        bool quit = Prompt("Are you sure you want to quit (y/n)? ").ToLower().Equals("y");
                        Console.WriteLine();
                        if (quit)
                        {
                            continueProgram = false;
                        }
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
                        break;
                }
            }

            DisplayProgramOutro();
        }

        /// <summary>
        /// Runs the analysis sub-menu to display summary metrics.
        /// </summary>
        /// <param name="dates">an array containing dates in YYYY-MM-DD format</param>
        /// <param name="numbers">an array containing numeric values</param>
        /// <param name="count">logical count of elements</param>
        static void RunAnalysisMenu(string[] dates, double[] numbers, int count)
        {
            bool runAnalysis = true;
            string year = dates[0].Substring(0, 4),
                month = dates[0].Substring(5, 3);

            while (runAnalysis)
            {
                string analysisMenuChoice;

                // TODO: call DisplayAnalysisMenu()
                DisplayAnalysisMenu();
                analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                Console.WriteLine();

                switch (analysisMenuChoice)
                {
                    case "A": //[A]verage 
                        // TODO: uncomment the next 2 lines & call CalculateMean();
                        double mean = CalculateMean(numbers, count);
                        Console.WriteLine($"The mean value for {month} {year} is: {mean:N2}.\n");
                        break;
                    case "H": //[H]ighest 
                        // TODO: uncomment the next 2 lines & call CalculateLargest();
                        double largest = CalculateLargest(numbers, count);
                        Console.WriteLine($"The largest value for {month} {year} is: {largest:N2}.\n");
                        break;
                    case "L": //[L]owest 
                              //TODO: uncomment the next 2 lines & call CalculateSmallest();
                        double smallest = CalculateSmallest(numbers, count);
                        Console.WriteLine($"The smallest value for {month} {year} is: {smallest:N2}.\n");
                        break;
                    case "G": //[G]raph 
                              //TODO: call DisplayChart()
                        DisplayChart(dates, numbers, count);
                        Prompt("Press <enter> to continue...");
                        break;
                    case "R": //[R]eturn to MAIN MENU
                        runAnalysis = false;
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a submenu option.");
                        break;
                }
            }
        }

        // ================================================================================================ //
        //                                                                                                  //
        //                                              METHODS                                             //
        //                                                                                                  //
        // ================================================================================================ //

        // ++++++++++++++++++++++++++++++++++++ Difficulty 1 ++++++++++++++++++++++++++++++++++++

        // TODO: create the DisplayMainMenu() method
        static void DisplayMainMenu()
        {
            Console.WriteLine("-= MAIN MENU = -");
            Console.WriteLine("N ->New Daily Entries");
            Console.WriteLine("S ->Save Entries to File");
            Console.WriteLine("L ->Load File");
            Console.WriteLine("V ->View Entries");
            Console.WriteLine("E ->Edit Entries");
            Console.WriteLine("M ->Monthly Statistics");
            Console.WriteLine("D ->Display Main Menu");
            Console.WriteLine("Q ->Quit Program");
            Console.WriteLine();
        }
        // TODO: create the DisplayAnalysisMenu() method
        static void DisplayAnalysisMenu()
        {
            Console.WriteLine("-=- ANALYSIS MENU -=-");
            Console.WriteLine("[A]verage");
            Console.WriteLine("[H]ighest");
            Console.WriteLine("[L]owest");
            Console.WriteLine("[G]raph");
            Console.WriteLine("[R]eturn to Main Menu");
            Console.WriteLine();
        }

        // TODO: create the Prompt method
        static string Prompt(string promptMessage)
        {
            Console.Write(promptMessage);
            return Console.ReadLine();
        }
        // TODO: create the PromptDouble() method
        static double PromptDouble(string promptMessage)
        {
            double result = 0;
            bool isValid = false;

            while (!isValid)
            {
                try
                {
                    Console.Write(promptMessage);
                    result = double.Parse(Console.ReadLine());

                    if (result < 0)
                    {
                        Console.WriteLine("Value must be zero or positive.");
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" {ex.Message}. Try again.");
                }
            }

            return result;
        }

        // optional TODO: create the PromptInt() method
        static int PromptInt(string promptMessage)
        {
            int result = 0;
            bool isValid = false;

            while (!isValid)
            {
                try
                {
                    Console.Write(promptMessage);
                    result = int.Parse(Console.ReadLine());
                    isValid = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}. Please try again.");
                }
            }

            return result;
        }

        // TODO: create the CalculateLargest() method
        static double CalculateLargest(double[] values, int countOfEntries)
            {
                double largest = values[0];

                for (int i = 1; i < countOfEntries; i++)
                {
                    if (values[i] > largest)
                    {
                        largest = values[i];
                    }
                }

                return largest;
            }
            // TODO: create the CalculateSmallest() method
            static double CalculateSmallest(double[] values, int countOfEntries)
            {
                double smallest = values[0];

                for (int i = 1; i < countOfEntries; i++)
                {
                    if (values[i] < smallest)
                    {
                        smallest = values[i];
                    }
                }

                return smallest;
            }
            // TODO: create the CalculateMean() method
            static double CalculateMean(double[] values, int countOfEntries)
            {
                double sum = 0;

                for (int i = 0; i < countOfEntries; i++)
                {
                    sum += values[i];
                }

                return sum / countOfEntries;
            }
            // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++

            // TODO: create the EnterDailyValues method
            static int EnterDailyValues(string[] dates, double[] values)
            {
                Console.WriteLine("\n=== Enter Daily Values ===");

                string year = Prompt("Enter year (YYYY): ");
                string month = Prompt("Enter month (MMM, e.g., JAN, FEB, JUL): ").ToUpper();
                int daysInMonth = PromptInt("Enter number of days in the month (28-31): ");

                // Validate days in month
                while (daysInMonth < 28 || daysInMonth > 31)
                {
                    Console.WriteLine(" Days must be between 28 and 31.");
                    daysInMonth = PromptInt("Enter number of days in the month (28-31): ");
                }

                Console.WriteLine();

                // Enter values for each day
                for (int i = 0; i < daysInMonth; i++)
                {
                    int day = i + 1;
                    dates[i] = $"{year}-{month}-{day:D2}";
                    values[i] = PromptDouble($"Enter minutes for {dates[i]}: ");
                }

                return daysInMonth;
            }
            // TODO: create the LoadFromFile method
            static int LoadFromFile(string filename, string[] dates, double[] values)
            {
                int count = 0;

                try
                {
                    StreamReader reader = new StreamReader(filename);

                    // Skip header line
                    reader.ReadLine();

                    // Read data lines
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(',');

                        if (parts.Length == 2)
                        {
                            dates[count] = parts[0];
                            values[count] = double.Parse(parts[1]);
                            count++;
                        }
                    }

                    reader.Close();
                    Console.WriteLine($"Successfully loaded data from {filename}.");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Error: File '{filename}' not found.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading file: {ex.Message}");
                }

                return count;
            }
            // TODO: create the SaveToFile method
            static void SaveToFile(string filename, string[] dates, double[] values, int countOfEntries)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(filename);

                    // Write header
                    writer.WriteLine("Date,Minutes");

                    // Write data
                    for (int i = 0; i < countOfEntries; i++)
                    {
                        writer.WriteLine($"{dates[i]},{values[i]:F2}");
                    }

                    writer.Close();
                    Console.WriteLine($"\nSuccessfully saved {countOfEntries} records to {filename}.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}\n");
                }
            }
            // TODO: create the DisplayEntries method
            static void DisplayEntries(string[] dates, double[] values, int countOfEntries)
            {
                Console.WriteLine("\n=== Current Entries ===");
                Console.WriteLine("{0,-15} {1,10}", "Date", "Minutes");
                Console.WriteLine(new string('-', 27));

                for (int i = 0; i < countOfEntries; i++)
                {
                    Console.WriteLine("{0,-15} {1,10:F2}", dates[i], values[i]);
                }

                Console.WriteLine();
            }
            // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

            // TODO: create the EditEntries method
            static void EditEntries(string[] dates, double[] values, int countOfEntries)
            {
                Console.WriteLine();
                DisplayEntries(dates, values, countOfEntries);

                string dateToEdit = Prompt("Enter the date you wish to edit (YYYY-MMM-DD): ");

                // Find the index of the date
                int indexToEdit = -1;
                for (int i = 0; i < countOfEntries; i++)
                {
                    if (dates[i].Equals(dateToEdit, StringComparison.OrdinalIgnoreCase))
                    {
                        indexToEdit = i;
                        break;
                    }
                }

                if (indexToEdit == -1)
                {
                    Console.WriteLine($"\nError: Date '{dateToEdit}' not found in current entries.\n");
                }
                else
                {
                    Console.WriteLine($"\nCurrent value for {dates[indexToEdit]}: {values[indexToEdit]:F2} minutes");
                    double newValue = PromptDouble("Enter new value: ");
                    values[indexToEdit] = newValue;
                    Console.WriteLine($"Successfully updated {dates[indexToEdit]} to {newValue:F2} minutes.\n");
                }
            }
            // ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

            // TODO: create the DisplayChart method
            static void DisplayChart(string[] dates, double[] values, int countOfEntries)
            {
                Console.WriteLine("\n=== Game Time Overview ===\n");

                for (int i = 0; i < countOfEntries; i++)
                {
                    // Calculate number of blocks (each block = 30 minutes)
                    int blocks = (int)(values[i] / 30);

                    // Display date and bar
                    Console.Write($"{dates[i]} | ");

                    for (int j = 0; j < blocks; j++)
                    {
                        Console.Write("█");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
            // ********************************* Helper methods *********************************

            /// <summary>
            /// Displays the Program intro.
            /// </summary>
            static void DisplayProgramIntro()
            {
                Console.WriteLine("****************************************\n" +
                    "*                                      *\n" +
                    "*          Monthly  Game Time          *\n" +
                    "*                                      *\n" +
                    "****************************************\n");
            }

            /// <summary>
            /// Displays the Program outro.
            /// </summary>
            static void DisplayProgramOutro()
            {
                Console.Write("Program terminated. Press ENTER to exit program...");
                Console.ReadLine();
            }

            /// <summary>
            /// Displays a disclaimer for NEW entry option.
            /// </summary>
            /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
            static bool AcceptNewEntryDisclaimer()
            {
                bool response;
                Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                    "Hint: Select EDIT from the main menu instead, to change individual days.\n");
                response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
                Console.WriteLine();
                return response;
            }

            /// <summary>
            /// Displays a disclaimer for SAVE entry option.
            /// </summary>
            /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
            static bool AcceptSaveEntryDisclaimer()
            {
                bool response;
                Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.\n" +
                    "Hint: Files will be saved to this program's directory by default.\n" +
                    "Hint: If the file does not yet exist, it will be created.\n");
                response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
                Console.WriteLine();
                return response;
            }

            /// <summary>
            /// Displays a disclaimer for EDIT entry option.
            /// </summary>
            /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
            static bool AcceptEditEntryDisclaimer()
            {
                bool response;
                Console.WriteLine("Disclaimer: editing will overwrite unsaved values.\n" +
                    "Hint: Save to a file before editing.\n");
                response = Prompt("Do you wish to proceed anyway? (y/n ").ToLower().Equals("y");
                Console.WriteLine();
                return response;
            }

            /// <summary>
            /// Displays a disclaimer for LOAD entry option.
            /// </summary>
            /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
            static bool AcceptLoadEntryDisclaimer()
            {
                bool response;
                Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                    "Hint: If you entered New Daily entries, save them first!\n");
                response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
                Console.WriteLine();
                return response;
            }

            /// <summary>
            /// Displays prompt for a filename, and returns a valid filename. 
            /// Includes exception handling.
            /// </summary>
            /// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
            static string PromptForFilename()
            {
                string filename = "";
                bool isValidFilename = true;
                const string CSV_FILE_EXTENSION = ".csv";
                const string TXT_FILE_EXTENSION = ".txt";

                do
                {
                    filename = Prompt("Enter name of .csv or .txt file to save to (e.g. JAN-2025-data.csv): ");
                    if (filename == "")
                    {
                        isValidFilename = false;
                        Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
                    }
                    else
                    {
                        if (!filename.EndsWith(CSV_FILE_EXTENSION) && !filename.EndsWith(TXT_FILE_EXTENSION)) //if filename does not end with .txt or .csv.
                        {
                            filename = filename + CSV_FILE_EXTENSION; //append .csv to filename
                            Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
                            isValidFilename = true;
                        }
                        else
                        {
                            Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
                            isValidFilename = true;
                        }
                    }
                } while (!isValidFilename);
                return filename;
            }

        
    }
}