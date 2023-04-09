using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int password;
    DoubleSlidingDoorController doorController;
    void Start()
    {
        doorController = GetComponent<DoubleSlidingDoorController>();
    }

    public void OpenDoor(int password)
    {
        if(this.password == password)
        {
            StartCoroutine(doorController.OpenDoors());
        }
        else
        {
            SoundManager.instance.PlaySoundEffects(SoundManager.AudioCallers.incorrect);
        }
    }
    
}
