using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public TutorialMainMenuPresenter TutorialMainMenuPresenter;
    public VisualElement SettingMenu;
    public Button backButton;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        SettingMenu = root.Q<VisualElement>("SettingMenu"); ; 
        backButton = root.Q<Button>("Back");
        backButton.clicked += BackButtonPressed;

    }
    void BackButtonPressed()
    {
        SettingMenu.style.visibility = Visibility.Hidden;
        TutorialMainMenuPresenter.MainMenu.style.visibility = Visibility.Visible;
    }


}