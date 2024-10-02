using GameLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameUI
{
    internal class GameManagerUI 
    {
        private const char k_FirstLetterOfTheAlphabet = 'A';
        private const int k_TwoPlayers = 2;
        private readonly GameInputHandler r_GameInputHandler;
        private PrintHandler m_PrintHandler;
        private int m_BoardHeight, m_BoardWidth;
        private readonly List<string> r_PlayerNames;
        private readonly List<string> r_ComputerNames;
        private Dictionary<string, UICard> m_UIBoard;
        private readonly bool r_IsPlayingAgainstComputer;
        private SingleGameManager m_SingleGameManager;

        internal GameManagerUI()
        {
            r_GameInputHandler = new GameInputHandler();
            r_GameInputHandler.GetPlayersFromUser(out r_PlayerNames, out r_ComputerNames, out r_IsPlayingAgainstComputer);
        }

        internal void RunGame()
        {
            bool playAnotherGame = true;
            bool userRequestedExit; 

            while (playAnotherGame)
            {
                setupNewGame();
                userRequestedExit = playSingleGame();
                if (!userRequestedExit)
                {
                    m_PrintHandler.AnnounceWinner(m_SingleGameManager);
                    playAnotherGame = r_GameInputHandler.AskForAnotherGame();
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Thank you for playing!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private void setupNewGame()
        {
            r_GameInputHandler.GetBoardDimensionsFromUser(out m_BoardHeight, out m_BoardWidth);
            m_SingleGameManager = new SingleGameManager(r_PlayerNames, r_ComputerNames, m_BoardHeight, m_BoardWidth);
            initializeUiBoard(m_SingleGameManager);
            m_PrintHandler = new PrintHandler(m_BoardHeight, m_BoardWidth);
        }

        private void initializeUiBoard(SingleGameManager i_SingleGameManager)
        {
            m_UIBoard = new Dictionary<string, UICard>();
            Dictionary<string, int> initializedLogicalBoard = i_SingleGameManager.GetInitialBoardState();

            foreach (var pair in initializedLogicalBoard)
            {
                m_UIBoard[pair.Key] = new UICard((char)(pair.Value + k_FirstLetterOfTheAlphabet));
            }
        }

        private bool playSingleGame()
        {
            string currentPlayerName = r_PlayerNames[0];
            bool isGameOver = false;
            bool userRequestedExit = false;

            m_PrintHandler.PrintBoard(m_SingleGameManager, m_UIBoard);
            while(!isGameOver)
            {
                if(!selectAndFlipPlayerCard(currentPlayerName, out string playerFirstPositionCard, out userRequestedExit))
                {
                    break;
                }

                if(!selectAndFlipPlayerCard(currentPlayerName, out string playerSecondPositionCard, out userRequestedExit))
                {
                    break;
                }

                Thread.Sleep(2000);
                isGameOver = m_SingleGameManager.EndOfPlayerTurn(playerFirstPositionCard, playerSecondPositionCard);
                if(m_UIBoard[playerFirstPositionCard].Symbol != m_UIBoard[playerSecondPositionCard].Symbol)
                {
                    flipBackCardsOnUiBoard(playerFirstPositionCard, playerSecondPositionCard);
                    setNextPlayerName(ref currentPlayerName);
                    if(r_IsPlayingAgainstComputer)
                    {
                        simulateComputerTurn();
                    }
                }
            }

            return userRequestedExit; 
        }

        private bool selectAndFlipPlayerCard(string i_CurrentPlayerName, out string o_SelectedCardPosition, out bool o_UserRequestedExit)
        {
            bool moveProcessedSuccessfully = true;
            o_SelectedCardPosition = r_GameInputHandler.GetInputFromUser(i_CurrentPlayerName, m_SingleGameManager, out o_UserRequestedExit);

            if(o_UserRequestedExit)
            {
                moveProcessedSuccessfully=false;
            }
            else
            {
                m_SingleGameManager.FlipCard(o_SelectedCardPosition);
                flipCardOnUiBoard(o_SelectedCardPosition);
            }

            return moveProcessedSuccessfully;
        }

        private void flipCardOnUiBoard(string i_CardPositionToFlip)
        {
            m_UIBoard[i_CardPositionToFlip].Flip();
            m_PrintHandler.PrintBoard(m_SingleGameManager, m_UIBoard);
        }

        private void flipBackCardsOnUiBoard(string i_FirstCardPosition, string i_SecondCardPosition)
        {
            m_UIBoard[i_FirstCardPosition].Flip();
            m_UIBoard[i_SecondCardPosition].Flip();
            m_PrintHandler.PrintBoard(m_SingleGameManager, m_UIBoard);
        }

        private void simulateComputerTurn()
        {
            StringBuilder promptMessageBuilder = new StringBuilder();

            promptMessageBuilder.AppendFormat("{0}'s turn {1}", r_ComputerNames[0], Environment.NewLine);
            foreach (var pairChosen in m_SingleGameManager.CurrentRoundComputersChoices[0])
            {
                flipCardOnUiBoard(pairChosen.firstCardPosition);
                Console.WriteLine(promptMessageBuilder.ToString());
                Thread.Sleep(2000);
                flipCardOnUiBoard(pairChosen.secondCardPosition);
                Console.WriteLine(promptMessageBuilder.ToString());
                Thread.Sleep(2000);
                if (m_UIBoard[pairChosen.firstCardPosition].Symbol != m_UIBoard[pairChosen.secondCardPosition].Symbol)
                {
                    flipBackCardsOnUiBoard(pairChosen.firstCardPosition, pairChosen.secondCardPosition);
                }
            }
        } 

        private void setNextPlayerName(ref string io_CurrentPlayerName)
        {
            if(r_PlayerNames.Count == k_TwoPlayers)
            {
                io_CurrentPlayerName = r_PlayerNames[0] == io_CurrentPlayerName ? r_PlayerNames[1] : r_PlayerNames[0];
            }
        }
    }
}
