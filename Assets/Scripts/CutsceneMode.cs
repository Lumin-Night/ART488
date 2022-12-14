using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneMode : MonoBehaviour
{
    public Animator mabelAnimations;
    [SerializeField] private string cutsceneName;
    [SerializeField] private bool cutsceneActive;

    private void OnTriggerEnter(Collider other)
    {
        mabelAnimations.SetBool(cutsceneName, cutsceneActive);
    }

}
