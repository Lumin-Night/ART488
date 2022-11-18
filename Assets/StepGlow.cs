using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepGlow : MonoBehaviour
{
    public Animator Platform;
    [SerializeField] private bool isGlow;

    private void OnTriggerEnter(Collider other)
    {
        isGlow = true;
        Platform.SetBool("Glow", isGlow);
    }
    private void OnTriggerExit(Collider other)
    {
        isGlow = false;
        Platform.SetBool("Glow", isGlow);
    }


}
