using Microsoft.Win32;
using PlaNet_Projekt_1.model.serialization;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlaNet_Projekt_1.views;

/// <summary>
/// Interaction logic for StartView.xaml
/// </summary>
public partial class StartView : UserControl
{
    public StartView()
    {
        InitializeComponent();
    }

    private void NewGameButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.Navigate(new CreateGameView());
    }

    private void LoadGameButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog();
        dialog.Filter = "Game of life save file (.txt)|*.txt";

        if (dialog.ShowDialog() == false)
        {
            return;
        }

        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.Navigate(new GameView(BoardSerializer.Deserialize(dialog.FileName)));
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this);
        window.Close();
    }
}
