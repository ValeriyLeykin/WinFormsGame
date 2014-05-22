using System;
using System.Collections.Generic;
using System.Text;

namespace B14_Ex05_Logic
{
    public class GameManager
    {
        private const int k_NumberOfCardsToSelectPerTurn = 2;
        private const int k_NumberOfPlayersInGame = 2;
        private readonly MemoryGameBoard r_GameBoard;
        private readonly ComputerSelectionLogic r_ComputerSelectionLogic;
        private readonly int r_MinDimensionSize = 4;
        private readonly int r_MaxDimensionSize = 6;
        private readonly Player[] r_Players;
        private int m_CurrentPlayerIndex;
        
        private CardLocation[] m_CurrentTurnSelectedCards = new CardLocation[k_NumberOfCardsToSelectPerTurn];

        public GameManager()
        {
            r_Players = new Player[k_NumberOfPlayersInGame];
            r_GameBoard = new MemoryGameBoard();
            r_ComputerSelectionLogic = new ComputerSelectionLogic();
        }

        public void StartNewGame(string i_NamePlayerA, bool i_IsComputerPlayerA, string i_NamePlayerB, bool i_IsComputerPlayerB, int i_BoardHeight, int i_BoardWidth)
        {
            r_Players[0] = new Player(i_NamePlayerA, i_IsComputerPlayerA);
            r_Players[1] = new Player(i_NamePlayerB, i_IsComputerPlayerB);
            m_CurrentPlayerIndex = 0;

            // there is really no point in calling IsBoardHeightValid(), IsBoardWidthValid(), IsBoardDimensionsValid() here because we can't throw exceptions yet. and... i know this is a comment... but this is just for you!
            r_GameBoard.Init(i_BoardHeight, i_BoardWidth);
        }
        
        public bool IsBoardHeightValid(int i_BoardDimension)
        {
            return (i_BoardDimension >= r_MinDimensionSize) && (i_BoardDimension <= r_MaxDimensionSize);
        }

        public bool IsBoardWidthValid(int i_BoardDimension)
        {
            return (i_BoardDimension >= r_MinDimensionSize) && (i_BoardDimension <= r_MaxDimensionSize);
        }

        public bool IsBoardDimensionsValid(int i_Height, int i_Width)
        {
            return r_GameBoard.IsBoardValid(i_Height, i_Width);
        }

        public int BoardHeight
        {
            get
            {
                return r_GameBoard.Height;
            }
        }

        public int BoardWidth
        {
            get
            {
                return r_GameBoard.Width;
            }
        }

        public void GetPlayersData(out string o_FirstPlayerName, out int o_FirstPlayerScore, out string o_SecondPlayerName, out int o_SecondPlayerScore)
        {
            o_FirstPlayerName = r_Players[0].Name;
            o_FirstPlayerScore = r_Players[0].GamePoints;

            o_SecondPlayerName = r_Players[1].Name;
            o_SecondPlayerScore = r_Players[1].GamePoints;
        }
        
        public bool IsGameFinished
        {
          get { return r_GameBoard.IsFullyRevealed; }
        }    

        public char? GetCharAt(CardLocation i_CardLocation)
        {
            return r_GameBoard.GetCharacterAt(i_CardLocation);
        }

        internal void ApplyCardSelection(CardLocation i_CardSelection, eCardToRevealOrderal i_CardToRevealOrderal)
        {
            m_CurrentTurnSelectedCards[(int)i_CardToRevealOrderal] = i_CardSelection;
            r_GameBoard.Reveal(i_CardSelection);
        }

        public bool CheckIfCardExistsInBoard(CardLocation i_CardLocation)
        {
            return r_GameBoard.IsCardLocationInsideBoard(i_CardLocation);
        }

        public bool CheckIfCardIsAlreadyRevealed(CardLocation i_CardLocation)
        {
            return r_GameBoard.IsCardVisibile(i_CardLocation);
        }

        internal bool CheckCardsSelectionMatch()
        {
            bool isMatch = r_GameBoard.AreCardsEqual(m_CurrentTurnSelectedCards[0], m_CurrentTurnSelectedCards[1]);
            if (isMatch)
            {
                this.r_Players[m_CurrentPlayerIndex].IncrementPoint();
            }
            else
            {
                r_GameBoard.Unreveal(m_CurrentTurnSelectedCards[0]);
                r_GameBoard.Unreveal(m_CurrentTurnSelectedCards[1]);
                m_CurrentPlayerIndex++;
                m_CurrentPlayerIndex = m_CurrentPlayerIndex % k_NumberOfPlayersInGame;
            }

            return isMatch;
        }

        public string CurrentPlayerName 
        { 
            get { return this.r_Players[m_CurrentPlayerIndex].Name; } 
        }

        public CardLocation GetCardLocationToRevealFromComputer()
       {
            List<CardLocation> availiableCardLocations = r_GameBoard.GetListOfUnrevealedCards();
            CardLocation computerChousenCard = r_ComputerSelectionLogic.GetSelection(availiableCardLocations);
            return computerChousenCard;
        }

        public bool IsCurrentPlayerComputer 
        {
            get { return r_Players[m_CurrentPlayerIndex].IsComputer; }
        }
    }
}
