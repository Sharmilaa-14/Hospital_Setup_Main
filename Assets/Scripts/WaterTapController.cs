using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterTapController : MonoBehaviour
{

    public bool isTriggered;

    public ParticleSystem Water;

    public bool left;
    public bool right;

    public TextMeshProUGUI WaterInfo;
    public TextMeshProUGUI DesInfo;
    

    private void Start()
    {
        isTriggered = false;
        left = false;
        right = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("lefthand")) /*&& other.gameObject.CompareTag("righthand"))*/
        {
            Water.gameObject.SetActive(true);
            Water.Play();
            
        }
        else if(other.gameObject.CompareTag("righthand"))
        {

            Water.gameObject.SetActive(true);
            Water.Play();
        }
        else
        {
            Water.gameObject.SetActive(false);
            Water.Stop();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("lefthand") || other.gameObject.CompareTag("righthand"))
        {
            Water.gameObject.SetActive(false);
            Water.Stop();
           
        }
        
        if (other.gameObject.CompareTag("lefthand"))
        {
            WaterInfo.text = "Wash Both the Hands";
            DesInfo.text = "Go Back & Wash Both the Hands";
            left = true;
        }else if (other.gameObject.CompareTag("righthand"))
        {
            WaterInfo.text = "Wash Both the Hands";
            DesInfo.text = "Go Back & Wash Both the Hands";
            right = true;
        }
        
    }

    private void Update()
    {
        if (left && right)
        {
            isTriggered = true;
            WaterInfo.text = "Sucess! Go Ahead with the Next Step";
            
        }
    }
}
