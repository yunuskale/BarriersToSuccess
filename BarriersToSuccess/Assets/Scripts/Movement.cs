using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 pointA;//m Ýlk olarak, karakterin hareket edeceði iki noktanýn konumunu Vector3 türünden deðiþkenlerle tanýmladým.
    public Vector3 pointB;//m
    private Vector3 target;// m Daha sonra, karakterin hareket etmesini istediðimiz yönü belirleyen bir hedef deðiþkeni tanýmladým.
    [SerializeField] private int speed;//m 
    [SerializeField] private Animator anim;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 clampX, clampZ;
    private readonly float interpolation = 10;
    private Vector3 m_currentDirection = Vector3.zero;
    private float m_currentV = 0;
    private float m_currentH = 0;
    [SerializeField] private Vector3 rot;

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

        float v = joystick.Vertical;
        float h = joystick.Horizontal;

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * interpolation);

        Vector3 direction = cameraTransform.forward * m_currentV + cameraTransform.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (v != 0 && h != 0)
        {
            anim.SetBool("run", true);
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * interpolation);
            transform.SetPositionAndRotation(transform.position + m_currentDirection * moveSpeed * Time.deltaTime, Quaternion.LookRotation(m_currentDirection));


        }
        else
        {
            anim.SetBool("run", false);
        }

        //Vector3 clampingPos = new Vector3(Mathf.Clamp(transform.position.x, clampX.x, clampX.y), transform.position.y, Mathf.Clamp(transform.position.z, clampZ.x, clampZ.y));
        //transform.position = clampingPos;
    }

}

