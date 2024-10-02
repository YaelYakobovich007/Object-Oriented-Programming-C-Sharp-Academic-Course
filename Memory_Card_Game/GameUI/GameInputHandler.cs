using GameLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameUI
{ 
    internal class GameInputHandler
    {
        private const int k_MinBoardDimension = 4;
        private const int k_MaxBoardDimension = 6;
        private const string k_EndGameInput = "Q";
        private const string k_YesResponse = "Y";
        private const string k_NoResponse = "N";
        private const int k_CorrectInputPositionFormatLength = 2;
        private const string k_TwoPlayersGameOptionNumber = "1";
        private const string k_GameAgainstComputerOptionNumber = "2"; 

        internal string GetInputFromUser(string i_CurrentPlayerName, SingleGameManager i_SingleGameManager, out bool o_UserRequestedQuit)
        {
            bool validInput;
            string userChoice;
            StringBuilder promptMessageBuilder = new StringBuilder();

            promptMessageBuilder.AppendFormat("{0}'s turn, please choose a card to flip{1}", i_CurrentPlayerName, Environment.NewLine);
            do
            {
                Console.WriteLine(promptMessageBuilder.ToString());
                userChoice = Console.ReadLine().ToUpper();
                validInput = isInputValid(userChoice, i_SingleGameManager, out o_UserRequestedQuit);
            } while(!validInput);

            return userChoice;
        }

        private bool isInputValid(string i_CardPosition, SingleGameManager i_SingleGameManager, out bool o_UserRequestedQuit)
        {
            bool validInput;
            o_UserRequestedQuit = false;

            if(i_CardPosition == k_EndGameInput)
            {
                validInput = true;
                o_UserRequestedQuit = true; 
            }
            else if(i_CardPosition.Length != k_CorrectInputPositionFormatLength || !char.IsLetter(i_CardPosition[0]) || !char.IsDigit(i_CardPosition[1]))
            {
                Console.WriteLine("Input is not at the correct format: <Letter><digit>, try again");
                validInput = false;
            }
            else if(!i_SingleGameManager.IsInputValid(i_CardPosition))
            {
                Console.WriteLine("The chosen place on board is already does not exist or is already flipped, try again");
                validInput = false;
            }
            else
            {
                validInput = true;
            }

            return validInput;
        }

        private bool isValidContinueInput(string i_UserResponse)
        {
            i_UserResponse = i_UserResponse.ToUpper();

            return i_UserResponse.Equals(k_YesResponse) || i_UserResponse.Equals(k_NoResponse);
        }

        internal bool AskForAnotherGame()
        {
            StringBuilder closingSentence = new StringBuilder();
            StringBuilder inValidInputSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            bool validInput = false;
            string userResponse = string.Empty;

            closingSentence.AppendFormat("Would you like to play again?{0}", Environment.NewLine);
            inValidInputSentence.AppendFormat("Input is not valid, please choose only one of the following options:{0}", Environment.NewLine);
            optionsSentence.Append("please type 'Y' for Yes or 'N' for No");
            closingSentence.Append(optionsSentence);
            inValidInputSentence.Append(optionsSentence);
            Console.WriteLine(closingSentence);
            while(!validInput)
            {
                userResponse = Console.ReadLine().ToUpper();
                validInput = isValidContinueInput(userResponse);
                if (!validInput)
                {
                    Console.WriteLine(inValidInputSentence);
                }
            }

            return userResponse.Equals(k_YesResponse);
        }

        public void GetBoardDimensionsFromUser(out int o_BoardHeight, out int o_BoardWidth)
        {
            bool validInput;

            do
            {
                o_BoardHeight = getDimensionInputFromUser("height");
                o_BoardWidth = getDimensionInputFromUser("width");
                if((o_BoardHeight * o_BoardWidth) % 2 == 0)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Board must have an even number of squares, try again");
                    validInput = false;
                }
            } while(!validInput);
        }

        private int getDimensionInputFromUser(string i_CurrentInputType)
        {
            StringBuilder promptMessageBuilder = new StringBuilder();
            int validHeight;
            bool validInput;

            promptMessageBuilder.AppendFormat("Enter the board's {0} (number between 4-6){1}", i_CurrentInputType, Environment.NewLine);
            do
            {
                Console.WriteLine(promptMessageBuilder);
                string userInput = Console.ReadLine();

                if(int.TryParse(userInput, out validHeight) && (validHeight >= k_MinBoardDimension && validHeight <= k_MaxBoardDimension))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Input is not a number between 4 - 6, try again"); 
                    validInput = false;
                }
            } while(!validInput);

            return validHeight;
        }

        internal void GetPlayersFromUser(out List<string> o_PlayerNames, out List<string> o_ComputerNames, out bool o_IsPlayerVsComputer)
        {
            bool validInput;
            string userInput;
            StringBuilder optionsMessageBuilder = new StringBuilder();
            o_PlayerNames = new List<string>();
            o_ComputerNames = new List<string>();

            optionsMessageBuilder.AppendLine("Choose one of the following options by entering the corresponding number: ");
            optionsMessageBuilder.AppendLine("1. Two players game");
            optionsMessageBuilder.AppendLine("2. A game against the computer");
            Console.WriteLine("Enter your name: ");
            o_PlayerNames.Add(Console.ReadLine());
            do
            {
                Console.WriteLine(optionsMessageBuilder);
                userInput = Console.ReadLine();
                validInput = userInput.Equals(k_TwoPlayersGameOptionNumber) || userInput.Equals(k_GameAgainstComputerOptionNumber);
                if(!validInput)
                {
                    Console.WriteLine("Invalid option, try again");
                }
            } while(!validInput);

            if(userInput == k_TwoPlayersGameOptionNumber)
            {
                o_IsPlayerVsComputer = false;
                Console.WriteLine("Enter the other player's name: ");
                o_PlayerNames.Add(Console.ReadLine());
            }
            else
            {
                o_IsPlayerVsComputer = true;
                o_ComputerNames.Add("Computer#1");
            }
        }
    }
}
