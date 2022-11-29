using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public PlayerInput MabelInput;
    private InputAction pause;
    public bool isPaused;
    public PauseMenu PauseMenu;
    private void Awake()
    {
        pause = MabelInput.actions["Pause"];
        pause.started += PauseAction;
        isPaused = false;
    }
    private void Update()
    {
        if (isPaused == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void PauseAction(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
    }
}
