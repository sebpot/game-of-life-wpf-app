using PlaNet_Projekt_1.model;
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
    public CreateGameView()
    {
        InitializeComponent();
    }

    private void CreateNewGame_Click(object sender, RoutedEventArgs e)
    {
        int width = int.Parse(widthBox.Text);
        int height = int.Parse(heightBox.Text);

        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.Navigate(new GameView(new Board(width, height)));
    }
}
