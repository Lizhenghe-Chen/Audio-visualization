using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{

    private AudioSource audioSource;
    //========== variables for the FFT analysis ==========
    [Header("Aduio Visualizer Settings")]
    public int num_Samples = 128;
    public int channels = 1;
    public int ignoreFirst_Samples = 8;
    public int ignoreFirst_SamplesMultiply = 10;
    public float[] averageSamples = new float[8];

    public float[] emm = new float[2048];
    public int rangeA, rangeB;
    public float TEST;
    public FFTWindowType fftWindowType;
    public enum FFTWindowType { Blackman, Triangle, Hamming, Hanning, BlackmanHarris }

    //============================================================================
    [Header("\n")]
    public float[] samples;


    public float BaseValue;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        samples = new float[num_Samples];
    }

    // Update is called once per frame
    void Update()
    {
        samples = GetSpectrumAudioSource(samples);
        GetAverageSpectrum();

        // Buffer();
    }
    private void FixedUpdate()
    {
        TEST = EMM();
    }

    float[] GetSpectrumAudioSource(float[] samples)    //get spectrum audio source
    {
        //get spectrum data
        switch (fftWindowType)
        {
            case FFTWindowType.Blackman:
                AudioListener.GetSpectrumData(samples, channels, FFTWindow.Blackman);
                break;
            case FFTWindowType.Triangle:
                AudioListener.GetSpectrumData(samples, channels, FFTWindow.Triangle);
                break;
            case FFTWindowType.Hamming:
                AudioListener.GetSpectrumData(samples, channels, FFTWindow.Hamming);
                break;
            case FFTWindowType.Hanning:
                AudioListener.GetSpectrumData(samples, channels, FFTWindow.Hanning);
                break;
            case FFTWindowType.BlackmanHarris:
                AudioListener.GetSpectrumData(samples, channels, FFTWindow.BlackmanHarris);
                break;

        }
        return samples;

    }
    void GetAverageSpectrum()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0f;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[j];
                count++;
            }
            average /= count;
            averageSamples[i] = average * 10;
        }
    }
    float EMM()//get rhythm
    {
        emm = GetSpectrumAudioSource(emm);
        float average = 0;
        int count = 0;
        for (int j = rangeA; j < rangeB; j++)
        {
            average += emm[j];
            count++;
        }
        return average /= count;
    }

    // void Buffer()
    // {
    //     for (int i = 0; i < 8; ++i)
    //     {
    //         if (averageSamples[i] + buferTrashold < averageSamplesBuffer[i])
    //         {
    //             averageSamplesBuffer[i] -= buferTrashold;
    //         }
    //         else if (averageSamples[i] < averageSamplesBuffer[i])
    //         {
    //             averageSamplesBuffer[i] -= 0.005f;
    //         }
    //         if (averageSamples[i] > averageSamplesBuffer[i] + buferTrashold)
    //         {
    //             averageSamplesBuffer[i] += buferTrashold;
    //         }
    //         else if (averageSamples[i] > averageSamplesBuffer[i])
    //         {
    //             averageSamplesBuffer[i] = averageSamples[i];
    //         }


    //     }

    // }
    void GetBaseSpectrum()
    {
        int count = 0;
        float average = 0f;

        for (int j = 256; j < 512; j++)
        {
            average += samples[j];
            count++;
        }
        BaseValue = average / count;
    }
}