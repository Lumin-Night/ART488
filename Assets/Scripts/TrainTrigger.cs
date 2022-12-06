using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public GameObject Train;
    public Collider Mabel;

    private void OnTriggerEnter(Collider other)
    {
        if(other == Mabel)
        {
            Train.SetActive(true);
        }
    }
}
