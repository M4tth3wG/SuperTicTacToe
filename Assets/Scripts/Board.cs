using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public readonly int pixelsPerCell = 32;

    public GameObject buttonPrefab;
    public GameObject[,] buttons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(int size)
    {
        int pixels = pixelsPerCell * size;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pixels);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pixels);

        GetComponent<GridLayoutGroup>().constraintCount = size;

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

    public void UpdateCell(Cell cell)
    {
        GameObject cellButton = buttons[cell.XPosition, cell.YPosition];
        cellButton.GetComponent<CellButtonControler>().UpdateButton(cell.Sign);
    }
}
