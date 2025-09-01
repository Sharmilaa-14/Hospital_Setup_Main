using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonDispenser : MonoBehaviour
{
    public Grabber LeftGrabber;

    public Grabber RightGrabber;

   
    public GameObject DispenserObject;

    


    public void GrabAmmo(Grabber grabber)
    {
        
        GameObject _cotton = DispenserObject;
        if (_cotton != null)
        {
            GameObject ammo = Instantiate(_cotton, grabber.transform.position, grabber.transform.rotation) as GameObject;
            Grabbable g = ammo.GetComponent<Grabbable>();

            

            // Offset to hand
            ammo.transform.parent = grabber.transform;
            ammo.transform.localPosition = -g.GrabPositionOffset;
            ammo.transform.parent = null;

            grabber.GrabGrabbable(g);
        }
    }

}