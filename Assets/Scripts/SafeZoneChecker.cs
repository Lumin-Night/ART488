using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneChecker : MonoBehaviour
{
    private int SafeZone;
    [SerializeField] private string SafeZoneLayerName="SafeZone";
    public PlayerController3D PlayerController3D;
    [SerializeField] private int SafeZoneCounter;
    [SerializeField] private float positionX;
    [SerializeField] private float positionY;

    private void Awake()
    {
        SafeZoneCounter = 0;
        SafeZone = LayerMask.NameToLayer(SafeZoneLayerName);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == SafeZone)
        {
            SafeZoneCounter--;
            if (SafeZoneCounter <= 0)
            {                
                PlayerController3D.SetSlow();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == SafeZone)
        {
            SafeZoneCounter++;
            if (SafeZoneCounter >= 0)
            {
                PlayerController3D.ClearSlow();
            }
        }

    }

    private void Update()
    {
        positionX = Input.GetAxis("Horizontal") * .4f;
        positionY = Input.GetAxis("Vertical") * .4f;

        transform.localPosition = new Vector3(positionX, 0, positionY);
    }



}




