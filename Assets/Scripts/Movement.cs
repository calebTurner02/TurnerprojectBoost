using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float fltMainThrust = 100f;
    [SerializeField] float fltRotationThrust = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
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
        transform.Rotate(Vector3.forward * fltRotationThisFrame * Time.deltaTime);
    }
}
