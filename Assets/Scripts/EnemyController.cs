using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController
{
    private Cell lastCell = null;
    private Cell[,] grid = null;
    private List<Cell> availableCells = new List<Cell>();

    public EnemyController(Cell[,] cells)
    {
        this.grid = cells;

        foreach (Cell cell in grid)
        {
            availableCells.Add(cell);
        }
    }

    public Cell PickNextCell()
    {
        Cell chosenCell;

        if (lastCell == null) { 
            chosenCell = PickRandomCell();
        }
        else
        {
            chosenCell = PickNearbyCell(lastCell);
        }

        chosenCell ??= PickRandomCell();
        lastCell = chosenCell;

        return chosenCell;
    }

    private Cell PickRandomCell()
    {
        return availableCells[Random.Range(0, availableCells.Count - 1)];
    }

    private Cell PickNearbyCell(Cell cell)
    {
        Cell chosenCell = null;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int x = cell.XPosition + i;
                int y = cell.YPosition + j;

                if (x >= 0 && x < grid.GetLength(0)
                    && y >= 0 && y < grid.GetLength(0)
                    && grid[x,y].Sign == Cell.SignType.Blank)
                {
                    chosenCell = grid[x,y];
                    break;
                }
            }
        }
        return chosenCell;
    }

    public void UpdateAvailableCells(Cell cell)
    {
        availableCells.Remove(cell);
    }
}
