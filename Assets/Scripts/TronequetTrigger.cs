using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TronequetTrigger : MonoBehaviour
{


    [SerializeField]
    private Material tronequet;

    public bool isTriggered;



    private void Start()
    {
        isTriggered = false;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tronequet"))


        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.GetComponent<MeshRenderer>().material = tronequet;
            Destroy(other.gameObject);
            isTriggered = true;
        }
    }
}
