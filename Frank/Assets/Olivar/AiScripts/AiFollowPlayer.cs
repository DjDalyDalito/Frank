using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    // Add variables for normal and maximum speeds
    public float normalSpeed;
    public float maxSpeed;

    // Threshold to determine if the player is sprinting
    public float playerSprintSpeedThreshold;

    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        lastPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        agent.destination = playerTransform.position;

        Vector3 playerMovement = playerTransform.position - lastPlayerPosition;
        float playerSpeed = playerMovement.magnitude / Time.deltaTime;
        lastPlayerPosition = playerTransform.position;

        bool playerIsSprinting = playerSpeed > playerSprintSpeedThreshold;

        if (playerIsSprinting)
        {
            agent.speed = maxSpeed;
        }
        else
        {
            agent.speed = normalSpeed;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
