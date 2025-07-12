using UnityEngine;

public class GlitchAnimator : MonoBehaviour
{
    public Material glitchMaterial;
    public float glitchSpeed = 1.0f;

    void Update()
    {
        float intensity = Mathf.PingPong(Time.time * glitchSpeed, 0.3f);
        glitchMaterial.SetFloat("_Intensity", intensity);
    }
}
