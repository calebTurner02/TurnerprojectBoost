using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float fltLoadLevelDelay = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip winAudio;

    AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly": 
                Debug.Log("This is friendly");
                break;
            case "Finish": 
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("This is fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

   void StartSuccessSequence()
   {
        audioSource.PlayOneShot(winAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", fltLoadLevelDelay);
   }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
       Invoke("ReloadLevel", fltLoadLevelDelay);
    }

    void LoadNextLevel()
    {
        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int intNextSceneIndex = intCurrentSceneIndex + 1;
        if(intNextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            intNextSceneIndex = 0;
        }
        SceneManager.LoadScene(intNextSceneIndex);
    }
    void ReloadLevel()
    {
        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(intCurrentSceneIndex);
    }
}
