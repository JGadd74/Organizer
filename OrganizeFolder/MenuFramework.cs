using System;
using System.Collections.Generic;

namespace MenuFramework
{

    public enum MenuType
    {
        TextBody,
        NmbrChoices,
        TextInput,
        ScrollInput
    }
    public class Menu
    {
        //.....................................................................................................................
        //.....................................................................................................................
        /// <summary>
        ///  
        /// The first things you need to know:
        /// 1. When making a new menu you must pass it an int indicating the menu type
        ///     a. 0 text body only
        ///     b. 1 numbered choices
        ///     c. 2 text input menu
        ///     d. 3 scroll menu
        /// 2. Only one member MUST be set and that is the headerText, using setHeaderText(string input)
        ///     - this will be the header for your menu.
        ///     
        /// Once those two steps are complete you can either add text(if type 0) or your new methods!
        /// 
        /// 1. addMethod(yourMethodHere , "NameOfYourMethodHere")
        ///     - yourMethod must follow this signature: bool MyMethod() 
        ///         - i.e. Must be return type bool and take no parameters
        ///     - the second parameter is the label the menu will use to display your method
        /// 
        /// 2. SetBodyText(string) (string[]) (string, lineLengthTimesTen , indent)
        ///     - 3 overloads
        ///         - first will take raw string and print it
        ///         - second will take string[] and print each index as a line
        ///         - third will take string and create new lines after a set length and indent
        ///           the first line if indent = true
        ///            - length is in multiples of 10
        ///            
        /// 3. bool runMenu() (out string)
        ///     -2 overloads
        ///     - has return type bool so runMenu() can be added to menus
        ///         -however runMenu(out string) CANNOT. I'm working on this.
        ///     -If your menu is type 2, text input, you must use the overload (out string)
        ///         - ex.
        ///                 string ThisIsMyString; 
        ///                 myMenu.RunMenu(out thisIsMyString)
        ///                 // takes user input and assigns value to ThisIsMyString
        ///                 
        ///           note* the out parameter acts similiarly to a pointer
        ///      
        ///     
        ///     - all menuRun types except for type 2, text input, is set on a loop
        ///         -to exit a menu, addMethod(yourMenu.exitMenuLoop, "I give the fork up")
        ///         
        /// 4. Extra methods you may never need
        ///     a. void getHasRunArray(bool[number of methods in your menu])
        ///         -pass a bool array
        ///             -arrays in c# are passed by reference
        ///         -array is populated by boolean values corresponding to which methods have run successfully
        ///             -success is defined by the user when creating their method
        ///                 - return value of user method defines success
        ///     b. bool haveAllRun() 
        ///         -returns true if all user methods have run successfully.
        ///         -returns false if... well you know the rest
        /// 
        ///  5. quick tutorial
        ///  
        ///     Menu myNewMenu = new Menu(0); //txt display
        ///     myNewMenu.setHeader("Woo look at me");
        ///     myNewMenu.setBodyText("This will be broken into lines of 10 chars for me" , 1 , false);
        ///          
        ///     Menu MainMenu = new Menu(3); // scroll display, change to 1 for numerical choices
        ///     MainMenu.setHeader("Main Menu");
        ///     MainMenu.addMethod(myNewMenu.runMenu, "Show Example");
        ///     MainMenu.addMethod(MainMenu.exitMenuLoop, "Quit");
        ///     MainMenu.runMenu();
        /// 
        /// 
        /// </summary>

        private string bodyText;
        private int type; // 0 display bodyText, 1 display choices and take input
        public delegate bool choiceDelegate(); // signature of possible functions
        private List<choiceDelegate> userMethods = new List<choiceDelegate>();
        private List<bool> hasRunList = new List<bool>();

        public Menu(MenuType type, string Header) // 0 for bodyText display, 1 for numbered options , 2 for string input, 3 for scroll input
        {
            this.type = (int)type;
            headerPrompt = Header;
        }

