using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace B14_Ex05_Logic
{
    public class ConsoleUI //TODO - to remove  later
    {
        private const string k_ComputerName = "Computer";
        private const string k_QuitCharacter = "Q";
        private const char k_headerColumn = 'A';
        private readonly GameManager r_GameManager;

        public ConsoleUI()
        {
            r_GameManager = new GameManager();
        }

        public void RunGames()
        {
            bool wantToContinuePlaying = true;
            bool isPendingQuitRequest;
            string firstPlayerName;
            const bool v_isFirstPlayerComputer = false;
            string secondPlayerName;
            bool isSecondPlayerComputer;
            int boardHeight;
            int boardWidth;

            while (wantToContinuePlaying)
            {
                askUserForPlayersData(out firstPlayerName, out isSecondPlayerComputer, out secondPlayerName);
                getValidBoardDimensionsFromUser(out boardHeight, out boardWidth);

                r_GameManager.StartNewGame(firstPlayerName, v_isFirstPlayerComputer, secondPlayerName, isSecondPlayerComputer, boardHeight, boardWidth);
                runOneGame(out isPendingQuitRequest);
                if (isPendingQuitRequest)
                {
                    break;
                }

                Console.WriteLine("Do you want to play another game? type (y)es/(n)o");
                string startNewGameInput = Console.ReadLine();
                if ("Y" != startNewGameInput.ToUpper())
                {
                    wantToContinuePlaying = false;
                }
            }
        }

        private void runOneGame(out bool o_IsPendingQuitRequest)
        {
            bool isGameFinished = false;
            o_IsPendingQuitRequest = false;

            while (!r_GameManager.IsGameFinished)
            {
                string currentPlayerName = r_GameManager.CurrentPlayerName;
                CardLocation firstCardSelection;
                CardLocation secondCardSelection;
                getSelectionsFromRelevantPlayer(r_GameManager.IsCurrentPlayerComputer, out firstCardSelection, out secondCardSelection, out o_IsPendingQuitRequest);
                if (o_IsPendingQuitRequest)
                {
                    break;
                }

                bool IsSuccessfulReveal = r_GameManager.CheckCardsSelectionMatch();
                outputTurnResult(currentPlayerName, IsSuccessfulReveal, out o_IsPendingQuitRequest);
                if (o_IsPendingQuitRequest)
                {
                    break;
                }

                isGameFinished = r_GameManager.IsGameFinished;
            }
        }

        private void getSelectionsFromRelevantPlayer(bool i_IsComputer, out CardLocation o_FirstCardSelection, out CardLocation o_SecondCardSelection, out bool o_IsPendingQuitRequest)
        {
            o_IsPendingQuitRequest = false;
            o_SecondCardSelection = new CardLocation();
            if (i_IsComputer)
            {
                o_FirstCardSelection = r_GameManager.GetCardLocationToRevealFromComputer();
                r_GameManager.ApplyCardSelection(o_FirstCardSelection, eCardToRevealOrderal.First);

                o_SecondCardSelection = r_GameManager.GetCardLocationToRevealFromComputer();
                r_GameManager.ApplyCardSelection(o_SecondCardSelection, eCardToRevealOrderal.Second);
                //printGameBoard();
            }
            else
            {
                o_FirstCardSelection = getCardLocationToRevealFromUser(out o_IsPendingQuitRequest);
                if (!o_IsPendingQuitRequest)
                {
                    r_GameManager.ApplyCardSelection(o_FirstCardSelection, eCardToRevealOrderal.First);
                    //printGameBoard();

                    o_SecondCardSelection = getCardLocationToRevealFromUser(out o_IsPendingQuitRequest);
                    if (!o_IsPendingQuitRequest)
                    {
                        r_GameManager.ApplyCardSelection(o_SecondCardSelection, eCardToRevealOrderal.Second);
                        //printGameBoard();
                    }
                }
            }
        }

        private void outputGameResults()
        {
            string firstPlayerName;
            int firstPlayerScore;
            string secondPlayerName;
            int secondPlayerScore;

            r_GameManager.GetPlayersData(out firstPlayerName, out firstPlayerScore, out secondPlayerName, out secondPlayerScore);

            string winnerPlayerName = firstPlayerScore > secondPlayerScore ? firstPlayerName : secondPlayerName;

            Console.WriteLine(string.Format(
@"Good job {0} you won!
Final results:
Player {1} score: {2}
Player {3} score: {4}", 
                      winnerPlayerName, 
                      firstPlayerName, 
                      firstPlayerScore, 
                      secondPlayerName, 
                      secondPlayerScore));
        }

        private void outputTurnResult(string i_CurrentPlayerName, bool i_IsSuccessfulReveal, out bool o_IsPendingQuitRequest)
        {
            o_IsPendingQuitRequest = false;
            if (i_IsSuccessfulReveal)
            {
                Console.WriteLine(string.Format("Good job {0} you got 1 point!!", i_CurrentPlayerName));
                if (r_GameManager.IsGameFinished)
                {
                    outputGameResults();
                }
            }
            else
            {
                Console.WriteLine(string.Format("No worries {0}! maybe next time", i_CurrentPlayerName));
            }

            Console.WriteLine("Press enter to continue");
            string anyKeyPressed = Console.ReadLine();
            if (anyKeyPressed.ToUpper() == k_QuitCharacter)
            {
                o_IsPendingQuitRequest = true;
            }
        }

        private void askUserForPlayersData(out string o_FirstPlayerName, out bool o_IsSecondPlayerComputer, out string o_SecondPlayerName)
        {
            Console.WriteLine("Please enter first player's name:");
            o_FirstPlayerName = Console.ReadLine();
            Console.WriteLine("Do you want to play against the computer? (y)es/(n)o");
            string playWithComputerDecision = Console.ReadLine();
            if ("Y" == playWithComputerDecision.ToUpper())
            {
                o_IsSecondPlayerComputer = true;
                o_SecondPlayerName = k_ComputerName;
            }
            else
            {
                o_IsSecondPlayerComputer = false;
                Console.WriteLine("Please enter second player's name:");
                o_SecondPlayerName = Console.ReadLine();
            }
        }

        private void getValidBoardDimensionsFromUser(out int o_Height, out int o_Width)
        {
            int boardHeight;
            int boardWidth;
            bool isValidDimensions = false;

             do
             {
                 boardHeight = askUserForDimension("height");
                 boardWidth = askUserForDimension("width");

                 bool isValidHeight = r_GameManager.IsBoardHeightValid(boardHeight);
                 bool isValidWidth = r_GameManager.IsBoardWidthValid(boardWidth);
                 bool isValidOveralDimensions = r_GameManager.IsBoardDimensionsValid(boardHeight, boardWidth);

                if (!isValidHeight)
                {
                    Console.WriteLine("The height is invalid");
                }

                if (!isValidWidth)
                {
                    Console.WriteLine("The width is invalid");
                }

                if (!isValidOveralDimensions)
                {
                    Console.WriteLine("The board need the total number of cells to be even.");
                }

                 isValidDimensions = isValidWidth && isValidHeight && isValidOveralDimensions;
             }
             while (!isValidDimensions);

             o_Height = boardHeight;
             o_Width = boardWidth;
        }

        private int askUserForDimension(string i_DimensionName)
        {
            string dimensionInputString;
            int result = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Please enter board's " + i_DimensionName);
                dimensionInputString = Console.ReadLine();

                if (!int.TryParse(dimensionInputString, out result))
                {
                    Console.WriteLine(string.Format("board's {0} must be a number", i_DimensionName));
                }
                else
                {
                    isValidInput = true;
                }
            }

            return result;
        }

        private bool tryParseRequestedCardLocationString(string i_UserInput, out int o_SelectedRow, out int o_SelectedColumn)
        {
            bool IsParseSuccessful = false;
            o_SelectedColumn = 0;
            o_SelectedRow = 0;

            Regex rowColumnExtractorPattern = new Regex(@"\s*(\w{1})\s*(\d)", RegexOptions.IgnoreCase);
            Match rowColumnExtractedMatch = rowColumnExtractorPattern.Match(i_UserInput);
            if (rowColumnExtractedMatch.Success)
            { 
                IsParseSuccessful = true;
                string selectedColumnText = rowColumnExtractedMatch.Groups[1].Value;
                o_SelectedColumn = selectedColumnText.ToUpper()[0] - k_headerColumn;
                o_SelectedRow = int.Parse(rowColumnExtractedMatch.Groups[2].Value) - 1;
            }

            return IsParseSuccessful;
        }

        private CardLocation getCardLocationToRevealFromUser(out bool o_PendingQuitRequest)
        {
            bool isInputValid = false;
            o_PendingQuitRequest = false;
            int selectedRowIndex = -1;
            int selectedColumnIndex = -1;

            do
            {
                Console.WriteLine(
@"{0} it is your turn. select card to reveal. 
Example: to select the 2nd row of B column, enter B2. to exit - press Q", 
                                                                        r_GameManager.CurrentPlayerName);
                string cardInputString = Console.ReadLine();
                if (k_QuitCharacter == cardInputString.ToUpper())
                {
                    o_PendingQuitRequest = true;
                }
                else
                {
                    if (tryParseRequestedCardLocationString(cardInputString, out selectedRowIndex, out selectedColumnIndex))
                    {
                        CardLocation cardLocation = new CardLocation(selectedRowIndex, selectedColumnIndex);
                        if (!r_GameManager.CheckIfCardExistsInBoard(cardLocation))
                        {
                            Console.WriteLine("Card does not exist in the game board.");
                        }
                        else if (r_GameManager.CheckIfCardIsAlreadyRevealed(cardLocation))
                        {
                            Console.WriteLine("That card is already revealed.");
                        }
                        else
                        {
                            isInputValid = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine(@"Incorrect format. Select a card to reveal. 
 +Example: to select the 2nd row of B column, enter B2
 +To quit the game press Q.");
                    }
                }
            }
            while (!isInputValid && !o_PendingQuitRequest);

            return new CardLocation(selectedRowIndex, selectedColumnIndex);
        }
    }
}
