using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float fltLoadLevelDelay = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip winAudio;

      [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem winParticle;

    AudioSource audioSource;

    bool bolTransitioning = false;
    bool bolCollisionDisable = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
       RespondToDebugKeys(); 
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            //Toggle collision
            bolCollisionDisable = !bolCollisionDisable; 
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (bolTransitioning || bolCollisionDisable)
        {
            return;
        }
        
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
        bolTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(winAudio);
        winParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", fltLoadLevelDelay);
   }

    void StartCrashSequence()
    {
        audioSource.Stop();
        bolTransitioning = true;
        audioSource.PlayOneShot(crashAudio);
        crashParticle.Play();
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
