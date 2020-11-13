using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController
{
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime,float TargetVolume,float Value)
    {

        float startVolume = audioSource.volume;

        while (audioSource.volume < TargetVolume)
        {
            audioSource.volume += 0.01f * Time.deltaTime / FadeTime;

            yield return null;
        }

       
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
