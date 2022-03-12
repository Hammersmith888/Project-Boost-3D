using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust(){
       if (Input.GetKey(KeyCode.Space)){
           StartThrust();
       }
       else{
           StopThrusting();
       }
           
           
           
       

    }
    void StartThrust(){
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying){
            mainEngineParticles.Play();
        }
               
           
        
               
            
    }
    private void StopThrusting(){
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
    void ProcessRotation(){
        
        if (Input.GetKey(KeyCode.A)){
            ApplyRotation(rotationThrust);
            if (!rightThrusterParticles.isPlaying){
               rightThrusterParticles.Play();
           }
        }
        else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotationThrust);
            if (!leftThrusterParticles.isPlaying){
               leftThrusterParticles.Play();
           }
        }
        else{
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }
    void ApplyRotation(float rotationThisFrame){
        rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
