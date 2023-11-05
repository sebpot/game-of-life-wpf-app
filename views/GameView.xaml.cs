using Microsoft.Win32;
using PlaNet_Projekt_1.model;
using PlaNet_Projekt_1.model.serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace PlaNet_Projekt_1.views;

/// <summary>
/// Interaction logic for GameView.xaml
/// </summary>
public partial class GameView : UserControl
{
    private ObservableCollection<CellItem> _cells = new ObservableCollection<CellItem>();
    private Board Board { get; set; }
    private double BoardWidth;
    private double BoardHeight;
    private DispatcherTimer _autoTimer;

    public GameView(Board board)
    {
        InitializeComponent();

        Board = board;

        _autoTimer = new DispatcherTimer(DispatcherPriority.Render);
        _autoTimer.Interval = TimeSpan.FromMilliseconds(200);
        _autoTimer.Tick += AutoTimerTick;

        BoardItemsControl.ItemsSource = _cells;
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

        var widthScale = BoardWidth/Board.Width;
        var heightScale = BoardHeight / Board.Height;
        var size = widthScale < heightScale ? widthScale : heightScale;
        var widthOffset = widthScale > heightScale ? (BoardWidth - (size *  Board.Width)) / 2 : 0;
        var heightOffset = widthScale < heightScale ? (BoardHeight - (size * Board.Height)) / 2 : 0;

        for (int i = 0; i < Board.Height; i++)
        {
            for (int j = 0; j < Board.Width; j++)
            {
                _cells.Add(new CellItem()
                {
                    Width = size - 1,
                    Height = size - 1,
                    Left = size * j + 2 + widthOffset,
                    Top = size * i + 2 + heightOffset,
                    Color = Board.States[Board.States.Count - 1].Cells[j, i] == true ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White),
                    X = j,
                    Y = i
                });
            }
        }
        StatGrid.DataContext = Board.States[Board.States.Count - 1];
    }

    private void BoardItemsControl_Loaded(object sender, RoutedEventArgs e)
    {
        ItemsControl itemsControl = (ItemsControl)sender;

        BoardWidth = itemsControl.ActualWidth;
        BoardHeight = itemsControl.ActualHeight;

        UpdateBoardItemsControl();
    }

    private void BoardItemsControl_SizeChanged(object sender, RoutedEventArgs e)
    {
        ItemsControl itemsControl = (ItemsControl)sender;

        BoardWidth = itemsControl.ActualWidth;
        BoardHeight = itemsControl.ActualHeight;

        UpdateBoardItemsControl();
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

    private void AutoButton_Checked(object sender, RoutedEventArgs e)
    {
        if (_autoTimer == null) return;

        if (!_autoTimer.IsEnabled)
        {
            _autoTimer.Start();
        }
    }

    private void AutoButton_Unchecked(object sender, RoutedEventArgs e)
    {
        if (_autoTimer == null) return;

        if (_autoTimer.IsEnabled)
        {
            _autoTimer.Stop();
        }
    }

    private async void AutoTimerTick(object? sender, EventArgs e)
    {
        _autoTimer.Stop();
        await Task.Run(Board.NextGen);
        UpdateBoardItemsControl();
        _autoTimer.Start();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog();
        dialog.Filter = "Game of life save file (.txt)|*.txt";
        dialog.FileName = "GOL_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

        if (dialog.ShowDialog() == false)
            return;

        BoardSerializer.Serialize(Board, dialog.FileName);
    }

    private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog();
        dialog.Filter = "Game of life state image (.jpeg)|*.jpeg";
        dialog.FileName = "GOL_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpeg";

        if (dialog.ShowDialog() == false)
            return;

        RenderTargetBitmap bitmap = new RenderTargetBitmap((int)this.BoardGrid.ActualWidth, (int)this.BoardGrid.ActualHeight,
                  96, 96, PixelFormats.Pbgra32);
        bitmap.Render(this.BoardGrid);

        using (FileStream stream = File.Create(dialog.FileName))
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
        }
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this);
        window.Close();
    }
}
