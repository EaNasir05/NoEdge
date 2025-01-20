using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorBase : MonoBehaviour
{
    public UnityEvent success;

    void Start()
    {
        success.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().OnSuccessEvent);
        success.AddListener(GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().OnSuccessEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            success.Invoke();
        }
    }
}
