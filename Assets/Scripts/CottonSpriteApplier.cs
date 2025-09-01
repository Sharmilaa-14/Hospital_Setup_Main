using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSpriteApplier : MonoBehaviour
{

    public bool isTriggered;



    private void Start()
    {
        isTriggered = false; 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cotton"))
        {
            isTriggered = true;
        }
    }
}
