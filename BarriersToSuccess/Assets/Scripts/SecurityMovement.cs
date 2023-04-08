using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityMovement : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 1f;

    private void Start()
    {
        StartCoroutine(Walk());
    }
    IEnumerator Walk()
    {
        while (true)
        {
            yield return null;
            Vector3 yon = pointA - transform.position;
            transform.LookAt(pointA);
            if (yon.magnitude > 0.3f)
            {
                yon.Normalize();
                transform.position += yon * speed * Time.deltaTime;
            }
            else
            {
                break;
            }
        }
        while (true)
        {
            yield return null;
            Vector3 yon = pointB - transform.position;
            transform.LookAt(pointB);
            if (yon.magnitude > 0.3f)
            {
                yon.Normalize();
                transform.position += yon * speed * Time.deltaTime;
            }
            else
            {
                break;
            }
        }
        StartCoroutine(Walk());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player") && !other.GetComponent<Movement>().isHide)
        {
            GameManager.instance.Lose();
        }
    }
}
