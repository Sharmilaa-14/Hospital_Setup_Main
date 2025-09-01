using UnityEngine;

public class GlowBlink : MonoBehaviour
{
    public Renderer rend;
    public Color glowColor = Color.yellow;
    public float blinkSpeed = 2f;
    public float maxGlow = 2f;

    void Start()
    {
        if (rend == null) rend = GetComponent<Renderer>();
        rend.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        // PingPong gives a repeating up-down value
        float emission = Mathf.PingPong(Time.time * blinkSpeed, maxGlow);
        Color finalColor = glowColor * Mathf.LinearToGammaSpace(emission);
        rend.material.SetColor("_EmissionColor", finalColor);
    }
}