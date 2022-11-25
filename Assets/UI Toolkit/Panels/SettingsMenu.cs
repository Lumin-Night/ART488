using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class SettingsMenu : MonoBehaviour
{
    public PostProcessVolume PostProcess;
    public TutorialMainMenuPresenter TutorialMainMenuPresenter;
    public VisualElement SettingMenu;
    public Button backButton;
    public Button Fullscreen;
    public Button BloomButton;
    public Button dofButton;
    [SerializeField] private bool FS;
    [SerializeField] private int FSI;
    [SerializeField] private bool BL;
    [SerializeField] private int BLI;
    [SerializeField] private bool DF;
    [SerializeField] private int DFI;
    Bloom bloom;
    DepthOfField dof;
    void Start()
    {
        BL = true;
        DF = true;
        PlayerPrefs.GetInt("Fullscreen");
        PlayerPrefs.GetInt("Bloom");
        PlayerPrefs.GetInt("dof");
        var root = GetComponent<UIDocument>().rootVisualElement;
        PostProcess.profile.TryGetSettings(out bloom);
        PostProcess.profile.TryGetSettings(out dof);

        SettingMenu = root.Q<VisualElement>("SettingMenu"); ; 
        backButton = root.Q<Button>("Back");
        backButton.clicked += BackButtonPressed;

        Fullscreen = root.Q<Button>("Fullscreen");
        Fullscreen.clicked += FullscreenPressed;

        BloomButton = root.Q<Button>("Bloom");
        BloomButton.clicked += BloomPressed;

        dofButton = root.Q<Button>("DepthofField");
        dofButton.clicked += DoFPressed;

    }
    void BackButtonPressed()
    {
        SettingMenu.style.visibility = Visibility.Hidden;
        TutorialMainMenuPresenter.MainMenu.style.visibility = Visibility.Visible;
    }

    void FullscreenPressed()
    {
        FS = !FS;
        FSI = Convert.ToInt32(FS);
        PlayerPrefs.SetInt("Fullscreen", FSI);
        Screen.fullScreen = FS;
    }

    void BloomPressed()
    {
        BL = !BL;
        BLI = Convert.ToInt32(BL);
        PlayerPrefs.SetInt("Bloom", BLI);
        bloom.active = BL;
    }

    void DoFPressed()
    {
        DF = !DF;
        DFI = Convert.ToInt32(DF);
        PlayerPrefs.SetInt("dof", DFI);
        dof.active = DF;
    }

}