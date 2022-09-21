using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{



    [SerializeField] float fltMainThrust = 100f;
    [SerializeField] float fltRotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftEngineParticle;
    [SerializeField] ParticleSystem rightEngineParticle;
    Rigidbody rb;

    AudioSource audioSource;

    
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * fltMainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticle.isPlaying)
            {
               mainEngineParticle.Play(); 
            }
            
           
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }
    void ProcessRotation()
    {
         if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(fltRotationThrust);

            if (!rightEngineParticle.isPlaying)
            {
               rightEngineParticle.Play(); 
            }
        }

         else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-fltRotationThrust);
            if (!leftEngineParticle.isPlaying)
            {
               leftEngineParticle.Play(); 
            }
        }
        else
        {
            rightEngineParticle.Stop();
            leftEngineParticle.Stop();
        }
    }
    void ApplyRotation(float fltRotationThisFrame)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * fltRotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze rotation so physics take over
    }
}
