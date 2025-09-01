using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.Rendering;

public class StartKeyHoldController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPanel;
    [SerializeField]
    private GameObject _infoPanel;
    [SerializeField]
    private GameObject _descriptionPanel;
    [SerializeField]
    private GameObject _modeule;
    [SerializeField]
    private GameObject _patientSitting;
    [SerializeField]
    private GameObject _patientSleeping;

    public bool isTriggered;


    public void Start()
    {
        _startPanel.SetActive(true);
        _infoPanel.SetActive(false);
        _descriptionPanel.SetActive(false);
        _modeule.SetActive(false); 
        isTriggered = false;
    }
  

    public void Update()
    {
        if (_startPanel.activeInHierarchy)
        {
            if (InputBridge.Instance.LeftTriggerDown || InputBridge.Instance.RightTriggerDown)
            {
                isTriggered = true;
                _startPanel.SetActive(false);
                _infoPanel.SetActive(true);
                _descriptionPanel.SetActive(true);
                _modeule.SetActive(true);
            }
            
        }
    }
}
