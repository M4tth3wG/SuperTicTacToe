using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class CellButtonController : MonoBehaviour
{
    public Sprite blankSprite;
    public Sprite circleSprite;
    public Sprite crossSprite;

    public Dictionary<Cell.SignType, Sprite> spriteDict;

    void Awake()
    {
        spriteDict = new Dictionary<Cell.SignType, Sprite>()
        {
            { Cell.SignType.Blank, blankSprite},
            { Cell.SignType.Circle, circleSprite},
            { Cell.SignType.Cross, crossSprite},
        };
    }

    public void UpdateButton(Cell.SignType sign)
    {
        switch (sign) {
            case Cell.SignType.Circle:
            case Cell.SignType.Cross:
                GetComponent<Button>().enabled = false;
            break;
        
            default:
                GetComponent<Button>().enabled = true;
            break; 
        }

        GetComponent<Button>().image.sprite = spriteDict[sign];
    }
}
