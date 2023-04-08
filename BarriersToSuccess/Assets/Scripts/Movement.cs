using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   
    
    [SerializeField] private Animator anim;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 clampX, clampZ;
    private readonly float interpolation = 10;
    private Vector3 m_currentDirection = Vector3.zero;
    private float m_currentV = 0;
    private float m_currentH = 0;
    public bool isHide;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraTransform.GetComponent<CameraManager>().playerTransform = transform;
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
            if(isHide)
            {
                CancelHide();
            }
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

    public void Hide()
    {
        isHide = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void CancelHide()
    {
        isHide = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}

