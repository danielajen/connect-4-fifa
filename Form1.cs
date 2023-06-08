using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace connect_4_fifa
{
    public partial class Form1 : Form
    {
        int[,] board =
        {
            {0,0,0,0,0,0 },
            {0,0,0,0,0,0 },
            {0,0,0,0,0,0 },
            {0,0,0,0,0,0 },
            {0,0,0,0,0,0 },
            {0,0,0,0,0,0 },
            {0,0,0,0,0,0 },
        };
        int currentPlayer = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void enter(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender; ;
            pictureBox.Tag = pictureBox.BackColor;
            pictureBox.BackColor = Color.LightBlue; // blue
        }

        private void leave(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Color originalColor = (Color)pictureBox.Tag;
            pictureBox.BackColor = originalColor; //og
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++)
                {
                    this.Controls["p" + col + row].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        private void click(object sender, EventArgs e)
        {
            PictureBox picked = (PictureBox)sender;
            char[] broken = picked.Name.ToCharArray();
            int namenum = int.Parse(broken[1].ToString());
            int drop = blankcheck(namenum);
            int rowspot = drop - 1;

            if (drop > 0)
            {
                // Current player Messi
                Image playerImage = GetPlayerImage(currentPlayer);

                // Set the backg
                this.Controls["p" + namenum + rowspot].BackgroundImage = playerImage;

                // Update the board 
                board[namenum, rowspot] = currentPlayer;


                UpdatePictureBox();

                // messi vs ronaldo
                currentPlayer = GetNextPlayer(currentPlayer);

                // win
                if (CheckForWinner(namenum, rowspot))
                {
                    string winner = GetPlayerName(currentPlayer);
                    MessageBox.Show(winner + " wins!");
                    ResetBoard();
                    return;
                }
            }


    }
        #region checks
        private bool CheckVertical(int col, int row)
        {
            int player = board[col, row];
            int count = 1;

            // Check upwards
            for (int i = row - 1; i >= 0 && board[col, i] == player; i--)
            {
                count++;
            }

            // Check downwards
            for (int i = row + 1; i < 6 && board[col, i] == player; i++)
            {
                count++;
            }

            return count >= 4;
        }
        private bool CheckHorizontal(int col, int row)
        {
            int player = board[col, row];
            int count = 1;

            // Check to the left
            for (int i = col - 1; i >= 0 && board[i, row] == player; i--)
            {
                count++;
            }

            // Check to the right
            for (int i = col + 1; i < 7 && board[i, row] == player; i++)
            {
                count++;
            }

            return count >= 4;
        }

        private bool CheckDiagonalUp(int col, int row)
        {
            int player = board[col, row];
            int count = 1;

            // Check upwards and to the right
            for (int i = col + 1, j = row - 1; i < 7 && j >= 0 && board[i, j] == player; i++, j--)
            {
                count++;
            }

            // Check downwards and to the left
            for (int i = col - 1, j = row + 1; i >= 0 && j < 6 && board[i, j] == player; i--, j++)
            {
                count++;
            }

            return count >= 4;
        }

        private bool CheckDiagonalDown(int col, int row)
        {
            int player = board[col, row];
            int count = 1;

            // Check upwards and to the left
            for (int i = col - 1, j = row - 1; i >= 0 && j >= 0 && board[i, j] == player; i--, j--)
            {
                count++;
            }

            // Check downwards and to the right
            for (int i = col + 1, j = row + 1; i < 7 && j < 6 && board[i, j] == player; i++, j++)
            {
                count++;
            }

            return count >= 4;
        }

        private bool CheckForWinner(int col, int row)
        {
            return CheckVertical(col, row) ||
                   CheckHorizontal(col, row) ||
                   CheckDiagonalUp(col, row) ||
                   CheckDiagonalDown(col, row);
        }



        private void ResetBoard()
        {
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++)
                {
                    board[col, row] = 0;
                    PictureBox pictureBox = GetPictureBox(col, row);
                    if (pictureBox != null)
                    {
                        pictureBox.BackgroundImage = null;
                    }
                }
            }
            currentPlayer = 1;
        }
        #endregion

        private void UpdatePictureBox()
        {
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++)
                {
                    PictureBox pictureBox = GetPictureBox(col, row);

                    if (pictureBox != null)
                    {
                        switch (board[col, row])
                        {
                            case 1:
                                pictureBox.BackgroundImage = Properties.Resources.messi;
                                break;
                            case 2:
                               pictureBox.BackgroundImage = Properties.Resources.ronado;
                                break;
                            default:
                                pictureBox.BackgroundImage = null; // default
                                break;
                        }
                    }
                }
            }
        }
        private Image GetPlayerImage(int player)
        {
            Image playerImage = null; //default

            if (player == 1)
            {
                playerImage = Properties.Resources.messi;
            }
            else if (player == 2)
            {
                playerImage = Properties.Resources.ronado;
            }

            return playerImage;
        }

        private int GetNextPlayer(int currentPlayer)
        {
            int nextPlayer = 1;

            if (currentPlayer == 1)
            {
                nextPlayer = 2;
            }

            return nextPlayer;
        }

        private string GetPlayerName(int player)
        {
            string playerName = "ronaldo";

            if (player == 2)
            {
                playerName = "messi";
            }

            return playerName;
        }


        private PictureBox GetPictureBox(int col, int row)
        {
            return this.Controls["p" + col + row] as PictureBox;
        }

        
                    
               
         private int blankcheck(int col)
        {
            int blank = 0;
            for (int i = 0; i < 6; i++)
            {
                if (board[col, i] == 0) blank++;
            }
            return blank;
        }
    }
}

