using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMM : MonoBehaviour
{
    public AudioPeer _audioPeer;
    public float multiplier = 10, buffer = 5;
    public float max, min, counter = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.localScale = new Vector3(1, 1 + (_audioPeer.TEST * multiplier), 1);
        transform.localScale = new Vector3(1, Mathf.Lerp(transform.localScale.y, _audioPeer.TEST * multiplier, Time.deltaTime * buffer), 1);
        if (System.Math.Round(transform.localScale.y, 3) >= max && counter == 0) { counter = 1; Debug.Log("Yes"); }
        if (System.Math.Round(transform.localScale.y, 3) <= min && counter == 1) { counter = 0; }
    }
}
