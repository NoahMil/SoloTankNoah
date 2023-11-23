using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class MonsterAI : MonoBehaviour {
    
        [Header("References")]
        public List<GameObject> playersInAttackRange = new List<GameObject>();
        public List<GameObject> playersInSightRange = new List<GameObject>();
        public List<GameObject> playersInFleeRange = new List<GameObject>();
        [SerializeField] private PlayerDatas playerData;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPosition;
        [SerializeField] private Transform turretHead;
        [SerializeField] private Transform temp;

    
        [Header("Parameters")]
        [HideInInspector] public bool injured = false;
        public float lifePoint = 3;
        private float securityDistance;
        private float timeSinceLastShot = 2f;
        private bool rallyNearestWaypoint;
        private float fireCooldown;
        private bool isAlreadyFiring;
        private BaseState _currentState;
        private float minDistance = Mathf.Infinity;
        
        
        [Header("Unfinished")]
        private Transform _nearestTarget;
        private Transform nearestWaypoint = null;
        private int _nearestWaypointIndex;
    
        private void Start() {
            _currentState = new WanderingState(this);
        }

        private void Update() {
            // State system
            BaseState nextState = _currentState.GetNextState();
            if (nextState != null)
            {
                _currentState = nextState;
            }
            else _currentState.Execute();
        
            // Handling cooldown
            timeSinceLastShot += Time.deltaTime;
        
            // Waypoint system
            for (int i = 0; i < Waypoints.List.Count; i++)
            {
                Transform waypoint = Waypoints.List[i];
                float dist = Vector3.Distance(transform.position, waypoint.position);

                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearestWaypoint = waypoint;
                    _nearestWaypointIndex = i;
                }
            }
        
        }

        // Utilisation de différents colliders afin de détecter la présence de GO tag "Player"
        public void EnterSightCollider(Collider other) {
            if (!other.CompareTag("Player")) return;
            playersInSightRange.Add(other.gameObject);
        }
    
        public void ExitSightCollider(Collider other) {
            if (!other.CompareTag("Player") || !playersInSightRange.Contains(other.gameObject)) return;
            playersInSightRange.Remove(other.gameObject);
        }
    
        public void EnterAttackCollider(Collider other) {
            if (!other.CompareTag("Player")) return;
            playersInAttackRange.Add(other.gameObject);
        }
    
        public void ExitAttackCollider(Collider other) {
            if (!other.CompareTag("Player") || !playersInAttackRange.Contains(other.gameObject)) return;
            playersInAttackRange.Remove(other.gameObject);
        }
    
        public void EnterFleeCollider(Collider other) {
            if (!other.CompareTag("Player")) return;
            playersInFleeRange.Add(other.gameObject);
        }
    
        public void ExitFleeCollider(Collider other) {
            if (!other.CompareTag("Player") || !playersInFleeRange.Contains(other.gameObject)) return;
            playersInFleeRange.Remove(other.gameObject);
        }
    
        public void RotateToTarget(Vector3 targetPos)
        { 
            temp.position = new Vector3(targetPos.x, turretHead.position.y, targetPos.z);
            turretHead.transform.LookAt(temp);
        }
    
        // Fonctions utilisables dans plusieurs states
        public void Fire()
        {
            if (!isAlreadyFiring)
            {
                isAlreadyFiring = true;
                Invoke(nameof(FireDelay), 2f);
            }
        }
        private void FireDelay()
        {
            Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);   
            isAlreadyFiring = false;
        }
    
        public void ApplyDamage(int damage)
        {
            lifePoint -=  damage;
            if (lifePoint <= 1)
            {
                injured = true;
            }
            if (lifePoint <= 0)
            {
                Destruction();
            }
        }
    
        private void Destruction()
        {
            Destroy(gameObject);
        }
    
    }
}