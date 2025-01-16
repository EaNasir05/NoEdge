using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Sentry : MonoBehaviour
{
    public StateMachine stateMachine;
    public GameObject player;
    public GameObject bullet;
    public Transform bulletSpawnPosition;
    public float bulletSpeed;
    public GameObject defaultDirection;
    public float fireRate;
    private float sightDistance = 20f;
    private float fieldOfView = 85f;

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
        GameObject newBullet = GameObject.Instantiate(bullet, bulletSpawnPosition.position, transform.rotation);
        newBullet.GetComponent<Bullet>().speed = bulletSpeed;
        newBullet.GetComponent<Bullet>().Travel(transform.forward);
    }
}