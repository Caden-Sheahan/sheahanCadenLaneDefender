/******************************************************************************  
//  File Name:      AudioController.cs     
//  Author:         Caden Sheahan
//  Creation Date:  September 6th, 2022
//      
//  Description:    TAKEN FROM BRACKEYS VIDEO "Introduction to AUDIO in Unity".
//                  This script creates the audio clip that can be edited using
//                  the settings in the "Sound" script. The "Play" method is 
//                  called in other classes to play the sound at those points.
******************************************************************************/
using UnityEngine.Audio; // Use when working with sound!
using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public Sound[] sounds; // Array for all sounds to be added on objects

    void Awake()
    {
        foreach(Sound s in sounds) // actually creates the settings for each sound
        {
            // makes the "source" variable in the Sound class add an AudioSource component to each object
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip; // settings for the source component. Clip
            s.source.volume = s.volume; // volume 
            s.source.pitch = s.pitch; // pitch (MUST BE AT LEAST 1) 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name) // takes the name of the clip set in the inspector
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // checks name
        s.source.Play(); // plays the audio
    }
}
