using System;
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

    public override bool Equals(object obj)
    {
        return obj is Cell cell &&
               XPosition == cell.XPosition &&
               YPosition == cell.YPosition;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(XPosition, YPosition);
    }
}
