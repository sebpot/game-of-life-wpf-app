using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlaNet_Projekt_1.model.template;

public class BoardTemplates
{
    public Dictionary<String, String> Templates { get; set; }

    public BoardTemplates() 
    {
        Templates = new Dictionary<String, String>()
        {
            { "Glider 3x3", "0_0-0_1-0_2-1_0-2_1" },
            { "Acorn 7x3", "0_1-1_4-2_0-2_1-2_4-2_5-2_6" },
            { "Light-Weight Spaceship 5x4", "0_0-0_3-1_4-2_0-2_4-3_1-3_2-3_3-3_4" },
            { "Middle-Weight Spaceship 6x5", "0_2-1_0-1_4-2_5-3_0-3_5-4_1-4_2-4_3-4_4-4_5" },
            { "Heavy-Weight Spaceship 7x5", "0_2-0_3-1_0-1_5-2_6-3_0-3_6-4_1-4_2-4_3-4_4-4_5-4_6" },
            { "Pulsar 15x15", "0_4-0_10-1_4-1_10-2_4-2_5-2_9-2_10-4_0-4_1-4_2-4_5-4_6-4_8-4_9-4_12-4_13-4_14-5_2-5_4-5_6-5_8-5_10-5_12-6_4-6_5-6_9-6_10-8_4-8_5-8_9-8_10-9_2-9_4-9_6-9_8-9_10-9_12-10_0-10_1-10_2-10_5-10_6-10_8-10_9-10_12-10_13-10_14-12_4-12_5-12_9-12_10-13_4-13_10-14_4-14_10" },
            { "Glider Gun 36x9", "0_24-1_22-1_24-2_12-2_13-2_20-2_21-2_34-2_35-3_11-3_15-3_20-3_21-3_34-3_35-4_0-4_1-4_10-4_16-4_20-4_21-5_0-5_1-5_10-5_14-5_16-5_17-5_22-5_24-6_10-6_16-6_24-7_11-7_15-8_12-8_13" },
            { "Block-Laying Switch Engine 29x28", "0_18-1_1-1_2-1_3-1_12-1_18-2_0-2_4-2_11-2_19-3_1-3_2-3_12-3_13-3_14-3_15-3_18-3_19-4_3-4_4-4_6-4_7-4_17-4_18-4_19-5_5-5_6-5_18-5_20-6_19-6_27-6_28-7_19-7_27-7_28-18_7-18_8-19_7-19_8-26_15-26_16-27_15-27_16" }
        };
    }

    public Board? CreateBoardWithTemplate(string templateName, int width, int height)
    {
        string[] strings = templateName.Split(' ');
        string dim = strings[strings.Length - 1];

        string[] dims = dim.Split("x");
        if (int.Parse(dims[1]) > height || int.Parse(dims[0]) > width)
            return null;

        Board board = new Board(width, height);

        string pattern = Templates[templateName];

        int widthOffset = (width - int.Parse(dims[0])) / 2;
        int heightOffset = (height - int.Parse(dims[1])) / 2;

        string[] cells = pattern.Split("-");
        foreach (string cell in cells)
        {
            string[] coords = cell.Split("_");
            board.States[0].Cells[int.Parse(coords[1]) + widthOffset, int.Parse(coords[0]) + heightOffset] = true;
        }

        return board;
    }
}
