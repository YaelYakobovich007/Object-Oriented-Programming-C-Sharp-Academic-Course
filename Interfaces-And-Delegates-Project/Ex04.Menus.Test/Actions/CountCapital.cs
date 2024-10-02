using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test.Actions
{
    public class CountCapital : IActionSelect
    {
        public void Action()
        {
            const string k_OpeningSentence = "Please enter your sentence:";
            const string k_InvalidInput = "Something went wrong, please try again";
            string sentence;

            Console.WriteLine(k_OpeningSentence);
            while((sentence = Console.ReadLine()) == null)
            {
                Console.WriteLine(k_InvalidInput);
            }

            Console.WriteLine(string.Format("There are {0} capitals in your sentence.", countAmountOfCapitalLetters(sentence)));
        }

        private int countAmountOfCapitalLetters(string i_String)
        {
            int countCapitalLetters = 0;

            foreach(char ch in i_String)
            {
                if(char.IsUpper(ch))
                {
                    countCapitalLetters++; 
                }
            }

            return countCapitalLetters;
        }
    }
}