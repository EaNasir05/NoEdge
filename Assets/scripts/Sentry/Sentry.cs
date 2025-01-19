using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Sentry : MonoBehaviour
{
    private StateMachine stateMachine;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject defaultDirection;
    [SerializeField] private float fireRate;
    [SerializeField] private bool plasma;
    [SerializeField] private AudioClip chargingAudioClip;
    [SerializeField] private AudioClip plasmaShotAudioClip;
    [SerializeField] private AudioClip bulletShotAudioClip;
    [SerializeField] private AudioClip resetAudioClip;
    [SerializeField] private AudioClip targetAcquiredAudioClip;
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

    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject GetDefaultDirection()
    {
        return defaultDirection;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public AudioClip GetResetAudioClip()
    {
        return resetAudioClip;
    }

    public AudioClip GetChargingAudioClip()
    {
        return chargingAudioClip;
    }

    public AudioClip GetTargetAcquiredAudioClip()
    {
        return targetAcquiredAudioClip;
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
        newBullet.GetComponent<Bullet>().SetSpeed(bulletSpeed);
        newBullet.GetComponent<Bullet>().SetPlasma(plasma);
        newBullet.GetComponent<Bullet>().Travel(transform.forward);
        if (plasma)
        {
            SoundFXManager.instance.PlaySoundFXClip(plasmaShotAudioClip, transform, 0.4f);
        }
        else
        {
            SoundFXManager.instance.PlaySoundFXClip(bulletShotAudioClip, transform, 0.7f);
        }
    }
}