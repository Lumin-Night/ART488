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
    public VisualElement MainMenu;
    public SettingsMenu SettingsMenu;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("Start");
        settingsButton = root.Q<Button>("Settings");
        quitButton = root.Q<Button>("Quit");
        MainMenu = root.Q<VisualElement>("MainMenu");

        startButton.clicked += StartButtonPressed;
        quitButton.clicked += QuitButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
    }

    private void Update()
    {
        if(SettingsMenu.settingsActive == true)
        {
            MainMenu.style.visibility = Visibility.Hidden;
        }
        else
        {
            MainMenu.style.visibility = Visibility.Visible;
        }
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void SettingsButtonPressed()
    {
        SettingsMenu.settingsActive = true;
    }

    void QuitButtonPressed()
    {
        Application.Quit();
    }
}
