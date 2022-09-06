/******************************************************************************  
//  File Name:      Sound.cs    
//  Author:         Caden Sheahan
//  Creation Date:  September 6th, 2022
//      
//  Description:    TAKEN FROM BRACKEYS VIDEO "Introduction to AUDIO in Unity".
//                  This script provides the settings for the audio source 
//                  added to any object.
******************************************************************************/
using UnityEngine.Audio; // Use this when working with sound!
using UnityEngine;

// makes the class visivle in the inspector without inheriting from MonoBehaviour
[System.Serializable] 
public class Sound
{
    public string name; // name of the clip (used for calling the specific one)

    public AudioClip clip; // The audio clip to be played

    [Range(0f, 1f)] 
    public float volume;
    [Range(0.1f, 3)]
    public float pitch;

    // Adds the component to whatever game object. Hidden from inspector since
    // it's set in code only.
    [HideInInspector]
    public AudioSource source; 
}
