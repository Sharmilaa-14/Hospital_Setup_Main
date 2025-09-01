using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCollectionTrigger : MonoBehaviour
{
    public SkinnedMeshRenderer Blood;
    public int initialValue;
    public float targetValue;
    public float animationDuration = 1f;

    private float currentLerpValue = 0f;

    public int _index;

    // Flag to control the animation state
    public bool lerper = false;
    public bool isTriggered = false;

    public void Start()
    {



        if (Blood == null)
        {
            Debug.LogError("SkinnedMeshRenderer reference is not assigned!");
            return;
        }


    }
    public void Update()
    {
        if (lerper)
        {

            // Increment the interpolation value based on the elapsed time and animation duration
            currentLerpValue += Time.deltaTime;

            // Calculate the lerp value between the two blend shape weights
            float lerpValue = Mathf.Lerp(initialValue, targetValue, currentLerpValue / animationDuration);

            Debug.Log(lerpValue); 
            // Set the blend shape weights using the lerp value
            if (Blood)
            {
                if (_index >= 0)
                {
                    Blood.SetBlendShapeWeight(_index, lerpValue);
                    

                }


                

            }

            // Check if the animation is completed
            if (currentLerpValue >= animationDuration)
            {
                currentLerpValue = 0;
                lerper = false;
                isTriggered = true;
            }
        }
    }

    public void StartLerpAnimation()
    {
        // Reset the current lerp value
        currentLerpValue = 0f;

        
            // Start the lerp animation
            lerper = true;
        
    }
}

