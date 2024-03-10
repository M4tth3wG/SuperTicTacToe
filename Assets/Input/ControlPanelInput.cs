using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlPanelInput : MonoBehaviour
{
    public Slider slider;
    public Game game;

    public void IncrementSlider(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            slider.value++;
        }
    }

    public void DecrementSlider(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            slider.value--;
        }
    }

    public void RestartGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            game.NewGame();
        }
    }
}
