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
        target = pointA;// m Start() fonksiyonu içerisinde, hedef deðiþkenini ilk olarak pointA olarak ayarladým.
    }
    void FixedUpdate()
    {
        // m Hedefe doðru hareket et
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // m Eðer hedefe ulaþtýysan, hedefi deðiþtir
        if (transform.position == target)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    void Update()
    {
        
    }
}
