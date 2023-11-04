using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaNet_Projekt_1.model.serialization;

public class BoardToJson
{
    [JsonProperty(PropertyName = "width")]
    public int Width { set; get; }
    [JsonProperty(PropertyName = "height")]
    public int Height { set; get; }
    [JsonProperty(PropertyName = "states")]
    public List<BoardStateToJson> States { get; set; }

    public BoardToJson() { }

    public BoardToJson(Board board)
    {
        Width = board.Width;
        Height = board.Height;
        States = new List<BoardStateToJson>();
        foreach (var state in board.States)
        {
            States.Add(new BoardStateToJson(state));
        }
    }

    public Board JsonToBoard()
    {
        Board board = new Board(Width, Height);

        foreach (var state in States)
        {
            board.States.Add(state.JsonToBoardState(Width, Height));
        }

        return board;
    }
}
