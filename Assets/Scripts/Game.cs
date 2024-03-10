using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private readonly Cell.SignType playerSign = Cell.SignType.Cross;
    private readonly Cell.SignType enemySign = Cell.SignType.Circle;
    private readonly int winCondition = 5;
    private readonly string playerWinMessage = "Player wins!";
    private readonly string enemyWinMessage = "Enemy wins!";
    private readonly string tieWinMessage = "Tie!";

    private bool isStoped = true;
    private int boardSize = 10;
    private int turnCounter = 0;

    private Board board;
    private Cell[,] cells;
    private EnemyController enemyController;

    public TextMeshProUGUI winMessageField;
    public PanelController panelController;

    void Awake()
    {
        board = GetComponentInChildren<Board>();
        board.Hide();
        panelController.SetPreGameButton();
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
        boardSize = (int)panelController.Slider.value;
        panelController.SetInGameButton();

        cells = CreateCells(boardSize);
        board.Create(boardSize);
        board.Show();

        enemyController = new EnemyController(cells);

        isStoped = false;
        turnCounter = 0;
        winMessageField.text = null;
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
                FinishGame(playerWinMessage);
            }
            else if (turnCounter == boardSize * boardSize)
            {
                FinishGame(tieWinMessage);
            }
            else
            {
                EnemyTurn();
            }
        }
    }

    private void FinishGame(string message)
    {
        isStoped = true;
        Debug.Log(message);
        winMessageField.text = message;
        panelController.SetPostGameButton();
        board.Disable();
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
            FinishGame(enemyWinMessage);
        }
        else if (turnCounter == boardSize * boardSize)
        {
            FinishGame(tieWinMessage);
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
