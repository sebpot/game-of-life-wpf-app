using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaNet_Projekt_1.model.serialization;

public class BoardStateToJson
{
    [JsonProperty(PropertyName = "width")]
    public int Width { get; set; }
    [JsonProperty(PropertyName = "height")]
    public int Height { get; set; }
    [JsonProperty(PropertyName = "generation")]
    public int Generation { get; set; }
    [JsonProperty(PropertyName = "born")]
    public int Born { get; set; }
    [JsonProperty(PropertyName = "died")]
    public int Died { get; set; }
    [JsonProperty(PropertyName = "cells")]
    public string Cells { get; set; }

    public BoardStateToJson() { }

    public BoardStateToJson(BoardState boardState)
    {
        Width = boardState.Width; 
        Height = boardState.Height;
        Generation = boardState.Generation;
        Born = boardState.Born;
        Died = boardState.Died;

        Cells = Cells2DArrayToString(boardState);
    }

    public static string Cells2DArrayToString(BoardState boardState)
    {
        var width = boardState.Width;
        var height = boardState.Height;
        var cells = boardState.Cells;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if(cells[j, i] == true)
                {
                    sb.Append($"{j}_{i}-");
                }
            }
        }

        return sb.ToString();
    }

    public BoardState JsonToBoardState(int width, int height)
    {
        BoardState boardState = new BoardState(width, height);
        boardState.Generation = Generation;
        boardState.Born = Born;
        boardState.Died = Died;
        boardState.Cells = CellsStringTo2DArray(Cells, width, height);
        return boardState;
    }

    public static bool[,] CellsStringTo2DArray(string cellsString, int width, int height)
    {
        bool[,] cells = new bool[width, height];

        string[] cellStrings = cellsString.Split('-');

        foreach (var cellString in cellStrings)
        {
            if(cellString.Length == 3)
            {
                string[] cords = cellString.Split('_');
                cells[int.Parse(cords[0]), int.Parse(cords[1])] = true;
            }
        }

        return cells;
    }
}
