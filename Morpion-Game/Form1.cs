﻿using System;
using System.Windows.Forms;

namespace Morpion_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AttachButtonClickEvents(panel1);
        }

        bool tour = true;
        bool victory = false;
        string X = "X";
        string O = "O";

        private void AttachButtonClickEvents(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button)
                {
                    control.Click += new EventHandler(button_Click);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                // Disable the choice button
                checksymbol.Enabled = false;

                clickedButton.Text = tour ? X : O;
                clickedButton.Enabled = false;
                tour = !tour;

                if (CheckPlayer())
                {
                    if (CheckVictory())
                    {
                        MessageBox.Show($"Player {(tour ? "1" : "2")} wins!");
                        ResetGame();
                    }
                }
                else
                {
                    if (CheckVictory())
                    {
                        MessageBox.Show($"Player {(tour ? "2" : "1")} wins!");
                        ResetGame();
                    }
                }

                if(CheckDraw())
                {
                    MessageBox.Show("Draw");
                    ResetGame();
                }
            }
        }

        private bool CheckVictory()
        {
            string[,] board = new string[3, 3]
            {
                { button1.Text, button2.Text, button3.Text },
                { button4.Text, button5.Text, button6.Text },
                { button7.Text, button8.Text, button9.Text }
            };

            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] != "" && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
                {
                    return true;
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] != "" && board[0, col] == board[1, col] && board[1, col] == board[2, col])
                {
                    return true;
                }
            }

            // Check diagonals
            if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }

            if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }

            return false;
        }

        private bool CheckDraw()
        {
            // Check if all buttons are already clicked with no victory
            if (!CheckVictory() && !button1.Enabled && !button2.Enabled && !button3.Enabled && !button4.Enabled && !button5.Enabled && !button6.Enabled && !button7.Enabled && !button8.Enabled && !button9.Enabled)
            {
                return true;
            }

            return false;
        }

        private bool ResetGame()
        {
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;

            checksymbol.Enabled = true;

            victory = false;
            tour = true;

            return victory;
        }

        private bool CheckPlayer()
        {
            if (checksymbol.Text == O)
            {
                return true;
            }

            return false;
        }

        private void checksymbol_Click(object sender, EventArgs e)
        {
            if (tour)
            {
                checksymbol.Text = O;
                tour = false;
            }
            else
            {
                checksymbol.Text = X;
                tour = true;
            }
        }

        private void goBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            menu.Show();
            this.Hide();
        }

        private void resetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}
