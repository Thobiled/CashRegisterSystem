using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    enum RegisterState
    {

        MainMenu,
        LoadShoppingBasket,
        PrintReceipt,
        Quit,
    };


    enum TaxSettings
    {
        BasicTaxRate = 10,
        DutyTaxRate = 5
    };

    public class CashRegisterManager
    {
        UIManager uiManager;
        ShoppingBasket? shoppingBasket;


        public CashRegisterManager()
        {
            uiManager = new UIManager("Cash Register");
            shoppingBasket   =  new ShoppingBasket();
        }

        public void Run()
        {
            int currentState = -12;

            if (uiManager != null)
            {
                uiManager.DisplayMainMenu();
                while (true)  // State Machine
                {
                    if ((int)RegisterState.Quit  == currentState)
                    {
                        uiManager.CloseMenu();
                        break;
                    }
                    else if ((int)RegisterState.MainMenu  == currentState)
                    {
                        uiManager.RunChosenOption();
                    }
                    else if ((int)RegisterState.LoadShoppingBasket  == currentState)
                    {
                        uiManager.RunChosenOption();
                        shoppingBasket   =  uiManager.GetBasket();
                    }
                    else if ((int)RegisterState.PrintReceipt  == currentState)
                    {
                        PrintReceit();
                        uiManager.RunChosenOption();
                    }
                    uiManager.ProcessInput();
                    currentState  =uiManager.GetCurrentState();
                }
            }
        }

        public void PrintReceit()
        {
            if (shoppingBasket   != null)
            {
                Console.Clear();
                decimal totalTax = 0;
                decimal totalPrice = 0;


                Console.WriteLine("\n");
                Console.WriteLine(" ****************************************");
                Console.WriteLine(" *              Sale receipt:           *");
                Console.WriteLine(" ****************************************");
                Console.WriteLine(" ");
                for (int i = 0; i < shoppingBasket.Count(); i++)
                {
                    Product? item = shoppingBasket.GetAt(i);
                    if (item  != null)
                    {
                        Console.Write(" 1 ");
                        Console.Write(item.Name);
                        Console.Write(" : ");
                        Console.Write((item.Price  +  item.ComputedTax).ToString("C2"));
                        Console.WriteLine("");
                        totalTax +=    item.ComputedTax;
                        totalPrice += (item.Price  +  item.ComputedTax);
                    }
                }
                Console.Write(" Sales Tax: ");
                Console.Write(totalTax.ToString("C2"));
                Console.WriteLine("");
                Console.Write(" Total: ");
                Console.Write(totalPrice.ToString("C2"));
                Console.WriteLine("");

                Console.WriteLine("");
                Console.Write(" Please, enter any key to continue: ");
                ConsoleKeyInfo isProcessCompleted = Console.ReadKey();
                Console.WriteLine("");
            }
        }


    }
}
