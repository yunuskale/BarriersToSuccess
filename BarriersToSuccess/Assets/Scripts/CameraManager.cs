using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private float lerpValue;
    [SerializeField] private float positionPlusY, positionPlusZ;
    private void LateUpdate()
    {
        if(GameManager.instance.IsGame)
        {
            Vector3 newPos = new Vector3(playerTransform.position.x, playerTransform.position.y + positionPlusY, playerTransform.position.z + positionPlusZ);
            transform.position = Vector3.Slerp(transform.position, newPos, lerpValue);
        }

    }
}
