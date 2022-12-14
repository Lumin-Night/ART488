using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationCam : MonoBehaviour
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
    private void OnTriggerExit(Collider other)
    {
        if(other == Mabel)
        {
            CamController.Camera = 0;
        }
    }
}
