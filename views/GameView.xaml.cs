using PlaNet_Projekt_1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PlaNet_Projekt_1.views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private ObservableCollection<CellItem> _cells = new ObservableCollection<CellItem>();
        private Board Board { get; set; }

        public GameView(Board board)
        {
            InitializeComponent();

            Board = board;

            BoardItemsControl.ItemsSource = _cells;

            UpdateBoardItemsControl();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var rect = (Rectangle)sender;
            var cellItem = (CellItem)rect.DataContext;

            Board.States[Board.States.Count - 1].Cells[cellItem.X, cellItem.Y] = Board.States[Board.States.Count - 1].Cells[cellItem.X, cellItem.Y] == true ? false : true;

            UpdateBoardItemsControl();
        }

        private void UpdateBoardItemsControl()
        {
            _cells.Clear();
            for (int i = 0; i < Board.Height; i++)
            {
                for (int j = 0; j < Board.Width; j++)
                {
                    _cells.Add(new CellItem()
                    {
                        Width = 20,
                        Height = 20,
                        Left = 20 * j + 1,
                        Top = 20 * i + 1,
                        Color = Board.States[Board.States.Count - 1].Cells[j, i] == true ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White),
                        X = j,
                        Y = i
                    });
                }
            }
            StatGrid.DataContext = Board.States[Board.States.Count - 1];
        }

        private void NextGen_Click(object sender, RoutedEventArgs e)
        {
            Board.NextGen();
            UpdateBoardItemsControl();
        }

        private void PrevGen_Click(object sender, RoutedEventArgs e)
        {
            Board.PrevGen();
            UpdateBoardItemsControl();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}
