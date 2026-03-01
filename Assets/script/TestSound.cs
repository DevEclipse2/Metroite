//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;
using UnityEngine.InputSystem;
public class Example : MonoBehaviour
{
    public AudioSource m_MyAudioSource;
    public GameObject child;
    
    

    void Start()
    {
        
    }
    public void OnJump()
    {
        m_MyAudioSource.Play();
    }
    void Update()
    {
        
    }

}