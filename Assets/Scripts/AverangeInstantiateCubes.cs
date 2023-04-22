using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverangeInstantiateCubes : MonoBehaviour
{
    public AudioPeer _audioPeer;
    public int band;
    public float Scale, multiplier;
    public float DebugValue;
    public bool showDebug = false, useBuffer;
    public float averageSamplesBuffer;

    public float minbuferTrashold = 0.1f, maxBufferTrashold = 0.9f, minSpeed = 0.005f, maxSpeed = 0.01f;
    // Update is called once per frame
    void Update()
    {

        transform.localScale = (useBuffer) ? new Vector3(Scale, Scale + (Buffer(_audioPeer.averageSamples[band]) * multiplier), Scale) : new Vector3(Scale, Scale + (_audioPeer.averageSamples[band] * multiplier), Scale);
        if (System.Math.Round(transform.localScale.y, 3) >= DebugValue && showDebug) { Debug.Log("Yes"); }
    }
    float Buffer(float sample)
    {

        if (sample + minbuferTrashold < averageSamplesBuffer)
        {
            averageSamplesBuffer = sample;
        }
        else if (sample < averageSamplesBuffer)
        {
            averageSamplesBuffer -= minSpeed;
        }
        if (sample > averageSamplesBuffer + maxBufferTrashold)
        {
            averageSamplesBuffer = sample;
        }
        else if (sample > averageSamplesBuffer)
        {
            averageSamplesBuffer += maxSpeed;
        }
        return averageSamplesBuffer;
    }


}
