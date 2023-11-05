using PlaNet_Projekt_1.model;
using PlaNet_Projekt_1.model.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlaNet_Projekt_1.views;

/// <summary>
/// Interaction logic for CreateGameView.xaml
/// </summary>
public partial class CreateGameView : UserControl
{
    public List<String> BoardTemplatesList { get; set; }
    public int MaxBoardSize { get; set; }

    public CreateGameView()
    {
        InitializeComponent();

        BoardTemplatesList = new List<String>()
        {
            "Glider 3x3",
            "Acorn 7x3",
            "Light-Weight Spaceship 5x4",
            "Middle-Weight Spaceship 6x5",
            "Heavy-Weight Spaceship 7x5",
            "Pulsar 15x15",
            "Glider Gun 36x9",
            "Block-Laying Switch Engine 29x28"
        };

        DataContext = this;
        MaxBoardSize = 100;
    }

    private void CreateNewGame_Click(object sender, RoutedEventArgs e)
    {
        BoardTemplates boardTemplates = new BoardTemplates();

        int width = int.Parse(widthBox.Text);
        int height = int.Parse(heightBox.Text);

        if(width > MaxBoardSize || height > MaxBoardSize || width < 1 || height < 1)
        {
            MessageBox.Show("Incorrect board size. Width and height\nmust be within 100.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Board board = boardTemplates.CreateBoardWithTemplate((string)BoardTemplatesListBox.SelectedItem, width, height);
        if(board == null)
        {
            MessageBox.Show("Chosen pattern cannot fit in given width and height.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.Navigate(new GameView(board));
    }
}
