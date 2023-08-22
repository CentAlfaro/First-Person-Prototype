using System;
using UnityEngine;
using UnityEngine.AI;
namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("State and Path")]
        [SerializeField] private string currentState;
        public Path path;
       
        [Header("Enemy Vision")]
        public float sightDistance = 20f;
        public float fieldOfView = 85f;
        public float eyeHeight;

        [Header("Weapon Values")] 
        public Transform gunBarrel;
        public GameObject bullet;
        [Range(0.1f, 10)] public float fireRate;

        private StateMachine _stateMachine;
        private GameObject _player;
        private NavMeshAgent _agent;

        public GameObject Player
        {
            get => _player;
        }

        public NavMeshAgent Agent
        {
            get => _agent;
        }

        private void Start()
        {
            _stateMachine = GetComponent<StateMachine>();
            _agent = GetComponent<NavMeshAgent>();
            _stateMachine.Initialize();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            CanSeePlayer();
            currentState = _stateMachine.ActiveState.ToString();
        }

        public bool CanSeePlayer()
        {
            if (_player != null)
            {
                // checks if the player is within sight
                if (Vector3.Distance(transform.position, _player.transform.position) < sightDistance)
                {
                    Vector3 targetDirection = _player.transform.position - transform.position - (Vector3.up * eyeHeight);
                    float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

                    // checks if the player is within the field of view
                    if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                    {
                        Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                        RaycastHit hitInfo = new RaycastHit();
                        if (Physics.Raycast(ray, out hitInfo, sightDistance))
                        {
                            if (hitInfo.transform.gameObject == _player)
                            {
                                Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                                return true;
                            }
                        }
                        
                    }
                }
            }

            return false;
        }
    }
}