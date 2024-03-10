using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    private readonly string preGameButtonLabel = "Start";
    private readonly string inGameButtonLabel = "Restart";
    private readonly string postGameButtonLabel = "New game";
    
    public GameObject sliderObject;
    public GameObject startButtonObject;
    public GameObject sizeDisplayFieldObject;

    public Slider Slider { get { return sliderObject.GetComponent<Slider>(); } }
    public Button StartButton { get { return startButtonObject.GetComponent<Button>(); } }
    public TextMeshProUGUI SizeDisplay { get { return sizeDisplayFieldObject.GetComponent<TextMeshProUGUI>(); } }

    public void UpdateDisplay()
    {
        SizeDisplay.text = Slider.value.ToString();
    }

    public void SetPreGameButton()
    {
        StartButton.GetComponentInChildren<TextMeshProUGUI>().text = preGameButtonLabel;
    }

    public void SetInGameButton()
    {
        StartButton.GetComponentInChildren<TextMeshProUGUI>().text = inGameButtonLabel;
    }

    public void SetPostGameButton()
    {
        StartButton.GetComponentInChildren<TextMeshProUGUI>().text = postGameButtonLabel;
    }
}
