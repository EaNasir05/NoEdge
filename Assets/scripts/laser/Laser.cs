using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    public UnityEvent death;

    void Start()
    {
        death.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().OnDeathEvent);
        death.AddListener(GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().OnDeathEvent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            death.Invoke();
        }
    }
}
