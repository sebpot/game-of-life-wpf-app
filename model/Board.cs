using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaNet_Projekt_1.model;

public class Board
{
    public int Width { set; get; }
    public int Height { set; get; }
    public List<BoardState> States { get; set; }


    public Board(int width, int height) 
    {
        Width = width;
        Height = height;
        States = new List<BoardState>();
        States.Add(new BoardState(Width, Height));
    }

    public void PrevGen()
    {
        if(States.Count == 1) return;
        States.RemoveAt(States.Count - 1);
    }

    public void NextGen()
    {
        States.Add(new BoardState(States[States.Count - 1]));
    }
}
