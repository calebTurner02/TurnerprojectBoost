using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float fltMainThrust = 100f;
    [SerializeField] float fltRotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
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
           
        }
        else
        {
            audioSource.Stop();
        }
    }
    void ProcessRotation()
    {
         if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(fltRotationThrust);
        }

         else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-fltRotationThrust);
        }
    }
    void ApplyRotation(float fltRotationThisFrame)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * fltRotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze rotation so physics take over
    }
}
