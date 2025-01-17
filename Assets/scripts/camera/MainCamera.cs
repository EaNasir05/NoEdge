using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform defaultPosition;
    [SerializeField] private Transform controlPoint;
    [SerializeField] private Transform altControlPoint;
    [SerializeField] private Transform altPosition;
    [SerializeField] private Transform firstPersonPosition;

    void Update()
    {
        Ray ray = new Ray(controlPoint.position, defaultPosition.position - controlPoint.position);
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(controlPoint.position, defaultPosition.position - controlPoint.position);
        float distance = Vector3.Distance(defaultPosition.position, controlPoint.position);
        if (Physics.Raycast(ray, out hit, distance) && !hit.transform.CompareTag("Bullet"))
        {
            ray = new Ray(controlPoint.position, altPosition.position - controlPoint.position);
            Debug.DrawRay(controlPoint.position, altPosition.position - controlPoint.position);
            distance = Vector3.Distance(altPosition.position, controlPoint.position);
            if (Physics.Raycast(ray, out hit, distance) && !hit.transform.CompareTag("Bullet"))
            {
                transform.position = firstPersonPosition.position;
                gameObject.GetComponent<Camera>().fieldOfView = 55;
            }
            else
            {
                transform.position = altPosition.position;
                gameObject.GetComponent<Camera>().fieldOfView = 45;
            }
        }
        else
        {
            transform.position = defaultPosition.position;
            gameObject.GetComponent<Camera>().fieldOfView = 40;
        }
    }
}
