using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B14_Ex05_Logic;

namespace B14_Ex05_WindowsUI
{
    internal class GameForm : Form
    {
        private readonly GameManager r_GameManager;
        SettingsForm settingForm = new SettingsForm();

        readonly Label r_LabelCurrentPlayer = new Label();
        readonly Label r_LabelFirstPlayer = new Label();
        readonly Label r_LabelSecondPlayer = new Label();
        readonly CardButton[,] gameCards;
        private const int k_Margin = 15;

        public GameForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            settingForm.ShowDialog();
            r_GameManager = new GameManager();
            r_GameManager.StartNewGame(settingForm.FirstPlayerName, false, settingForm.SecondPlayerName, settingForm.IsComputerInGame, settingForm.BoardHeight, settingForm.BoardWidth);
            gameCards = new CardButton[r_GameManager.BoardWidth, r_GameManager.BoardHeight];
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            initGameControls();
        }

        private void initGameControls()
        {
            int boardWidth = gameCards.GetLength(0);
            int boardHeight = gameCards.GetLength(1);

            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    gameCards[i,j] = new CardButton(new CardLocation(j, i), k_Margin);
                    this.Controls.Add(gameCards[i, j]);
                }
            }

            r_LabelCurrentPlayer.Text = r_GameManager.CurrentPlayerName;
            r_LabelCurrentPlayer.AutoSize = true;
            r_LabelCurrentPlayer.Location = new Point(k_Margin, gameCards[boardWidth - 1,boardHeight - 1].Bottom + k_Margin);
            this.Controls.Add(r_LabelCurrentPlayer);

            r_LabelFirstPlayer.Text = settingForm.FirstPlayerName;
            r_LabelFirstPlayer.AutoSize = true;
            r_LabelFirstPlayer.Location = new Point(k_Margin, r_LabelCurrentPlayer.Bottom + k_Margin);
            r_LabelFirstPlayer.BackColor = Color.PaleGreen;
            this.Controls.Add(r_LabelFirstPlayer);      
            
            r_LabelSecondPlayer.Text = settingForm.SecondPlayerName;
            r_LabelSecondPlayer.AutoSize = true;
            r_LabelSecondPlayer.Location = new Point(k_Margin, r_LabelFirstPlayer.Bottom + k_Margin);
            r_LabelSecondPlayer.BackColor = Color.Thistle;
            this.Controls.Add(r_LabelSecondPlayer);

            this.ClientSize = new Size(gameCards[boardWidth - 1, boardHeight - 1].Right + k_Margin, r_LabelSecondPlayer.Bottom + k_Margin);
        }


    }
}
