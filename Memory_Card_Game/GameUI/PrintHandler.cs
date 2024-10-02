using System;
using System.Collections.Generic;
using System.Text;
using GameLogic;

namespace GameUI
{
    internal class PrintHandler
    {
        private const char k_FirstLetterOfAlphabet = 'A';
        private readonly int r_BoardHeight;
        private readonly int r_BoardWidth;

        internal PrintHandler(int i_BoardHeight, int i_BoardWidth)
        {
            r_BoardHeight = i_BoardHeight;
            r_BoardWidth = i_BoardWidth;
        }

        internal void PrintBoard(SingleGameManager i_GameManager, Dictionary<string, UICard> i_UIBoard)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            printTableHeader();
            for(int i = 0; i < r_BoardHeight; i++)
            {
                printEqualLine();
                printLine(i + 1, i_GameManager, i_UIBoard);
            }

            printEqualLine();
        }

        private void printEqualLine()
        {
            StringBuilder equalLineBuilder = new StringBuilder();

            equalLineBuilder.Append("   ");
            for(int i = 0; i < r_BoardWidth; i++)
            {
                equalLineBuilder.Append("====");
            }

            equalLineBuilder.AppendLine("=");
            Console.WriteLine(equalLineBuilder.ToString());
        }

        private void printTableHeader()
        {
            StringBuilder headerBuilder = new StringBuilder();

            headerBuilder.Append("    ");
            for(int i = k_FirstLetterOfAlphabet; i < k_FirstLetterOfAlphabet + r_BoardWidth; i++)
            {
                headerBuilder.AppendFormat("{0}   ", (char)i);
            }

            headerBuilder.AppendLine();
            Console.WriteLine(headerBuilder.ToString());
        }

        private void printLine(int i_LineNumber, SingleGameManager i_GameManager, Dictionary<string, UICard> i_UIBoard)
        {
            StringBuilder lineBuilder = new StringBuilder();
            lineBuilder.Append(i_LineNumber.ToString() + " ");
            for(int i = 0; i < r_BoardWidth; i++)
            {
                string position = string.Format("{0}{1}", (char)(k_FirstLetterOfAlphabet + i), i_LineNumber);
                char symbol = i_UIBoard[position].Symbol;
                if(i_UIBoard[position].IsFlipped)
                {
                    lineBuilder.AppendFormat("| {0} ", symbol);
                }
                else
                {
                    lineBuilder.Append("|   ");
                }
            }

            lineBuilder.AppendLine("|");
            Console.WriteLine(lineBuilder.ToString());
        }

        internal void AnnounceWinner(SingleGameManager i_SingleGame)
        {
            StringBuilder messageBuilder = new StringBuilder();

            Ex02.ConsoleUtils.Screen.Clear();
            printPlayerScores(i_SingleGame);
            i_SingleGame.GetWinners(out List<string> winnerNames, out int winnerScore);
            Console.WriteLine("=====================================");
            if(winnerNames.Count == 1)
            {
                messageBuilder.AppendFormat("The winner is {0} with a score of {1} points!", winnerNames[0], winnerScore);
            }
            else
            {
                messageBuilder.AppendLine("It's a tie! The winners are:");
                foreach(string winnerName in winnerNames)
                {
                    messageBuilder.AppendFormat("{0} with a score of {1} points{2}", winnerName, winnerScore, Environment.NewLine);
                }
            }

            Console.WriteLine(messageBuilder.ToString());
        }

        private void printPlayerScores(SingleGameManager i_SingleGame)
        {
            List<(string Name, int Points)> playerScores = i_SingleGame.GetPlayerScores();
            StringBuilder scoreBuilder = new StringBuilder();

            Console.WriteLine("Game Over! Here are the final scores:");
            Console.WriteLine("=====================================");
            foreach ((string name, int points) in playerScores)
            {
                scoreBuilder.AppendFormat("{0}: {1} points{2}", name, points, Environment.NewLine);
            }

            Console.WriteLine(scoreBuilder.ToString());
        }
    }
}
