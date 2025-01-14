using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector3 lastVelocity;
    public float speed;
    private Vector3 direction;

    void Update()
    {
        lastVelocity = rb.velocity;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Cube"))
        {
            speed = lastVelocity.magnitude;
            direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
