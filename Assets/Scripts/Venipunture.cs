using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using BNG;
using Unity.VisualScripting;
using UnityEngine.Video;
using TMPro;

public class Venipunture : MonoBehaviour
{


    [Header("Scripts")]
    public StartKeyHoldController startKeyHoldController;
    public WaterTapController waterTapController;
    public MedicalGloveController gloveController;
    public SpriteTrigger spriteTrigger;
    public CottonSpriteApplier cottonSpriteApplier;
    public BloodCollectionTrigger bloodCollectionTrigger;
    public Destroyer destroyer;

    [Header("Description")]
    public Transform _descriptionPanel;
    public GameObject GloveInfo;
    public GameObject WaterInfo;


    [Header("SnapZone Slots")]
    public GameObject ButterflyNeedleSlot;
    public GameObject collectionTubeSlot;
    public GameObject BandSlot;


    [Header("Medical Items & Triggers")]
    public GameObject _cottonBox;
    public GameObject _band;
    public GameObject _spriteTrigger;
    public GameObject _collectionTube;
    public GameObject _butterflyNeedle;
    public GameObject _gloveBox;
    public GameObject _vein;





    [Header("Annotations")]
    
    public List<GameObject> Annotations;
   

    [Serializable]
    class Steps
    {
        [SerializeField]
        public VideoClip _clip;

        [SerializeField]
        public string _description;
    }

    [SerializeField]
    public VideoPlayer _descriptionVideoPlayer;

    [SerializeField]
    public TextMeshProUGUI _descriptionText;


    [SerializeField]
    List<Steps> m_StepList = new List<Steps>();

    int m_CurrentStepIndex = 0;

    


    IEnumerator activeThread;

    public enum VenipuntureSteps
    {
        startTrigger,
        handwash,
        glovesTrigger,
        tronequetTrigger,
        cottonSpriteTrigger,
        cottonApplierTrigger,
        canullaSlotTrigger,
        tronequetRemovalTrigger,
        collectionTubeSlotTrigger,
        bloodCollectionTrigger,
        collectionTubeRemovalTrigger,
        canullaRemovalTrigger,
        endTrigger,
    }


    private VenipuntureSteps activeStep = 0;


   





    public void Annotator(int index)
    {
        if (Annotations == null || Annotations.Count == 0)
        {
            Debug.LogWarning("The list of GameObjects is empty or not set.");
            return;
        }

        if (index < 0 || index >= Annotations.Count)
        {
            Debug.LogWarning("Index out of range.");
            return;
        }

        for (int i = 0; i < Annotations.Count; i++)
        {
            Annotations[i].SetActive(i == index);
        }
    }
    public void Next()
    {
        _descriptionVideoPlayer.Stop();
        m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
        _descriptionVideoPlayer.clip = m_StepList[m_CurrentStepIndex]._clip;
        _descriptionVideoPlayer.Play();
        _descriptionText.text = m_StepList[m_CurrentStepIndex]._description;
    }


    private void Start()
    {
        VenipuntureManager(0);
        _band.GetComponent<Grabbable>().enabled = false;
        _spriteTrigger.GetComponent<Collider>().enabled = false;
        _collectionTube.GetComponent<Grabbable>().enabled = false;
        _butterflyNeedle.GetComponent<Grabbable>().enabled = false;
        _gloveBox.GetComponent<Collider>().enabled = false;
        _vein.GetComponent<Collider>().enabled = false;

        ButterflyNeedleSlot.GetComponent<Collider>().enabled = false;
        collectionTubeSlot.GetComponent<Collider>().enabled = false;
        BandSlot.GetComponent<Collider>().enabled = false;

        GloveInfo.SetActive(false);
        WaterInfo.SetActive(false);
    }


    public void Update()
    {
        if (InputBridge.Instance.BButtonDown || Input.GetKeyDown(KeyCode.H))
        {
            _descriptionPanel.gameObject.SetActive(!_descriptionPanel.gameObject.activeSelf);
        }
    }


