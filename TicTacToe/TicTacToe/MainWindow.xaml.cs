using System;
using System.Reflection;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shell;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        /// <summary>
        /// Holds the input of the User in the Game
        /// </summary>
        private MarkType[] UserInput;

        /// <summary>
        /// On the turn of the player 1 the boolean value is set to tru or else false
        /// </summary>
        private bool Player;

        /// <summary>
        /// Decnotes wheater the game is over or on the process
        /// </summary>
        private bool GameTermination;

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Creates a new Game and then clears all the value 
        /// </summary>
        public void NewGame()
        {
            //for each boxes in the game
            UserInput = new MarkType[9];
            /// <summary>
            /// By default the array is set to the value of 0 which in our case is the enumaration datatype whose 0 value is free
            /// But if the order of the eniumaration datatype is changes then it give unpleseat outputs so we can declarae it Explicitely.
            /// </summary>
            
            for(var i = 0; i < UserInput.Length; i++)
                UserInput[i] = 0;
            //Lets initinalte the user to Start the game
            Player = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.WhiteSmoke;
            });
            GameTermination = false;

        }
        /// <summary>
        /// Click Event Declaration
        /// </summary>
        /// <param name="sender">THe button Clicks</param>
        /// <param name="e">THe event to be Performed on the event of the click Made.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GameTermination)
            {
                NewGame();
                return;
            }
            //Explicit Type casting of the Object
            var button = (Button)sender;
            //Getting teh row and the Column of the Element
            var CorrespondingColumn = Grid.GetColumn(button);
            var CorrespondingRow = Grid.GetRow(button);
            //Calculation of the index position based on the Column and the row number retrieved.
            int Corresponding_Index = CorrespondingColumn + (CorrespondingRow * 3);
            //Don;t allow the users to change the input that was made earlier
            if (UserInput[Corresponding_Index] != MarkType.Free)
                return;
            //Setting the Cell value with the help of the Player varilable for P1-> X else O
            UserInput[Corresponding_Index] = (Player) ? MarkType.Cross : MarkType.Nought;
            //Set button Content    
            button.Content = Player ? "X" : "O";
            //Button Color Based on the player
            button.Foreground = Player ? Brushes.Red : Brushes.Blue;
            //Change the player each time
            Player = !Player;
            //Also Posible Player^=Player;
            // Check for the Possibility of winning the match
            WinningPossiblity();

        }

        private void WinningPossiblity()
        {
            #region Row wise Checking of the board

            // Horizontal Win in the Row 1
            if (UserInput[0] != MarkType.Free && (UserInput[0] & UserInput[1] & UserInput[2]) == UserInput[0])
            {
                // Game ends
                GameTermination = true;
                // Highlight winning cells in green
                Button_0_0.Background = Button_0_1.Background = Button_0_2.Background = Brushes.Green;
            }
            
            // Horizontal Win in the Row 2
            if (UserInput[3] != MarkType.Free && (UserInput[3] & UserInput[4] & UserInput[5]) == UserInput[3])
            {
                // Game ends
                GameTermination = true;
                // Highlight winning cells in green
                Button_1_0.Background = Button_1_1.Background = Button_1_2.Background = Brushes.Green;
            }

            // Horizontal Win in the Row 3
            if (UserInput[6] != MarkType.Free && (UserInput[6] & UserInput[7] & UserInput[8]) == UserInput[6])
            {
                // Game ends
                GameTermination = true;
                // Highlight winning cells in green
                Button_2_0.Background = Button_2_1.Background = Button_2_2.Background = Brushes.Green;
            }

            #endregion

            #region Column Wise Checking of the Board

            // Winning Chance Checking for the Column 1
            if (UserInput[0]!=MarkType.Free && (UserInput[0] & UserInput[3] & UserInput[6]) == UserInput[0])
            {
                //Terminates the User Inputs
                GameTermination = true;
                //Highlights the Winning Part
                Button_0_0.Background = Button_1_0.Background = Button_2_0.Background = Brushes.Green;
            }

            //Winning Chance CHecking for the Column 2
            if (UserInput[1]!=MarkType.Free && (UserInput[1] & UserInput[4] & UserInput[7]) == UserInput[1])
            {
                //Terminates the Game
                GameTermination = true;
                //Highlight the Wining PArt
                Button_0_1.Background = Button_1_1.Background = Button_2_1.Background = Brushes.Green;
            }

            //Winning Chance for the column 3
            if (UserInput[2]!=MarkType.Free && (UserInput[2] & UserInput[5] & UserInput[8]) == UserInput[2])
            {
                //Terminates the game
                GameTermination = true;
                //Highlight the Winngin Parts
                Button_0_2.Background = Button_1_2.Background = Button_2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal wise Checking of the Board

            //Checking the Left to Right Diagonal
            if (UserInput[0] != MarkType.Free && (UserInput[0] & UserInput[4] & UserInput[8]) == UserInput[0])
            {
                //Terminates the Game
                GameTermination = true;
                //Highlight the diagonal
                Button_0_0.Background = Button_1_1.Background = Button_2_2.Background = Brushes.Green;
            }

            //Checking the RIght to Left Diagonal
            if (UserInput[2] != MarkType.Free && (UserInput[2] & UserInput[4] & UserInput[6]) == UserInput[2])
            {
                //Game Termination
                GameTermination = true;
                //Hightlight the Diagonal
                Button_0_2.Background = Button_1_1.Background = Button_2_0.Background = Brushes.Green;
            }
            {

            }

            #endregion

            #region Worst Case : No One Wins

            if (!UserInput.Any(Results => Results == MarkType.Free))
            {
                GameTermination = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });

            }

            #endregion
        }
    }
}