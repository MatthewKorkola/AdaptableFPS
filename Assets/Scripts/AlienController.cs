using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public int damageAmount = 1;

    private bool wasHit;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }

            if (agent.remainingDistance < .25f)
            {
                //anim.SetBool("isMoving", false);
            }
            else
            {
                //anim.SetBool("isMoving", true);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;

                Vector3 direction = (targetPoint - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)) * Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
            else
            {
                agent.destination = transform.position;
            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                if (!wasHit)
                {
                    chasing = false;
                    chaseCounter = keepChasingTime;
                }
            }
            else
            {
                wasHit = false;
            }

        }
    }

    public void GetHit()
    {
        wasHit = true;
        chasing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
        }
    }
}

