using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private bool isStoped = true;
    private bool playerTurn = true;
    private Cell.SignType playerSign = Cell.SignType.Cross;
    private int boardSize = 10;
    private Board board;
    private Cell[,] cells;

    void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    void NewGame()
    {
        cells = CreateCells(boardSize);
        board.Create(boardSize);

        isStoped = false;
        playerTurn = true;
    }

    Cell[,] CreateCells(int size)
    {
        Cell[,] newCells = new Cell[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                newCells[x, y] = new Cell(x, y);
            }
        }

        return newCells;
    }

    public void OnPlayerClick(int x, int y)
    {
        Debug.Log($"X: {x}, Y: {y}");

        if (!isStoped && playerTurn)
        {
            Cell currentCell = cells[x, y];
            currentCell.Sign = playerSign;
            playerTurn = false;


            board.UpdateCell(currentCell);
        }
    }
}
