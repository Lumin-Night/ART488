using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button settingsButton;
    public Button quitButton;
    public VisualElement Background;
    public SettingsMenu SettingsMenu;
    public Pause Pause;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        resumeButton = root.Q<Button>("Resume");
        settingsButton = root.Q<Button>("Settings");
        quitButton = root.Q<Button>("Quit");
        Background = root.Q<VisualElement>("Background");

        resumeButton.clicked += ResumeButtonPressed;
        quitButton.clicked += QuitButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
    }

    private void Update()
    {
        if (SettingsMenu.settingsActive == true)
        {
            Background.style.visibility = Visibility.Hidden;
        }
        else if(SettingsMenu.settingsActive == false && Pause.isPaused)
        {
            Background.style.visibility = Visibility.Visible;
        }
        else if(SettingsMenu.settingsActive == false && Pause.isPaused == false)
        {
            Background.style.visibility = Visibility.Hidden;
        }
    }

    public void ResumeButtonPressed()
    {
        Pause.isPaused = false;
    }
    private void SettingsButtonPressed()
    {
        SettingsMenu.settingsActive = true;
    }
    private void QuitButtonPressed()
    {
        Application.Quit();
    }
}
