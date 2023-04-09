using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Movement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed;
    private float interpolation = 10;
    private Vector3 currentDirection = Vector3.zero;
    private float currentV = 0;
    private float currentH = 0;
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

        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        Vector3 direction = cameraTransform.forward * currentV + cameraTransform.right * currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (v != 0 && h != 0)
        {
            if (isHide)
            {
                CancelHide();
            }
            anim.SetBool("run", true);
            currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);
            transform.SetPositionAndRotation(transform.position + currentDirection * moveSpeed * Time.deltaTime, Quaternion.LookRotation(currentDirection));


        }
        else
        {
            anim.SetBool("run", false);
        }

    }

    public void Hide()
    {
        if(!isHide)
        {
            SoundManager.instance.PlaySoundEffects(SoundManager.AudioCallers.box);
            transform.GetChild(2).GetComponent<ParticleSystem>().Play();
            isHide = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).transform.position += Vector3.up;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).transform.DOLocalMoveY(0.42f, 0.5f);
        }
    }
    public void CancelHide()
    {
        isHide = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}

