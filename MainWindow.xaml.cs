using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Puissance4
{
    public partial class MainWindow : Window
    {
        private int[,] gameState = new int[6, 7]; // grille de 6 rangées et 7 colonnes
        private int currentPlayer = 1; // on commence par le 1er joueur

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // initialisation du jeu / jeton vidé 
            for (int row = 0; row < gameState.GetLength(0); row++)
            {
                for (int col = 0; col < gameState.GetLength(1); col++)
                {
                    gameState[row, col] = 0;
                }
            }
        }

        private void UpdateUICell(int row, int column, int player)
        {
            // Construction du nom de l'Ellipse basé sur sa position
            string ellipseName = $"Cell{row}{column}";
            var ellipse = (Ellipse)FindName(ellipseName);

            if (ellipse != null)
            {
                // ellipse visible
                ellipse.Visibility = Visibility.Visible;
                
                // en fct du joueur change de couleur
                if (player == 1)
                {
                    ellipse.Fill = new SolidColorBrush(Colors.Red);
                }
                else if (player == 2)
                {
                    ellipse.Fill = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                // si aucune ellipse trouvée
                MessageBox.Show($"L'Ellipse nommée '{ellipseName}' n'a pas été trouvée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ColumnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tagString)
            {
                if (int.TryParse(tagString, out var column))
                {
                    for (int row = gameState.GetLength(0) - 1; row >= 0; row--)
                    {
                        if (gameState[row, column] == 0)
                        {
                            gameState[row, column] = currentPlayer;
                            UpdateUICell(row, column, currentPlayer);

                            currentPlayer = currentPlayer == 1 ? 2 : 1;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erreur de conversion du Tag en entier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
