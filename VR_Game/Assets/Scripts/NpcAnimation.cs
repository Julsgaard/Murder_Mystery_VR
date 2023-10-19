using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NpcAnimation : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    
    
    private bool hasStartedTalking = false; 
    
    public float rotationSpeed = 2.0f; // Adjust the rotation speed as needed
    [SerializeField] private GameObject headAim;
    private MultiAimConstraint _multiAimConstraint;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        headAim = transform.Find("Rig 1/Head Aim").gameObject;
        _multiAimConstraint = headAim.GetComponent<MultiAimConstraint>();
        
        
        GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");
        // Get existing WeightedTransformArray
        WeightedTransformArray weightedTransforms = _multiAimConstraint.data.sourceObjects;
        // Add the new target to the existing WeightedTransformArray
        weightedTransforms.Add(new WeightedTransform(currentPlayer.transform, 1f));  // 1f is the weight
        // Update the MultiAimConstraint's sourceObjects
        _multiAimConstraint.data.sourceObjects = weightedTransforms;
        
        gameObject.GetComponent<RigBuilder>().Build();

    }

    public void Update()
    {
        if (audioSource.isPlaying && !hasStartedTalking)
        {
            hasStartedTalking = true;
            animator.SetBool("isTalking", true);
        }
        else if (!audioSource.isPlaying)
        {
            hasStartedTalking = false;
            animator.SetBool("isTalking", false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _multiAimConstraint.weight = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _multiAimConstraint.weight = 0;
            
        }
    }
}
