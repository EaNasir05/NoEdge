using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private bool plasma;
    private float speed;
    private Vector3 direction;

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
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
