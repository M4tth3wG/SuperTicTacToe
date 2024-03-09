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
    private Cell.SignType enemySign = Cell.SignType.Circle;
    private int boardSize = 10;
    private Board board;
    private Cell[,] cells;
    private EnemyController enemyController;

    void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        board.Clear();
        boardSize = (int)GetComponentInChildren<Slider>().value;

        cells = CreateCells(boardSize);
        board.Create(boardSize);

        enemyController = new EnemyController(cells);

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
            //TODO uncoment
            //playerTurn = false;


            board.UpdateCell(currentCell);
            enemyController.UpdateAvailableCells(currentCell);
        }

        Cell enemyCell = enemyController.PickNextCell();
        enemyCell.Sign = enemySign; 
        board.UpdateCell(enemyCell);
        enemyController.UpdateAvailableCells(enemyCell);
    }
}
