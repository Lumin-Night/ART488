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
    public Light HeaderLight;

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
                HeaderLight.color = Color.red;
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
                HeaderLight.color = Color.white;
            }
        }

    }

    private void Update()
    {
        positionX = Input.GetAxis("Horizontal") * 1.2f;
        positionY = Input.GetAxis("Vertical") * 1.2f;

        transform.localPosition = new Vector3(positionX, 0, positionY);
    }



}




