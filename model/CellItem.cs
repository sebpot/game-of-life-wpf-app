using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PlaNet_Projekt_1.model;

public class CellItem
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Left { get; set; }
    public double Top { get; set; }
    public Brush Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    

    public CellItem() 
    {
    }
}
