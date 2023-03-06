using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BScreenCallback : MonoBehaviour
{
    
    public void OnOpened()
    {
        GameManager.instance.uiManager.OnReload();
    }
}
