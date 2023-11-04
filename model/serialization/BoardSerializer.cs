using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlaNet_Projekt_1.model.serialization;

public class BoardSerializer
{
    public static void Serialize(Board board, string path)
    {
        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

        BoardToJson boardToJson = new BoardToJson(board);

        using StreamWriter sw = new StreamWriter(path);
        using JsonWriter jw = new JsonTextWriter(sw);
        try
        {
            serializer.Serialize(jw, boardToJson);
        }
        catch
        {
            throw new JsonSerializationException("Serialization error");
        }
    }

    public static Board Deserialize(string path)
    {
        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

        using StreamReader sr = new StreamReader(path);
        using JsonReader jr = new JsonTextReader(sr);

        var des = serializer.Deserialize<BoardToJson>(jr);
        if (des == null)
            throw new JsonSerializationException("Deserialization error");

        try
        {
            var board = des.JsonToBoard();
            return board;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
