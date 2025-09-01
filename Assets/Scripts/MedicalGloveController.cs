using BNG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MedicalGloveController : MonoBehaviour
{
    public List<SkinnedMeshRenderer> _handMesh;

    public Material _gloveMateral;

    public Material _defaulthand;


    private bool left;
    private bool right;

    public bool isTriggered;
    
    public TextMeshProUGUI GloveInfo;
    public TextMeshProUGUI DesInfo;



    public void Start()
    {
        left = false;
        right = false;
    }

    void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("lefthand"))
        {
            _handMesh[0].material = _gloveMateral;
            left = true;
            GloveInfo.text = "Put Gloves on Both the Hands";
            DesInfo.text = "Go Back & Put Gloves on Both the Hands";
        }


        if (other.gameObject.CompareTag("righthand"))
        {
            _handMesh[1].material = _gloveMateral;
            right = true;
            GloveInfo.text = "Put Gloves on Both the Hands";
            DesInfo.text = "Go Back & Put Gloves on Both the Hands";
        }


    }


    private void Update()
    {
        
        /*if (InputBridge.Instance.AButtonDown || Input.GetKeyDown(KeyCode.G))
        {
            foreach (SkinnedMeshRenderer skinnedMeshRenderer in _handMesh)
            {
                skinnedMeshRenderer.material = _defaulthand;
            }
        }*/

        if(left &&  right)
        {
            isTriggered = true;
            GloveInfo.text = "Sucess! Go Ahead with the Next Step";
        }
    }
}