        private string headerPrompt;
        private string[] choiceLabel = new string[20]; // display label for choices 
        private int currentScrollSelection = 0;
        private char selectedSymbol = ':';
        private char boundrySymbol = '|';

        public void SetHeaderPrompt(string input)
        {
            if (input.Length < 100) // arbitary number, need to adjust after testing
            {
                headerPrompt = input;
            }
            else throw new Exception("Header Prompts must be less than 15 characters long.");
        }
        private void GenerateNonScrollHeader()
        {
            string border = "";
            int currentLength = 0;
            int maxLength = 0;
            for (int i = 0; i < headerPrompt.Length; i++) // find longest line
            {
                currentLength++;
                if (headerPrompt[i] == '\n')
                {
                    currentLength = 0;
                }
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                }
            }

            for (int k = 0; k < maxLength; k++) // create border no longer than longest line
            {
                border += '=';
            }
            char newline = '\n';
            Console.WriteLine(border + newline + headerPrompt + newline + border);
        }
        private void GenerateScrollHeader()
        {
            string border = "";
            int currentLength = 0;
            int maxLength = 0;
            for (int i = 0; i < headerPrompt.Length; i++) // find longest line
            {
                currentLength++;
                if (headerPrompt[i] == '\n')
                {
                    currentLength = 0;
                }
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                }
            }
            string spacer = "";
            for (int k = 0; k < (maxLength * 3) - headerPrompt.Length; k++) // create border no longer than longest line
            {
                border += '=';
                if (k < ((maxLength) / 2)) spacer += ' ';
            }
            char newline = '\n';
            Console.WriteLine(border + newline + spacer + headerPrompt + newline + border);
        }
        private void displayMenu() // 
        {
            if (type != 3) GenerateNonScrollHeader();
            else GenerateScrollHeader();
            switch (type)
            {
                case 0://bodyText 
                    Console.WriteLine(bodyText + "\n");
                    Console.ReadLine();
                    KeepRunning = false;
                    break;
                case 1://numberedChoices
                    for (int x = 0; x < userMethods.Count; x++)
                    {
                        Console.WriteLine("{0}: {1}", x, choiceLabel[x]);
                    }
                    break;
                case 3: //scroll
                    string currentLine = ""; //generate line then add to final
                    string displayedMenu = ""; //final
                    int menuWidth = scrollMenuWidth;

                    for (int i = 0; i < userMethods.Count; i++)
                    {
                        int labelLength = choiceLabel[i].Length;
                        string spaces = "";
                        int paddingSize = (menuWidth - labelLength) / 2;
                        string adjustedSpaces = ""; // used to even out lines with odd lengthed titles
                        for (int l = 0; l <= paddingSize; l++) // generate spaces string
                        {
                            spaces += ' ';
                            if (labelLength % 2 != 0) // if label length is odd then shorten adjustedSpaces by 1
                            {
                                if (l <= paddingSize - 1)
                                {
                                    adjustedSpaces += ' ';
                                }
                            }
                            else adjustedSpaces += ' ';
                        }
                        if (i == currentScrollSelection) //selected
                        {
                            currentLine += boundrySymbol + spaces + selectedSymbol + choiceLabel[i] + selectedSymbol + adjustedSpaces + boundrySymbol;
                        }
                        else //basic format
                        {
                            currentLine += boundrySymbol + spaces + ' ' + choiceLabel[i] + adjustedSpaces + ' ' + boundrySymbol;
                        }
                        currentLine += '\n';
                        displayedMenu += currentLine;
                        currentLine = "";
                    }
                    Console.WriteLine(displayedMenu);
                    //Console.WriteLine(currentScrollSelection);
                    break;
            }
        }
        private string getStringInput()
        {
            string input = Console.ReadLine();
            return input;
        }
        private void getScrollInput()
        {
            bool hasRun = false;
            while (!hasRun)
            {// get input until 'enter' is detected
                //then execute selected method
                ConsoleKeyInfo userInput = Console.ReadKey();
                switch (userInput.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (currentScrollSelection < userMethods.Count - 1)
                        {
                            currentScrollSelection++;
                            Console.Clear();
                            displayMenu();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (currentScrollSelection > 0)
                        {
                            currentScrollSelection--;
                            Console.Clear();
                            displayMenu();
                        }
                        break;

                    case ConsoleKey.Enter:
                        choiceDelegate selection = userMethods[currentScrollSelection];
                        hasRunList[currentScrollSelection] = selection();
                        hasRun = true;
                        break;
                }
            }
        }
        private void getIntInput() // gets input and converts to int
        {
            try
            {
                int input;
                switch (type)
                {
                    case 0:
                        input = Convert.ToInt16(Console.ReadLine());
                        break;
                    case 1:
                        while (true)
                        {
                            input = Convert.ToInt16(Console.ReadLine());
                            if (input < userMethods.Count)//prevent invalid selections
                            {
                                break;
                            }
                            // Console.Clear();  // UNNECCESARY?
                            // displayMenu();   //
                        }
                        choiceDelegate Choice = userMethods[input];
                        Console.Clear();
                        hasRunList[input] = Choice();
                        break;
                    case 2:
                        getStringInput();
                        break;
                }
            }
            catch (System.FormatException)
            {
                runMenu();
            }
        }
        public void SetBodyText(string input)
        {// raw string input
            bodyText = input;
        }
        public void SetBodyText(string[] byLIne) // currently causes conflicts
        {// generate string where each index is a new line of bodyText
            string temp = null;
            for (int i = 0; i < byLIne.Length; i++)
            {
                temp += byLIne[i];
                temp += '\n';
            }
            bodyText = temp;
        }
        public void SetBodyText(string input, int lineLengthTimesTen, bool indent)
        {// auto format string into lines of lineLengthTimesTen length
            lineLengthTimesTen *= 10;
            string temp = null;
            if (indent == true)
            {
                temp += '\t';
            }

            int v = 0;
            for (int i = 0; i < input.Length; i++)
            {
                v++;
                temp += input[i];
                if (v >= lineLengthTimesTen && (input[i] == ' ' || input[i] == '\n'))
                { // create new line if length and char requirements are met
                    v = 0;
                    temp += '\n';
                }
            }
            bodyText = temp;
        }

        private int assignedChoices = 0;
        public void addMethod(choiceDelegate userMethod, string label) // userMethod must match delegate signature
        {
            if (type == 0)
            {
                throw new Exception("Cannot add methods to TextDisplay type menus.");
            }
            choiceLabel[assignedChoices] = label;
            if (type == 3)
            {
                findScrollMenuWidth(label);
            }
            assignedChoices++;
            hasRunList.Add(false);
            userMethods.Add(userMethod);
        }

        private int scrollMenuWidth = 0;
        private void findScrollMenuWidth(string input)
        {
            int tLength = input.Length;
            if (tLength % 2 == 0) tLength++;
            if (tLength > scrollMenuWidth)
            {
                scrollMenuWidth = tLength;
            }
        }
        public void getHasRunArray(bool[] boolsList)
        {
            for (int i = 0; i < hasRunList.Count; i++)
            {
                boolsList[i] = hasRunList[i];
            }
        }
        public bool haveAllRun()
        {
            bool returnValue = false;
            for (int i = 0; i < hasRunList.Count; i++)
            {
                if (hasRunList[i] == false)
                {
                    break;
                }
                else if (i == hasRunList.Count - 1)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        private bool KeepRunning = true;
        public bool exitMenuLoop()
        {
            KeepRunning = false;
            return true;
        }

        public bool runMenu()
        {
            hasRunList.TrimExcess();
            KeepRunning = true;
            while (KeepRunning)
            {
                Console.Clear();
                displayMenu();
                if (type == 3)
                {
                    getScrollInput();
                }
                else if (type == 0)
                {
                }
                else getIntInput();
            }
            return true;
        }
        public bool runMenu(out string userInput)
        {
            hasRunList.TrimExcess();
            Console.Clear();
            displayMenu();
            userInput = getStringInput();
            return true;
        }
    }
}

