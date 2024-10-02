using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GameLogic
{
    public class SingleGameManager
    {
        private readonly Board r_BoardGame;
        private readonly List<Player> r_HumanPlayers;
        private readonly List<ComputerAi> r_ComputerAis;
        private int m_CurrentPlayerIndex;
        private ePlayerType m_CurrentPlayerType;

        private enum ePlayerType
        {
            Human,
            ComputerAi,
        }

        public List<List<(string firstCardPosition, string secondCardPosition)>> CurrentRoundComputersChoices { get; private set; } 

        public SingleGameManager(List<string> i_HumanPlayersNames, List<string> i_ComputerNames, int i_BoardHeight, int i_BoardWidth)
        {
            CurrentRoundComputersChoices = new List<List<(string firstCard, string secondCard)>>();
            m_CurrentPlayerIndex = 0;
            m_CurrentPlayerType = i_HumanPlayersNames.Count == 0 ? ePlayerType.ComputerAi : ePlayerType.Human;
            r_BoardGame = new Board(i_BoardHeight, i_BoardWidth);
            r_HumanPlayers = new List<Player>();
            foreach(string name in i_HumanPlayersNames)
            {
                r_HumanPlayers.Add(new Player(name));
            }

            r_ComputerAis = new List<ComputerAi>();
            foreach(string name in i_ComputerNames)
            {
                r_ComputerAis.Add(new ComputerAi(name, ComputerAi.eDifficulty.Medium, r_BoardGame.GetPositionsList()));
            }
        }

        private bool endOfTurnLogic(Card i_FirstCardFlippedInTurn, Card i_SecondCardFlippedInTurn)
        {
            if(i_FirstCardFlippedInTurn.Symbol == i_SecondCardFlippedInTurn.Symbol)
            {
                r_BoardGame.RemovePairFromBoard(i_FirstCardFlippedInTurn, i_SecondCardFlippedInTurn);
                if(m_CurrentPlayerType == ePlayerType.Human)
                {
                    r_HumanPlayers[m_CurrentPlayerIndex].Points++;
                }
                else if(m_CurrentPlayerType == ePlayerType.ComputerAi)
                {
                    r_ComputerAis[m_CurrentPlayerIndex].Points++;
                }

                foreach(ComputerAi computerAi in r_ComputerAis)
                {
                    computerAi.RemoveFromAiMemory(i_FirstCardFlippedInTurn, i_SecondCardFlippedInTurn);
                }
            }
            else
            {
                i_FirstCardFlippedInTurn.Flip();
                i_SecondCardFlippedInTurn.Flip();
                foreach(ComputerAi computerAi in r_ComputerAis)
                {
                    computerAi.AddToAiMemory(i_FirstCardFlippedInTurn, i_SecondCardFlippedInTurn);
                }

                setNextPlayer();
            }

            return r_BoardGame.AreAllPairsOpened();
        }

        public bool EndOfPlayerTurn(string i_FirstCardPosition, string i_SecondCardPosition)
        {
            bool isGameOver = false;
            Card firstCard = r_BoardGame.GetCardInPosition(i_FirstCardPosition);
            Card secondCard = r_BoardGame.GetCardInPosition(i_SecondCardPosition);

            if(m_CurrentPlayerType == ePlayerType.Human)
            {
                isGameOver = endOfTurnLogic(firstCard, secondCard); 
            }

            CurrentRoundComputersChoices.Clear();
            bool didCardsMatch = false;

            while(m_CurrentPlayerType == ePlayerType.ComputerAi && !isGameOver)
            {
                if(!didCardsMatch)
                {
                    CurrentRoundComputersChoices.Add(new List<(string firstCard, string secondCard)>());
                }

                didCardsMatch = playComputerAiTurn(out isGameOver);
            }
            
            return isGameOver; 
        }

        private bool playComputerAiTurn(out bool o_IsGameOver)
        {
            Card firstCard = r_ComputerAis[m_CurrentPlayerIndex].AiChoice(null, r_BoardGame);
            Card secondCard = r_ComputerAis[m_CurrentPlayerIndex].AiChoice(firstCard, r_BoardGame);

            CurrentRoundComputersChoices[m_CurrentPlayerIndex].Add((firstCard.Position, secondCard.Position));
            FlipCard(firstCard.Position);
            FlipCard(secondCard.Position);
            o_IsGameOver = endOfTurnLogic(firstCard, secondCard);

            return firstCard.Symbol == secondCard.Symbol;
        }

        private void setNextPlayer()
        {
            m_CurrentPlayerIndex++;
            if (m_CurrentPlayerType == ePlayerType.Human)
            {
                if(m_CurrentPlayerIndex == r_HumanPlayers.Count)
                {
                    m_CurrentPlayerIndex = 0;
                    if (r_ComputerAis.Count > 0)
                    {
                        m_CurrentPlayerType = ePlayerType.ComputerAi;
                    }
                }
            }
            else
            {
                if (m_CurrentPlayerIndex == r_ComputerAis.Count)
                {
                    m_CurrentPlayerIndex = 0;
                    if (r_HumanPlayers.Count > 0)
                    {
                        m_CurrentPlayerType = ePlayerType.Human;
                    }
                }
            }
        }

        public void FlipCard(string i_CardPosition)
        {
            r_BoardGame.GetCardInPosition(i_CardPosition).Flip();
        }

        public bool IsInputValid(string i_CardPosition)
        {
            return r_BoardGame.IsPositionLegal(i_CardPosition) && !r_BoardGame.GetCardInPosition(i_CardPosition).IsFlipped;
        }
       
        public void GetWinners(out List<string> o_WinnerNames, out int o_WinnerScore)
        {
            o_WinnerNames = new List<string>();
            o_WinnerScore = -1;
            int highestScore = -1;

            foreach(Player player in r_HumanPlayers)
            {
                if(player.Points > highestScore)
                {
                    highestScore = player.Points;
                    o_WinnerNames.Clear();
                    o_WinnerNames.Add(player.Name);
                    o_WinnerScore = highestScore;
                }
                else if(player.Points == highestScore)
                {
                    o_WinnerNames.Add(player.Name);
                }
            }

            foreach(ComputerAi computerAi in r_ComputerAis)
            {
                if(computerAi.Points > highestScore)
                {
                    highestScore = computerAi.Points;
                    o_WinnerNames.Clear();
                    o_WinnerNames.Add(computerAi.Name);
                    o_WinnerScore = highestScore;
                }
                else if(computerAi.Points == highestScore)
                {
                    o_WinnerNames.Add(computerAi.Name);
                }
            }
        }

        public List<(string Name, int Points)> GetPlayerScores()
        {
            List<(string Name, int Points)> scores = new List<(string Name, int Points)>();

            foreach(Player player in r_HumanPlayers)
            {
                scores.Add((player.Name, player.Points));
            }

            foreach(ComputerAi ai in r_ComputerAis)
            {
                scores.Add((ai.Name, ai.Points));
            }

            return scores.OrderByDescending(x => x.Points).ToList();
        }

        public Dictionary<string, int> GetInitialBoardState()
        {
            return r_BoardGame.GetInitialBoardState();
        }
    }
}
