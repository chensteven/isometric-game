using UnityEngine;
using System.Collections;

public class GrowShrinkLoop : MonoBehaviour
{

    public float Speed = 1;
    public float Amplitude = 1;
    private float initialScale;

    // Use this for initialization
    void Start()
    {
        initialScale = transform.localScale.y;
    }

    void Update()
    {
        var scale = Sine(Amplitude, Speed, Time.time, 0) + Amplitude + initialScale;
        transform.localScale = new Vector3(scale,scale,scale);
    }

    private float Sine(float amplitute, float frequency, float time, float phase)
    {
        return amplitute * Mathf.Cos(frequency * time + phase);
    }

}
