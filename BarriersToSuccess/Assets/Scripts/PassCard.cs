using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PassCard : MonoBehaviour
{
    public int password;

    public void PassCardClicked()
    {
        CollisionManager.instance.door.GetComponent<Door>().OpenDoor(password);
    }
    
}
