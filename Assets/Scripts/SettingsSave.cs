using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSave : MonoBehaviour
{
    public int bloom;
    public int dof;
    public int fullscreen;

    private void Start()
    {
        PlayerPrefs.SetInt("Fullscreen", fullscreen);
    }
}
