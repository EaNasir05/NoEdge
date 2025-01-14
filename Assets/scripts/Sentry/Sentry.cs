using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Sentry : MonoBehaviour
{
    public StateMachine stateMachine;
    public GameObject player;
    public GameObject barrel;
    /*private LayerMask mask;
    private bool bouncingBullets;
    private float bounceDistance = 10f;*/
    public GameObject Player { get => player; }
    private float sightDistance = 20f;
    private float fieldOfView = 85f;
    public float fireRate;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();

    }

    void Update()
    {
        CanSeePlayer();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position, targetDirection);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit, sightDistance))
                    {
                        if (hit.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void Shoot()
    {
        Transform sentryBarrel = barrel.transform;
        GameObject bullet = GameObject.Instantiate(Resources.Load("prefabs/Bullet") as GameObject, sentryBarrel.position, transform.rotation);
    }
}