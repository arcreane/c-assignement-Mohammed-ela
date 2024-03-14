using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Puissance4
{
    public partial class MainWindow : Window
    {
        private int[,] gameState = new int[6, 7]; // grille de 6 rangé et 7 colonnes
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

        private void ColumnClick(object sender, RoutedEventArgs e)
{
    // Utilisation de 'is' pour une vérification de type plus sûre et extraction du Tag si c'est un string
    if (sender is Button button && button.Tag is string tagString)
    {
        // Tentative de conversion du Tag string en entier avec int.TryParse
        if (int.TryParse(tagString, out var column))
        {
            // Itération à partir du bas de la colonne pour trouver la première cellule vide
            for (int row = gameState.GetLength(0) - 1; row >= 0; row--)
            {
                if (gameState[row, column] == 0)
                {
                    // Mise à jour de l'état du jeu avec le jeton du joueur actuel
                    gameState[row, column] = currentPlayer;
                    UpdateUICell(row, column, currentPlayer);

                    // Alternance entre les joueurs après chaque coup
                    currentPlayer = currentPlayer == 1 ? 2 : 1;

                    // Sortie de la boucle une fois le jeton placé
                    break;
                }
            }
        }
        else
        {
            // Le Tag ne peut pas être converti en entier, gérer l'erreur si nécessaire
            MessageBox.Show("Erreur de conversion du Tag en entier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
        // Rend l'Ellipse visible
        ellipse.Visibility = Visibility.Visible;
        
        // Change la couleur de l'Ellipse en fonction du joueur
        if (player == 1)
        {
            ellipse.Fill = new SolidColorBrush(Colors.Red);
        }
        else
        {
            ellipse.Fill = new SolidColorBrush(Colors.Yellow);
        }
    }
}

    }
}
