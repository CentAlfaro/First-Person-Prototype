using UnityEngine;

namespace Interactables
{
    public class Cone : Interactable
    {
        [SerializeField] private GameObject door;
        private bool _doorOpen;
        
        protected override void Interact()
        {
            _doorOpen = !_doorOpen;
            door.GetComponent<Animator>().SetBool("IsOpen", _doorOpen);
        }
    }
}
