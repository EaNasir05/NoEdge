using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float speed;

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bouncy"))
        {
            Debug.Log("BOUNCE");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
