using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AiFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    public float normalSpeed;
    public float maxSpeed;
    public float playerSprintSpeedThreshold;
    private Vector3 lastPlayerPosition;

    public float maxTime = 0.1f;
    public float maxDistance = 1.0f;

    private NavMeshAgent agent;
    private Animator animator;
    float timer = 0.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        lastPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0.0f)
        {
            float sqrdistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if(sqrdistance > maxDistance*maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            timer = maxTime;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);

        //sätt en normal speed eller en max speed beroende på om spelaren springer eller inte
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
        //sätt en normal speed eller en max speed beroende på om spelaren springer eller inte

    }
}
