using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public void UpdateDisplay()
    {
        var sizeTextField = gameObject.transform.parent.Find("SizeText").GetComponent<TextMeshProUGUI>();

        var slider = GetComponent<Slider>();

        sizeTextField.text = slider.value.ToString();
    }
}
