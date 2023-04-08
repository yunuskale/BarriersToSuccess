using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityMovement : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    private Vector3 target;
    [SerializeField] private int speed;
    void Start()
    {
        target = pointA;// m Start() fonksiyonu i�erisinde, hedef de�i�kenini ilk olarak pointA olarak ayarlad�m.
    }
    void FixedUpdate()
    {
        // m Hedefe do�ru hareket et
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // m E�er hedefe ula�t�ysan, hedefi de�i�tir
        if (transform.position == target)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    void Update()
    {
        
    }
}
