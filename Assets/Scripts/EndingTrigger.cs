using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    public GameObject Mabel;
    public GameObject EndingMabel;

    private void OnTriggerEnter(Collider other)
    {
        Mabel.SetActive(false);
        EndingMabel.SetActive(true);
    }
}
