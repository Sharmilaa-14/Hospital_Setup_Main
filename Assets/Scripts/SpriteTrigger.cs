using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrigger : MonoBehaviour
{
    [SerializeField]

    private Material _stainnedCotton;


    public bool isTriggered;


    private void Start()
    {
        isTriggered = false;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cotton"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material= _stainnedCotton;


            isTriggered = true;
        }
    }
}
