using UnityEngine;

namespace Enemy.States
{
    public class AttackState : BaseState
    {
        private float _moveTimer;
        private float _losePlayerTimer;
        private float _shotTimer;
        public override void Enter()
        {
            
        }

        public override void Perform()
        {
            if (Enemy.CanSeePlayer())
            {
                _losePlayerTimer = 0;
                _moveTimer += Time.deltaTime;
                _shotTimer += Time.deltaTime;
                Enemy.transform.LookAt(Enemy.Player.transform);

                if (_shotTimer > Enemy.fireRate)
                {
                    Shoot();
                }
                
                //moves the enemy to a random position within a random time
                if (_moveTimer > Random.Range(3, 7))
                {
                    Enemy.Agent.SetDestination(Enemy.transform.position + (Random.insideUnitSphere * 4));
                    _moveTimer = 0;
                }
            }
            else
            {
                _losePlayerTimer += Time.deltaTime;
                if (_losePlayerTimer > 4)
                {
                    StateMachine.ChangeState(new PatrolState());
                }
            }
        }

        public override void Exit()
        {
            
        }

        public void Shoot()
        {
            Debug.Log("Shoot");
            Transform gunBarrel = Enemy.gunBarrel;
            
            //instantiate bullet
            GameObject bullet = GameObject.Instantiate(Enemy.bullet, gunBarrel.position, Enemy.transform.rotation);
            //calculate the direction to the player
            Vector3 shootDirection = (Enemy.Player.transform.position - gunBarrel.transform.position).normalized;
            //adds force to the bullet
            bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-1f, 5f), Vector3.up) * shootDirection * 40;
            _shotTimer = 0;
        }
    }
}