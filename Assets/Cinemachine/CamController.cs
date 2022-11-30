using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamController : MonoBehaviour
{
    public int Camera = 0;
    public Animator animator;
    public void Update()
    {
        if (Camera == 0)
        {
            animator.Play("MainCam");
        }
        else if (Camera == 1)
        {
            animator.Play("HallCam");
        }
        else if (Camera == 2)
        {
            animator.Play("JumpTutorialCam");
        }
        else if (Camera == 3)
        {
            animator.Play("BalconyCam");
        }
        else if (Camera == 4)
        {
            animator.Play("ObservationCam");
        }
    }

}
