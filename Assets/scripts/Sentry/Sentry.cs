using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Sentry : MonoBehaviour
{
    public StateMachine stateMachine;
    public GameObject player;
    public Transform bulletSpawnPosition;
    [SerializeField] private ParticleSystem shootingSystem;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float speed = 20;
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool bouncingBullets;
    [SerializeField] private float bounceDistance = 200f;
    public GameObject defaultDirection;
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
        shootingSystem.Play();
        Vector3 direction = transform.forward;
        TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPosition.position, Quaternion.identity);
        if (Physics.Raycast(bulletSpawnPosition.position, direction, out RaycastHit hit, float.MaxValue, mask))
        {
            if (hit.transform.CompareTag("Bouncy"))
            {
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, bounceDistance, true, true));
            }
            else
            {
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, bounceDistance, true, false));
            }
        }
        else
        {
            StartCoroutine(SpawnTrail(trail, bulletSpawnPosition.position + direction * 100, Vector3.zero, bounceDistance, false, true));
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, float bounceDistance, bool madeImpact, bool bouncySurface)
    {
        Vector3 startPosition = trail.transform.position;
        Vector3 direction = (hitPoint - trail.transform.position).normalized;
        float distance = Vector3.Distance(trail.transform.position, hitPoint);
        float startingDistance = distance;
        while (distance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * speed;
            yield return null;
        }
        trail.transform.position = hitPoint;
        if (madeImpact)
        {
            
            if (bouncySurface && bouncingBullets && bounceDistance > 0)
            {
                Vector3 bounceDirection = Vector3.Reflect(direction, hitNormal);
                if (Physics.Raycast(hitPoint, bounceDirection, out RaycastHit hit, bounceDistance, mask))
                {
                    if (hit.transform.CompareTag("Bouncy"))
                    {
                        yield return StartCoroutine(SpawnTrail(
                        trail,
                        hit.point,
                        hit.normal,
                        bounceDistance - Vector3.Distance(hit.point, hitPoint),
                        true,
                        true
                        ));
                    }
                    else
                    {
                        yield return StartCoroutine(SpawnTrail(
                        trail,
                        hit.point,
                        hit.normal,
                        bounceDistance - Vector3.Distance(hit.point, hitPoint),
                        true,
                        false
                    ));
                    }
                }
                else
                {
                    yield return StartCoroutine(SpawnTrail(
                        trail,
                        hitPoint + bounceDirection * bounceDistance,
                        Vector3.zero,
                        0,
                        false,
                        true
                    ));
                }
            }
        }
        Destroy(trail.gameObject, trail.time);
    }
}