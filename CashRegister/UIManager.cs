using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Text.RegularExpressions;


namespace CashRegister
{
    public class UIManager
    {
        string heading;
        Dictionary<int, Func<int>> mainMenuOptions;
        Dictionary<int, string>? mainMenuOptionHeadings;
        Dictionary<int, Func<int>> shoppingBasketMenuOptions;
        Dictionary<int, string>? shoppingBasketMenuOptionHeadings;
        int quitProcess;
        int processOption;
        int currentState = 0;
        ShoppingBasket? shoppingBasket;

        public UIManager(string heading)
        {
            this.heading = heading;
            mainMenuOptions =  new Dictionary<int, Func<int>>();
            mainMenuOptionHeadings = new Dictionary<int, string>();
            shoppingBasketMenuOptions =  new Dictionary<int, Func<int>>();
            shoppingBasketMenuOptionHeadings = new Dictionary<int, string>();
            quitProcess =0;
            processOption=0;
            ConfigureMainMenuOptions();
        }

        void ConfigureMainMenuOptions()
        {
            if ((null !=  mainMenuOptions) && (null != mainMenuOptionHeadings))
            {
                mainMenuOptions.Clear();
                mainMenuOptionHeadings.Clear();

                mainMenuOptions.Add((int)RegisterState.LoadShoppingBasket, ProcessBasketItems);
                mainMenuOptionHeadings.Add((int)RegisterState.LoadShoppingBasket, "Process Basket Items");

                mainMenuOptions.Add((int)RegisterState.PrintReceipt, PrintReceipt);
                mainMenuOptionHeadings.Add((int)RegisterState.PrintReceipt, "Print Receipt for current Basket Items");

                mainMenuOptions.Add((int)RegisterState.Quit, Quit);
                mainMenuOptionHeadings.Add((int)RegisterState.Quit, "Exit");
            }
        }

        int PrintReceipt()
        {
            this.DisplayMainMenu();
            return 0;
        }

        public ShoppingBasket? GetBasket()
        {
            return shoppingBasket;
        }

        public int ProcessBasketItems()
        {
            int headingCount = this.heading.Count();
            string starsString = "";

            shoppingBasket  =  new   ShoppingBasket();
            while (true)
            {
                starsString = new string('*', (int)(headingCount * 2));
                Console.Clear();
                Console.WriteLine(starsString);
                var tempString = new string(' ', (int)(headingCount * 0.5));
                Console.Write(tempString);
                Console.WriteLine("Product Data");
                Console.WriteLine(starsString);

            Reenter_Name:
                Console.Write(" Please, enter the product name: ");
                var name = GetInput();
                if (name.Trim() == "")
                {
                    Console.WriteLine(" You did not enter the product name.");
                    goto Reenter_Name;
                }
            Reenter_Price:
                Console.Write(" Please, enter the product price: ");
                string price = GetInput();
                if (price.Trim() == "")
                {
                    Console.WriteLine(" You did not enter the product price.");
                    goto Reenter_Price;
                }
                decimal number;
                try
                {
                    number = decimal.Parse(price, CultureInfo.InvariantCulture);
                }
                catch
                {
                    Console.WriteLine(" You did not enter the product price, as a numeric value.");
                    goto Reenter_Price;
                }
            Reenter_TaxExemption:
                Console.Write(" Is product a books, food, or medical product?, Please enter Y or N: ");
                ConsoleKeyInfo isTaxExempted = Console.ReadKey();
                Console.WriteLine("");
                if ((isTaxExempted.Key.ToString().Trim() != "") &&
                    ((isTaxExempted.Key.ToString().ToLower() != "n") && (isTaxExempted.Key.ToString().ToLower() != "y")))
                {
                    Console.WriteLine(" You did not enter a valid response.");
                    goto Reenter_TaxExemption;
                }
            Reenter_IsImported:
                Console.Write(" Is product imported?, Please enter Y or N: ");
                ConsoleKeyInfo isImported = Console.ReadKey();
                Console.WriteLine("");
                if ((isImported.Key.ToString().Trim() != "") &&
                    ((isImported.Key.ToString().ToLower() != "n") && (isImported.Key.ToString().ToLower() != "y")))
                {
                    Console.WriteLine(" You did not enter a valid response.");
                    goto Reenter_IsImported;
                }

                if (null != shoppingBasket)
                {
                    bool isItImported = true;
                    bool isItTaxExempted = true;

                    if (isImported.Key.ToString().ToLower() == "n")
                    {
                        isItImported = false;
                    }
                    if (isTaxExempted.Key.ToString().ToLower() == "n")
                    {
                        isItTaxExempted = false;
                    }
                    var testProduct = new Product(name, number, (decimal)TaxSettings.BasicTaxRate,
                                                  (decimal)TaxSettings.DutyTaxRate, isItTaxExempted, isItImported);
                    if (testProduct != null)
                    {
                        testProduct.ComputeTax();
                        shoppingBasket.Add(testProduct);
                    }

                }
            Reenter_IsProcessCompleted:
                Console.Write(" Do you want to continue adding product in to the Basket?, Please enter Y or N: ");
                ConsoleKeyInfo isProcessCompleted = Console.ReadKey();
                Console.WriteLine("");
                if ((isProcessCompleted.Key.ToString().Trim() != "") &&
                    ((isProcessCompleted.Key.ToString().ToLower() != "n") && (isProcessCompleted.Key.ToString().ToLower() != "y")))
                {
                    Console.WriteLine(" You did not enter a valid response.");
                    goto Reenter_IsProcessCompleted;
                }
                if (isProcessCompleted.Key.ToString().ToLower() == "n")
                {
                    break;
                }
            }
            this.DisplayMainMenu();
            return 0;
        }

        public void ProcessInput()
        {
            int chosenOption = -13;
            string option = GetInput();

            chosenOption   = int.Parse(option);
            while (mainMenuOptions.Keys.Contains(chosenOption) == false)
            {
                Console.WriteLine(" No such option available.  Please retry: ");
                option = GetInput();
                chosenOption = int.Parse(option);
                this.currentState = chosenOption;
            }
            this.currentState = chosenOption;
        }

        public void RunChosenOption()
        {
            mainMenuOptions[this.currentState]();
        }

        public int GetCurrentState()
        {
            return this.currentState;
        }

        public int DisplayMainMenu()
        {
            int headingCount = this.heading.Count();
            string starsString = "";

            starsString = new string('*', (int)(headingCount * 2));
            Console.Clear();
            Console.WriteLine(starsString);
            var tempString = new string(' ', (int)(headingCount * 0.5));
            Console.Write(tempString);
            Console.WriteLine(this.heading);
            Console.WriteLine(starsString);
            Console.WriteLine("");
            Console.WriteLine("  Available options:");
            Console.WriteLine("  ");
            if (null != mainMenuOptionHeadings)
            {
                foreach (KeyValuePair<int, string> option in mainMenuOptionHeadings)
                {
                    Console.Write("   {0} - ", option.Key);
                    Console.WriteLine(option.Value);
                }
            }
            Console.WriteLine("");
            Console.WriteLine(starsString);
            Console.Write(" Please make your selection: ");
            return 0;
        }

        int Quit()
        {
            quitProcess = 1;
            return 0;
        }

        public bool IsQuit()
        {
            if (quitProcess == 1)
            {
                return true;
            }
            return false;
        }

        string GetInput()
        {
            string input = "";

            input = Console.ReadLine();
            return input;
        }

        public void CloseMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("     ****************************************");
            Console.WriteLine("     *     Closing the cash Register...     *");
            Console.WriteLine("     ****************************************");

        }


    }
}
