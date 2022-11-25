using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;
public class LoadPrefs : MonoBehaviour
{
    public int DFI;
    public int BLI;
    public bool BL;
    public bool DF;
    public PostProcessVolume PostProcess;
    Bloom bloom;
    DepthOfField dof;
    void Start()
    {
        PlayerPrefs.GetInt("Dof", DFI);
        PlayerPrefs.GetInt("Bloom", BLI);
        PostProcess.profile.TryGetSettings(out bloom);
        PostProcess.profile.TryGetSettings(out dof);

        if (BLI == 1)
        {
            BL = true;
        }
        else BL = false;
        if (BL == true)
        {
            bloom.active = true;
        }
        else bloom.active = false;

        if (DFI == 1)
        {
            DF = true;
        }
        else DF = false;
        if (DF == true)
        {
            dof.active = true;
        }
        else dof.active = false; 

    }
}
