using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private bool plasma;
    private float speed;
    private Vector3 direction;
    [SerializeField] private AudioClip bounceAudioClip;
    [SerializeField] private AudioClip glassDestroyedAudioClip;
    [SerializeField] private AudioClip laserDestroyedAudioClip;
    public UnityEvent death;

    void Start()
    {
        death.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().OnDeathEvent);
        death.AddListener(GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().OnDeathEvent);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetPlasma(bool plasma)
    {
        this.plasma = plasma;
    }

    public void Travel(Vector3 direction)
    {
        this.direction = direction;
        rb.velocity = direction * this.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.CompareTag("Bouncy") && this.plasma)
        {
            var contact = collision.contacts[0];
            Vector3 newVelocity = Vector3.Reflect(direction.normalized, contact.normal);
            Travel(newVelocity.normalized);
            SoundFXManager.instance.PlaySoundFXClip(bounceAudioClip, transform, 0.7f);
        }
        else
        {
            GameObject.Destroy(gameObject);
            if (collision.transform.CompareTag("Cube"))
            {
                death.Invoke();
            }
            if (this.plasma) 
            {
                if (collision.transform.CompareTag("Laser"))
                {
                    GameObject.Destroy(collision.gameObject);
                    SoundFXManager.instance.PlaySoundFXClip(laserDestroyedAudioClip, transform, 1f);
                }
            }
            else
            {
                if (collision.transform.CompareTag("Glass"))
                {
                    GameObject.Destroy(collision.gameObject);
                    SoundFXManager.instance.PlaySoundFXClip(glassDestroyedAudioClip, transform, 1f);
                }
            }
        }
    }
}
