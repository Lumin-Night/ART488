using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallCam : MonoBehaviour
{
    public CamController CamController;
    public int Cam;
    
    public Collider Mabel;
    private void OnTriggerEnter(Collider other)
    {
        if (other == Mabel)
        {
            CamController.Camera = Cam;
        }
    }
}
