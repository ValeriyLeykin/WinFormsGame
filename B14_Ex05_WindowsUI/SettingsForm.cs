using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B14_Ex05_WindowsUI
{
    class SettingsForm : Form
    {
        private readonly TextBox r_TextboxFirstPlayerName = new TextBox();
        private readonly TextBox r_TextboxSecondPlayerName = new TextBox();

        private readonly Label r_LabelFirstPlayerName = new Label();
        private readonly Label r_LabelSecondPlayerName = new Label();
        private readonly Label r_LabelBoardSize = new Label();

        private readonly Button r_ButtonAgainstComputer = new Button();
        private readonly Button r_ButtonBoardSize = new Button();
        private readonly Button r_ButtonStart = new Button();
        bool m_computerInGame = true;
        const string k_ComputerName = "-Computer-";

        public SettingsForm()
        {
            this.Text = "Memory Game - Settings";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            initControls();
        }
        
        private void initControls()
        {
            int margin = 10;
            int closeSpaceBetweenControls = 8; 
            int midSpaceBetweenControls = 15; 
            int xPoint ;
            int yPoint;

            this.Controls.AddRange(new Control[] {r_TextboxFirstPlayerName, r_TextboxSecondPlayerName, r_LabelFirstPlayerName, r_LabelSecondPlayerName, r_LabelBoardSize, r_ButtonAgainstComputer, r_ButtonBoardSize, r_ButtonStart });

            r_LabelFirstPlayerName.Text = "First Player Name";
            r_LabelFirstPlayerName.Location = new Point(margin, margin);
            r_LabelFirstPlayerName.AutoSize = true;
            
            r_LabelSecondPlayerName.Text = "Second Player Name";
            r_LabelSecondPlayerName.Location = new Point(margin, r_LabelFirstPlayerName.Bottom + closeSpaceBetweenControls);
            r_LabelSecondPlayerName.AutoSize = true;

            r_LabelBoardSize.Text = "Board Size";
            r_LabelBoardSize.Location = new Point(margin, r_LabelSecondPlayerName.Bottom + midSpaceBetweenControls);
            r_LabelBoardSize.AutoSize = true;

            xPoint = Math.Max(r_LabelFirstPlayerName.Right, r_LabelSecondPlayerName.Right) + midSpaceBetweenControls;
            yPoint = calcTopYAllignedWithAnchor(r_LabelFirstPlayerName, r_TextboxFirstPlayerName);

            r_TextboxFirstPlayerName.Location = new Point(xPoint, yPoint);

            yPoint = calcTopYAllignedWithAnchor(r_LabelSecondPlayerName, r_TextboxSecondPlayerName);
            r_TextboxSecondPlayerName.Location = new Point(xPoint, yPoint);
            r_TextboxSecondPlayerName.Enabled = false;
            r_TextboxSecondPlayerName.Text = k_ComputerName;

            r_ButtonAgainstComputer.Text = "Against Computer";
            xPoint = r_TextboxSecondPlayerName.Right + midSpaceBetweenControls;
            yPoint = calcTopYAllignedWithAnchor(r_TextboxSecondPlayerName, r_ButtonAgainstComputer);
            r_ButtonAgainstComputer.Location = new Point(xPoint, yPoint);
            r_ButtonAgainstComputer.AutoSize = true;
            r_ButtonAgainstComputer.Click += m_ButtonAgainstComputer_Click;
            
            r_ButtonBoardSize.Text = "4x4"; 
            r_ButtonBoardSize.BackColor = Color.MediumPurple;
            r_ButtonBoardSize.Size = new Size (120, 80);
            r_ButtonBoardSize.Location = new Point(margin, r_LabelBoardSize.Bottom + closeSpaceBetweenControls);

            r_ButtonStart.Text = "Start";
            r_ButtonStart.BackColor = Color.LawnGreen;
            r_ButtonStart.Location = new Point(r_ButtonAgainstComputer.Right - r_ButtonStart.Width, r_ButtonBoardSize.Bottom - r_ButtonStart.Height);
            r_ButtonStart.Click += m_ButtonStart_Click;

            this.ClientSize = new Size(r_ButtonAgainstComputer.Right + margin, r_ButtonBoardSize.Bottom + margin);
        }

        private int calcTopYAllignedWithAnchor(Control i_Anchor, Control i_ControlToAllign)
        {
            int midLine = i_Anchor.Top + i_Anchor.Height / 2;
            return (midLine - i_ControlToAllign.Height / 2);
        }

        void m_ButtonAgainstComputer_Click(object sender, EventArgs e)
        {
            if (m_computerInGame)
            {
                r_TextboxSecondPlayerName.Enabled = true;
                r_TextboxSecondPlayerName.Text = "";
                m_computerInGame = false;
            }
            else 
            {
                r_TextboxSecondPlayerName.Enabled = false;
                r_TextboxSecondPlayerName.Text = k_ComputerName;
                m_computerInGame = true;
            }
        }

        void m_ButtonStart_Click(object sender, EventArgs e)
        {
            if (validatePlayersNames())
            {
                this.Close();
            }        
        }

        private bool validatePlayersNames()
        {
            bool validFields = true;
            if (r_TextboxFirstPlayerName.Text == "")
            {
                MessageBox.Show("First Player name is a mandatory field");
                validFields = false;
            }
            if (r_TextboxSecondPlayerName.Text == "")
            {
                MessageBox.Show("Second Player name is a mandatory field");
                validFields = false;
            }

            return validFields;
        }

        internal string FirstPlayerName
        {
            get {return r_TextboxFirstPlayerName.Text;}
        }

        internal string SecondPlayerName
        {
            get { return r_TextboxSecondPlayerName.Text; }
        }

        //TODO: decide how we implement board size choice: 
        //string array and then parsing or width and height vars.
        internal int BoardWidth
        {
            get { return 5; }
        }

        internal int BoardHeight
        {
            get { return 4; }
        }

        internal bool IsComputerInGame
        {
             get {return m_computerInGame;}
        }
    }
}
