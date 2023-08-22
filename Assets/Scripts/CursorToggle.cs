using System;
using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

}