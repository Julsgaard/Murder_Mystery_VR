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
    
    private Coroutine weightChangeCoroutine;  // To store the coroutine reference
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        headAim = transform.Find("Rig 1/Head Aim").gameObject;
        _multiAimConstraint = headAim.GetComponent<MultiAimConstraint>();
        
        
        GameObject currentCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // Get existing WeightedTransformArray
        WeightedTransformArray weightedTransforms = _multiAimConstraint.data.sourceObjects;
        // Add the new target to the existing WeightedTransformArray
        weightedTransforms.Add(new WeightedTransform(currentCamera.transform, 1f));  // 1f is the weight
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
            // If the coroutine is already running, stop it before starting a new one
            if (weightChangeCoroutine != null)
            {
                StopCoroutine(weightChangeCoroutine);
            }

            // Start a new coroutine to gradually change the weight to 1 over 2 seconds
            weightChangeCoroutine = StartCoroutine(ChangeWeightOverTime(1.0f, 1.0f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If the coroutine is already running, stop it before starting a new one
            if (weightChangeCoroutine != null)
            {
                StopCoroutine(weightChangeCoroutine);
            }

            // Start a new coroutine to gradually change the weight to 0 over 2 seconds
            weightChangeCoroutine = StartCoroutine(ChangeWeightOverTime(0.0f, 1.0f));
        }
    }

    private IEnumerator ChangeWeightOverTime(float targetWeight, float duration)
    {
        float startTime = Time.time;
        float startWeight = _multiAimConstraint.weight;

        while (Time.time - startTime < duration)
        {
            float elapsedTime = Time.time - startTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            _multiAimConstraint.weight = Mathf.Lerp(startWeight, targetWeight, progress);
            yield return null;
        }

        // Ensure the weight is set to the target value at the end
        _multiAimConstraint.weight = targetWeight;

        // Clear the coroutine reference
        weightChangeCoroutine = null;
    }
}
