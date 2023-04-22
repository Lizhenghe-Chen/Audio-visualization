using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public AudioPeer _audioPeer;
    public int changeInactiveTimer = 1;
    public GameObject cubePrefab;
    public GameObject sun;
    public int sunIndex;
    public float SunScale;
    public AverangeInstantiateCubes sunScale;
    public float SunSize;
    public float radius = 100;
    public float cubeScale = 1;
    public bool isVertical;
    public int rangeTimer = 100;
    public float maxRange = 100f;
    public int inactiveGap = 4;
    GameObject[] _cubesSamples;

    private WaitForSeconds _waitForSeconds;
    private Vector3 intial_scate;
    void Start()
    {
        _waitForSeconds = new WaitForSeconds(changeInactiveTimer);
        intial_scate = new Vector3(cubeScale, cubeScale, cubeScale);
        //  StartCoroutine(ChangeInactive());
        InstantiateCircle();
        // InstantiateLine();
    }

    // Update is called once per frame
    void Update()
    {
        SimpleCircle();
        //for (int i = _audioPeer.ignoreFirst_Samples; i < _audioPeer.num_Samples; i++)
        //{
        //    float range;
        //    if (i >= _audioPeer.ignoreFirst_SamplesMultiply) { range = _audioPeer.samples[i] * rangeTimer; } else { range = _audioPeer.samples[i] * 100; }

        //    if (range >= maxRange) { range = maxRange; }

        //    _cubesSamples[i].transform.position = _cubesSamples[i].transform.forward * radius + this.transform.position;
        //    if (i == _audioPeer.num_Samples - 1)//is this is sun
        //    {
        //        var Size = sunScale.averageSamplesBuffer * SunScale + SunSize;
        //        _cubesSamples[i].transform.localScale = new Vector3(Size, Size, Size);
        //        return;
        //    }
        //    if (i % inactiveGap == 0)
        //    {
        //        if (isVertical) { _cubesSamples[i].transform.localScale = new Vector3(cubeScale, range, cubeScale); }
        //        else { _cubesSamples[i].transform.localScale = new Vector3(cubeScale, cubeScale, range); }

        //    }
        //    else
        //    {
        //        _cubesSamples[i].transform.localScale = intial_scate;
        //    }
        //}
    }
    void SimpleCircle()
    {
        for (int i = 512; i < _audioPeer.samples.Length; i++)
        {
            _cubesSamples[i].transform.position = _cubesSamples[i].transform.forward * radius + this.transform.position;
            var range = _audioPeer.samples[i];
            if (range * rangeTimer < 1) { range *= 100000; }
            if (isVertical) { _cubesSamples[i].transform.localScale = new Vector3(cubeScale, Mathf.Lerp(_cubesSamples[i].transform.localScale.y, range, Time.deltaTime * 10), cubeScale); }
            else { _cubesSamples[i].transform.localScale = new Vector3(cubeScale, cubeScale, Mathf.Lerp(_cubesSamples[i].transform.localScale.z, range, Time.deltaTime * 10)); }
        }
    }
    void InstantiateCircle()
    {
        _cubesSamples = new GameObject[_audioPeer.samples.Length];

        float averangeEulerAng = (float)360 / (_audioPeer.num_Samples - _audioPeer.ignoreFirst_Samples);
        Debug.Log(averangeEulerAng);
        for (int i = _audioPeer.ignoreFirst_Samples; i < _cubesSamples.Length; i++)
        {
            GameObject Ins_cube;
            if (i == _cubesSamples.Length - 1)
            {
                Ins_cube = (GameObject)Instantiate(sun, this.transform.position, Quaternion.Euler(0, averangeEulerAng * i, 0));
            }
            else { Ins_cube = (GameObject)Instantiate(cubePrefab, this.transform.position, Quaternion.Euler(0, averangeEulerAng * i, 0)); }

            Ins_cube.transform.parent = this.transform;
            // Ins_cube.transform.position = this.transform.position;
            // this.transform.eulerAngles = new Vector3(0, averangeEulerAng * i, 0);

            Ins_cube.name = "Cube" + i;
            _cubesSamples[i] = Ins_cube;
        }
    }
    void InstantiateLine()
    {
        _cubesSamples = new GameObject[_audioPeer.num_Samples];
        for (int i = 0; i < _cubesSamples.Length; i++)
        {
            GameObject Ins_cube = (GameObject)Instantiate(cubePrefab, this.transform.position + new Vector3(cubeScale * i, 0, 0), Quaternion.identity);
            Ins_cube.transform.parent = this.transform;
            // Ins_cube.transform.position = this.transform.position;
            // this.transform.eulerAngles = new Vector3(0, averangeEulerAng * i, 0);

            Ins_cube.name = "Cube" + i;
            _cubesSamples[i] = Ins_cube;
        }
    }
    IEnumerator ChangeInactive()
    {
        while (true)//only do below if robot need rush and move
        {
            if (inactiveGap == 4) { inactiveGap = 3; } else { inactiveGap = 4; }

            yield return _waitForSeconds;      //every n seconds 
        }
    }
}
