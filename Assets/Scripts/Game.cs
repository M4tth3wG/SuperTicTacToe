using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private readonly Cell.SignType playerSign = Cell.SignType.Cross;
    private readonly Cell.SignType enemySign = Cell.SignType.Circle;
    private readonly int winCondition = 5;

    private bool isStoped = true;
    private int boardSize = 10;
    private int turnCounter = 0;

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
        turnCounter = 0;
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

        if (!isStoped)
        {
            turnCounter++;

            Cell currentCell = cells[x, y];
            currentCell.Sign = playerSign;

            board.UpdateCell(currentCell);
            enemyController.UpdateAvailableCells(currentCell);

            if (CheckWin(currentCell))
            {
                Debug.Log("Player wins!");
                isStoped=true;
            }
            else if (turnCounter == boardSize * boardSize)
            {
                isStoped = true;
                Debug.Log("Draw!");
            }
            else
            {
                EnemyTurn();
            }
        }
    }

    private void EnemyTurn()
    {
        Cell enemyCell = enemyController.PickNextCell();

        if (enemyCell != null)
        {
            turnCounter++;
            enemyCell.Sign = enemySign;
            board.UpdateCell(enemyCell);
            enemyController.UpdateAvailableCells(enemyCell);
        }

        if (CheckWin(enemyCell))
        {
            Debug.Log("Enemy wins!");
            isStoped = true;
        }
        else if (turnCounter == boardSize * boardSize)
        {
            isStoped = true;
            Debug.Log("Draw!");
        }
    }

    private bool CheckWin(Cell cell)
    {
        return
            CheckLine(cell, 0, 1)
            ||
            CheckLine(cell, 1, 0)
            ||
            CheckLine(cell, 1, 1)
            ||
            CheckLine(cell, 1, -1);
    }

    private bool CheckLine(Cell cell, int xIncrement, int yIncrement)
    {
        return CountDirection(cell, xIncrement, yIncrement) + CountDirection(cell, xIncrement, yIncrement, true) == winCondition - 1;
    }

    private int CountDirection(Cell cell, int xIncrement, int yIncrement, bool reverse = false)
    {
        int counter = 0;
        Cell currentCell = cell;

        if (reverse)
        {
            xIncrement *= -1;
            yIncrement *= -1;
        }

        int x = cell.XPosition + xIncrement;
        int y = cell.YPosition + yIncrement;

        for (; x >= 0 && y >= 0 && x < boardSize && y < boardSize; x += xIncrement, y += yIncrement)
        {
            currentCell = cells[x, y];

            if (currentCell.Sign == cell.Sign)
            {
                counter++;
            }
            else
            {
                break;
            }
        }

        return counter;
    }
}
