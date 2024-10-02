using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    internal class Board
    {
        private const char k_FirstLetterOfAlphabet = 'A';
        private readonly Dictionary<string, Card> r_CurrentGameBoard = new Dictionary<string, Card>();
        private readonly Random r_Randomizer = new Random();
        private readonly int r_BoardHeight, r_BoardWidth;
        private int m_NumberOfPairsUnopened; 

        internal Board(int i_BoardHeight, int i_BoardWidth)
        {
            r_BoardHeight = i_BoardHeight;
            r_BoardWidth = i_BoardWidth;
            m_NumberOfPairsUnopened = (i_BoardHeight * i_BoardWidth) / 2;
            generateGameBoard();
        }

        private void generateGameBoard()
        {
            List<string> positions = generatePositionList();
            List<int> randomizedPairs = generateRandomizedPairsList();
            for(int i = 0; i < randomizedPairs.Count; i++)
            {
                r_CurrentGameBoard[positions[i]] = new Card(randomizedPairs[i], positions[i]);
            }
        }

        private List<int> generateRandomizedPairsList()
        {
            List<int> pairs = new List<int>();

            for(int i = 1; i <= m_NumberOfPairsUnopened; i++)
            {
                pairs.Add(i);
                pairs.Add(i);
            }

            return pairs.OrderBy(x => r_Randomizer.Next()).ToList();
        }

        private List<string> generatePositionList()
        {
            List<string> positions = new List<string>();

            for(char i = k_FirstLetterOfAlphabet; i < k_FirstLetterOfAlphabet + r_BoardWidth ; i++)
            {
                for(int j = 1; j <= r_BoardHeight; j++)
                {
                    positions.Add(string.Format("{0}{1}", i, j));
                }
            }

            return positions;
        }

        internal List<string> GetPositionsList()
        {
            return r_CurrentGameBoard.Keys.ToList();
        }

        internal Card GetCardInPosition(string i_BoardPos)
        {
            return r_CurrentGameBoard[i_BoardPos];
        }

        internal bool AreAllPairsOpened()
        {
            return m_NumberOfPairsUnopened == 0;
        }

        internal void RemovePairFromBoard(Card i_FirstCard, Card i_SecondCard)
        {
            m_NumberOfPairsUnopened--;
        }

        internal bool IsPositionLegal(string i_CardLocation)
        {
            return r_CurrentGameBoard.ContainsKey(i_CardLocation);
        }

        internal Dictionary<string, int> GetInitialBoardState()
        {
            Dictionary<string, int> initialState = new Dictionary<string, int>();

            foreach(var position in r_CurrentGameBoard.Keys)
            {
                initialState[position] = r_CurrentGameBoard[position].Symbol;
            }

            return initialState;
        }
    }
}
