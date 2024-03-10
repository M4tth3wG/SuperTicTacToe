using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    private readonly int pixelsPerCell = 48;
    private readonly int spacePixels = 4;

    public GameObject buttonPrefab;
    public GameObject[,] buttons;

    public void Create(int size)
    {
        int pixels = pixelsPerCell * size + spacePixels * (size - 1);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pixels);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pixels);

        GetComponent<GridLayoutGroup>().constraintCount = size;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(pixelsPerCell, pixelsPerCell);
        GetComponent<GridLayoutGroup>().spacing = new Vector2(spacePixels, spacePixels);

        buttons = new GameObject[size, size];

        for (int x = 0; x < size; x++) { 
            for (int y = 0; y < size; y++)
            {
                int currentX = x;
                int currentY = y;

                GameObject cellButton = Instantiate(buttonPrefab, GetComponent<Transform>());
                cellButton.GetComponent<Button>().onClick.AddListener(() => GetComponentInParent<Game>().OnPlayerClick(currentX, currentY));
                buttons[x, y] = cellButton;
            }
        }
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void Disable()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Button>().enabled = false;
        }
    }

    public void Show()
    {
        GetComponent<Image>().enabled = true;
    }

    public void Hide()
    {
        GetComponent<Image>().enabled = false;
    }

    public void UpdateCell(Cell cell)
    {
        GameObject cellButton = buttons[cell.XPosition, cell.YPosition];
        cellButton.GetComponent<CellButtonController>().UpdateButton(cell.Sign);
    }
}
