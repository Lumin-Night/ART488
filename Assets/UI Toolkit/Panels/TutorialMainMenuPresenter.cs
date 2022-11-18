using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TutorialMainMenuPresenter : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("Start");
        settingsButton = root.Q<Button>("Settings");
        quitButton = root.Q<Button>("Quit");

        startButton.clicked += StartButtonPressed;
        quitButton.clicked += QuitButtonPressed;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void QuitButtonPressed()
    {
        Application.Quit();
    }
}
