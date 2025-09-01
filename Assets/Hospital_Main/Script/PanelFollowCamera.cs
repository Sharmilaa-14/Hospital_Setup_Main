using UnityEngine;

public class PanelFacePlayer : MonoBehaviour
{
    public GameObject panel;      // assign your panel
    public Transform xrRig;       // XR Rig root (Player)
    public float rotationSpeed = 2f; // speed of panel rotation

    void Start()
    {
        if (panel != null)
            panel.SetActive(false); // hide at start
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(panel != null)
                panel.SetActive(true); // show panel
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(panel != null)
                panel.SetActive(true); // hide panel
        }
    }

    void Update()
{
    if(panel.activeSelf && xrRig != null)
    {
        // Direction from panel to player
        Vector3 direction = xrRig.position - panel.transform.position;

        // Ignore vertical rotation for upright panel
        direction.y = 0;

        if(direction != Vector3.zero)
        {
            // Get rotation looking at player
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Fix 180-degree flip by rotating around Y if needed
            lookRotation *= Quaternion.Euler(0, 180, 0);

            // Smooth rotation
            panel.transform.rotation = Quaternion.Slerp(panel.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}



}