    public void VenipuntureManager(int newStep = -1)
    {

        if (activeThread != null)
        {
            StopCoroutine(activeThread);
        }
        if (newStep == -1)
        {
            activeStep++;
        }
        else
        {

            activeStep = (VenipuntureSteps)newStep;
        }

        switch (activeStep)
        {
            case VenipuntureSteps.startTrigger:

                Debug.Log(activeStep);
                Debug.Log(m_CurrentStepIndex);
                StartTriggerVerifier();
                break;

            case VenipuntureSteps.handwash:
                WaterInfo.SetActive(true);
                Debug.Log(m_CurrentStepIndex);
                Annotator(0);
                Debug.Log(activeStep);
                HandwashTriggerVerifier();
                break;


            case VenipuntureSteps.glovesTrigger:
                Annotator(1);
                WaterInfo.SetActive(false);
                GloveInfo.SetActive(true);
                Debug.Log(m_CurrentStepIndex);
                _gloveBox.GetComponent<Collider>().enabled = true;
                Debug.Log(activeStep);
                glovesTriggerVerifier();
                BandSlot.GetComponent<Collider>().enabled = true;
                break;


            case VenipuntureSteps.tronequetTrigger:
                Annotator(2);
                GloveInfo.SetActive(false);
                Debug.Log(m_CurrentStepIndex);
                _band.GetComponent<Grabbable>().enabled = true;
                TronequetTriggerVerifier();
                Debug.Log(activeStep);
                _spriteTrigger.GetComponent<Collider>().enabled = true;
                break;


            case VenipuntureSteps.cottonSpriteTrigger:
                Annotator(4); Debug.Log(m_CurrentStepIndex);
                BandSlot.GetComponent<SnapZone>().CanRemoveItem = false;
                BandSlot.GetComponent<StepVerifier>().isTriggered = false;
                Debug.Log(activeStep);
                CottonSpriteTriggerVerifer();
                _vein.GetComponent<Collider>().enabled = true;
                break;

            case VenipuntureSteps.cottonApplierTrigger:
                Annotator(5); Debug.Log(m_CurrentStepIndex);
                CottonSpriteApplierTriggerVerifer();
                Debug.Log(activeStep);
                ButterflyNeedleSlot.gameObject.SetActive(true);
                ButterflyNeedleSlot.GetComponent<Collider>().enabled = true;
                break;

            case VenipuntureSteps.canullaSlotTrigger:
                
                Annotator(6); Debug.Log(m_CurrentStepIndex);
                _butterflyNeedle.GetComponent<Grabbable>().enabled = true;
                CanullaSlotTriggerVerifier();
                Debug.Log(activeStep);
                break;

            case VenipuntureSteps.tronequetRemovalTrigger:
                Debug.Log(m_CurrentStepIndex);

                BandSlot.GetComponent<SnapZone>().CanRemoveItem = true;
                ButterflyNeedleSlot.GetComponent<SnapZone>().CanRemoveItem = false;
                TronequetRemovalTriggerVerifier();
                Debug.Log(activeStep);
                collectionTubeSlot.GetComponent<Collider>().enabled = true;
                break;


            case VenipuntureSteps.collectionTubeSlotTrigger:
                _collectionTube.GetComponent<Grabbable>().enabled= true;
                CollectionTubeSlotTriggerVerifier();
                Debug.Log(activeStep); Debug.Log(m_CurrentStepIndex);
                break;


            case VenipuntureSteps.bloodCollectionTrigger:
                
                collectionTubeSlot.GetComponent<SnapZone>().CanRemoveItem = false;
                BloodCollectionTriggerVerifier();
                Debug.Log("1"); Debug.Log(m_CurrentStepIndex);
                break;


            case VenipuntureSteps.collectionTubeRemovalTrigger:
                collectionTubeSlot.GetComponent<SnapZone>().CanRemoveItem = true;
                collectionTubeSlot.GetComponent<StepVerifier>().isTriggered = false;
                CollectionTubeRemovalTriggerVerifer();
                Debug.Log(activeStep); Debug.Log(m_CurrentStepIndex);
                break;


            case VenipuntureSteps.canullaRemovalTrigger:
                ButterflyNeedleSlot.GetComponent <SnapZone>().CanRemoveItem = true;
                ButterflyNeedleSlot.GetComponent<StepVerifier>().isTriggered = false;
                CanullaRemovalTriggerVerifier();
                Debug.Log(activeStep); Debug.Log(m_CurrentStepIndex);
                break;


            case VenipuntureSteps.endTrigger:
                Destroy(Annotations[7]);
                Debug.Log(activeStep); Debug.Log(m_CurrentStepIndex);
                break;


        }


    }
    

    private void BloodCollectionTriggerVerifier()
    {
       
        activeThread = BloodCollectionTriggerVerifierAction();
        StartCoroutine(BloodCollectionTriggerVerifierAction());
    }

