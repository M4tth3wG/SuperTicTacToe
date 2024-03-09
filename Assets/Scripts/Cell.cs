using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public enum SignType
    {
        Blank,
        Circle,
        Cross
    }

    public SignType Sign { get; set; }
    public int XPosition { get;}
    public int YPosition { get;}

    public Cell(int x, int y)
    {
        XPosition = x;
        YPosition = y;
        Sign = SignType.Blank;
    }
}
