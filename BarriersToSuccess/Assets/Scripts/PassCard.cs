using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PassCard : MonoBehaviour
{
    public int password;

    public void PassCardClicked()
    {
        if(CollisionManager.instance.door != null)
            CollisionManager.instance.door.GetComponent<Door>().OpenDoor(password);
    }
    
}