    private IEnumerator BloodCollectionTriggerVerifierAction()
    {
        while (true)
        {
           

            if (bloodCollectionTrigger.isTriggered)
            {
                break;
            } 
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }

    private void TronequetRemovalTriggerVerifier()
    {
        activeThread = TronequetRemovalTriggerVerifierAction();
        StartCoroutine(TronequetRemovalTriggerVerifierAction());
    }


    private IEnumerator TronequetRemovalTriggerVerifierAction()
    {
        while (true)
        {
            

            if (BandSlot.GetComponent<StepVerifier>().isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }


    private void CanullaRemovalTriggerVerifier()
    {

        activeThread = CanullaRemovalTriggerVerifierAction();
        StartCoroutine(CanullaRemovalTriggerVerifierAction());
    }

    private IEnumerator CanullaRemovalTriggerVerifierAction()
    {
        while (true)
        {
            Annotator(7);
            
            if (ButterflyNeedleSlot.GetComponent<StepVerifier>().isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }

    private void CollectionTubeRemovalTriggerVerifer()
    {
        activeThread = CollectionTubeRemovalTriggerVeriferAction();
        StartCoroutine(CollectionTubeRemovalTriggerVeriferAction());
    }

    private IEnumerator CollectionTubeRemovalTriggerVeriferAction()
    {
        while (true)
        {
            Annotator(9);
               
            
            if (collectionTubeSlot.GetComponent<StepVerifier>().isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }

    private void CollectionTubeSlotTriggerVerifier()
    {
        activeThread = collectionTubeSlotTriggerVerifierAction();
        StartCoroutine(collectionTubeSlotTriggerVerifierAction());
    }

    private IEnumerator collectionTubeSlotTriggerVerifierAction()
    {
        while (true)
        {
            if (_collectionTube.GetComponent<Grabbable>().BeingHeld)
            {
                collectionTubeSlot.GetComponent<SkinnedMeshRenderer>().enabled = true;
                Annotator(9);
            }
            if (collectionTubeSlot.GetComponent<StepVerifier>().isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }

    private void CanullaSlotTriggerVerifier()
    {
        activeThread = canullaSlotTriggerVerifierAction();
        StartCoroutine(canullaSlotTriggerVerifierAction());
    }

    private IEnumerator canullaSlotTriggerVerifierAction()
    {
        while (true)
        {
            if (_butterflyNeedle.GetComponent<Grabbable>().BeingHeld)
            {
                ButterflyNeedleSlot.GetComponent<SkinnedMeshRenderer>().enabled = true;
                Annotator(7);
            }
            if (ButterflyNeedleSlot.GetComponent<StepVerifier>().isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();

    }

    private void CottonSpriteApplierTriggerVerifer()
    {
        activeThread = CottonSpriteApplierTriggerVeriferAction();
        StartCoroutine(CottonSpriteApplierTriggerVeriferAction());
    }


    private IEnumerator CottonSpriteApplierTriggerVeriferAction()
    {
        while (true)
        {
            if (cottonSpriteApplier.isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }

    private void CottonSpriteTriggerVerifer()
    {
        activeThread = CottonSpriteTriggerVeriferAction();
        StartCoroutine(CottonSpriteTriggerVeriferAction());
    }

    private IEnumerator CottonSpriteTriggerVeriferAction()
    {
        while (true)
        {
            if (spriteTrigger.isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }

    private void TronequetTriggerVerifier()
    {
        activeThread = TronequetTriggerVerifierAction();
        StartCoroutine(TronequetTriggerVerifierAction());
    }

    private IEnumerator TronequetTriggerVerifierAction()
    {
        while (true)
        {
            if (_band.GetComponent<Grabbable>().BeingHeld)
            {
                Annotator(3);
            }
            if (BandSlot.GetComponent<StepVerifier>().isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();
        VenipuntureManager();
    }





    public void StartTriggerVerifier()
    {
        activeThread = StartTriggerVerifierAction();
        StartCoroutine(StartTriggerVerifierAction());

    }

    private IEnumerator StartTriggerVerifierAction()
    {
        while (true)
        {
            if (startKeyHoldController.isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Next();

        VenipuntureManager();


    }






    private void HandwashTriggerVerifier()
    {
        activeThread = HandwashTriggerVerifierAction();
        StartCoroutine(HandwashTriggerVerifierAction());

    }

    private IEnumerator HandwashTriggerVerifierAction()
    {
        while (true)
        {
            if (waterTapController.isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        /*waterTapController.gameObject.GetComponent<BoxCollider>().enabled = false;*/

        destroyer.DestroyComp(waterTapController.gameObject.GetComponent<WaterTapController>());
        Next();
        VenipuntureManager();

    }






    private void glovesTriggerVerifier()
    {
        activeThread = glovesTriggerverifierAction();
        StartCoroutine(glovesTriggerverifierAction());

    }

    private IEnumerator glovesTriggerverifierAction()
    {
        while (true)
        {
            if (gloveController.isTriggered)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        destroyer.DestroyComp(gloveController.gameObject.GetComponent<MedicalGloveController>());
        Next();
        VenipuntureManager();
    }






}
