using System;
using System.Collections.Generic;
using System.Text;

namespace B14_Ex05_Logic
{
    internal class Player
    {
        private string m_Name;
        private int m_GamePoints = 0;
        private bool m_IsComputer;

        public bool IsComputer
        {
            get { return m_IsComputer; }
            set { m_IsComputer = value; }
        }

        public Player(string i_Name, bool i_IsComputer)
        {
            m_Name = i_Name;
            m_IsComputer = i_IsComputer;
        }

        public int GamePoints
        {
            get { return m_GamePoints; }
            set { m_GamePoints = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public void IncrementPoint()
        {
            m_GamePoints++;
        }
    }
}
