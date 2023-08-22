using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerLook : MonoBehaviour
    {
        public Camera cam;
        private float _xRotation = 0f;

        public float _xSensitivity = 30f;
        public float _ySensitivity = 30f;

        public void ProcessLook(Vector2 input)
        {
            
            float mouseX = input.x;
            float mouseY = input.y;
            //Calculate camera rotation for up and down
            _xRotation -= (mouseY * Time.deltaTime) * _ySensitivity;
            _xRotation = Mathf.Clamp(_xRotation, -80, 80f);
            cam.transform.localRotation = Quaternion.Euler(_xRotation, 0,0);
        
            //Rotate camera for left and right
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) *_xSensitivity);
        }
    }
}
