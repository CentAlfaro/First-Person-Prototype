using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInteract : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LayerMask mask;
        private Camera cam;
        private PlayerUI _playerUI;
        private InputManager _inputManager;
        
        [Header("Interacting Stats")]
        [SerializeField ]private float distance = 3f;
        private void Start()
        {
            cam = GetComponent<PlayerLook>().cam;
            _playerUI = GetComponent<PlayerUI>();
            _inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            _playerUI.UpdateText(string.Empty);
            // creates the ray at the center of the camera that shoots outward
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance);
            //this variable will store the information of what the raycast hit
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, distance, mask))
            {
                if (hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                    _playerUI.UpdateText($"[{interactable.promptMessage}]");
                    if (_inputManager.OnFoot.Interact.triggered)
                    {
                        interactable.BaseInteract();
                    }
                }
            }
        }
    }
}