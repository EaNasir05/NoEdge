using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float speed;

    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
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

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bouncy"))
        {
            Debug.Log("BOUNCE");
            Plane plane = other.gameObject.GetComponent<Plane>();
            Vector3.Reflect(gameObject.transform.forward, plane.normal);
            
        }
    }*/
}
