using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimation : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    
    private bool hasStartedTalking = false;
    
    public float rotationSpeed = 2.0f; // Adjust the rotation speed as needed
    [SerializeField]
    private Transform _head, _spine;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        _head = transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head");
        _spine = transform.Find("Root/Hips/Spine_01/Spine_02");
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


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnTowardsPlayer(other.gameObject);
            
        }
    }

    public void TurnTowardsPlayer(GameObject playerObject)
    {
        // Calculate the direction from the NPC to the player
        Vector3 directionToPlayer = playerObject.transform.position - transform.position;

        // Calculate the rotation needed to align with the player's direction
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Apply the rotation to the head and spine using Slerp
        _head.transform.rotation = Quaternion.Slerp(_head.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        //_spine.transform.rotation = Quaternion.Slerp(_spine.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
