using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace PlaNet_Projekt_1.model;

public class BoardState
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Generation { get; set; }
    public int Born { get; set; }
    public int Died { get; set; }
    public bool[,] Cells { get; set; }

    public BoardState(int width, int height)
    {
        Generation = 0;
        Born = 0;
        Died = 0;
        Width = width; 
        Height = height;
        Cells = new bool[Width, Height];
    }

    public BoardState(BoardState currentState)
    {
        Generation = currentState.Generation + 1;
        Born = currentState.Born;
        Died = currentState.Died;
        Width = currentState.Width;
        Height = currentState.Height;
        Cells = new bool[Width, Height];

        CreateNextGen(currentState.Cells);
    }

    private void CreateNextGen(bool[,] oldCells)
    {
        for(int i = 0; i < Height; i++)
        {
            for(int j = 0; j < Width; j++)
            {
                int neighbors = CountNeighbors(j, i, oldCells);

                if(neighbors == 3 && oldCells[j, i] == false) 
                {
                    Cells[j, i] = true;
                    Born++;
                }
                else if((neighbors < 2 || neighbors > 3) && oldCells[j, i] == true)
                {
                    Cells[j, i] = false;
                    Died++;
                }
                else if(neighbors >= 2 && neighbors <= 3 && oldCells[j, i] == true)
                {
                    Cells[j, i] = true;
                }
            }
        }
    }

    private int CountNeighbors(int x, int y, bool[,] oldCells)
    {
        int count = 0;

        if(x > 0)
        {
            if (oldCells[x - 1, y] == true) count++;
            if (y > 0)
            {
                if (oldCells[x - 1, y - 1] == true) count++;
            }
            if(y < Height - 1)
            {
                if (oldCells[x - 1, y + 1] == true) count++;
            }
        }
        
        if(y > 0)
        {
            if (oldCells[x, y - 1] == true) count++;
            if(x < Width - 1)
            {
                if (oldCells[x + 1, y - 1] == true) count++;
            }
        }

        if(y < Height - 1)
        {
            if (oldCells[x, y + 1] == true) count++;
            if(x < Width - 1)
            {
                if (oldCells[x + 1, y + 1] == true) count++;
            }
        }
        
        if(x < Width - 1)
        {
            if (oldCells[x + 1, y] == true) count++;
        }

        return count;
    }
}
