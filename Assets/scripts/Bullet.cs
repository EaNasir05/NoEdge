using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float speed;
    private Vector3 direction;

    public void Travel(Vector3 direction)
    {
        this.direction = direction;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bouncy"))
        {
            Debug.Log("BOUNCE");
            var contact = collision.contacts[0];
            Vector3 newVelocity = Vector3.Reflect(direction.normalized, contact.normal);
            Travel(newVelocity.normalized);
        }
        else
        {
            var contact = collision.contacts[0];
            Vector3 newVelocity = Vector3.Reflect(direction.normalized, contact.normal);
            Travel(newVelocity.normalized);
            //Destroy(gameObject);
        }
    }
}
