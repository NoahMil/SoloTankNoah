using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform temp;
    public List<GameObject> playersInAttackRange = new List<GameObject>();
    public List<GameObject> playersInSightRange = new List<GameObject>();

    [Header("Parameters")]
    [SerializeField] private bool rallyNearestWaypoint;
    [SerializeField] private float fireCooldown;
    
    private Transform _nearestTarget;
    private bool _targetInRange;
    private BaseState _currentState;
    private Transform _nearestWaypoint = null;
    private float _minDistance = Mathf.Infinity;
    private float _timeSinceLastShot;
    
    public static int NearestWaypointIndex { get; set; }
    
    private void Start() {
        _currentState = new WanderingState(this);
    }

    private void Update() {
        Debug.Log(_currentState);
        // State system
        BaseState nextState = _currentState.GetNextState();
        if (nextState != null)
        {
            _currentState = nextState;
        }
        else _currentState.Execute();
        
        // Handling cooldown
        _timeSinceLastShot += Time.deltaTime;
        
        // Strange stuff
        for (int i = 0; i < Waypoints.List.Count; i++)
        {
            Transform waypoint = Waypoints.List[i];
            float dist = Vector3.Distance(transform.position, waypoint.position);

            if (dist < _minDistance)
            {
                _minDistance = dist;
                _nearestWaypoint = waypoint;
                NearestWaypointIndex = i;
            }
        }
        
    }

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
    
    public void Fire() {
        if (_timeSinceLastShot < fireCooldown) return;
        Invoke(nameof(FireDelay), 2);
        _timeSinceLastShot = 0;
    }

    private void FireDelay() {
        Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);   
    }
    
    public void RotateToTarget(Vector3 targetPos)
    { 
        temp.position = new Vector3(targetPos.x, turretHead.position.y, targetPos.z);
        turretHead.transform.LookAt(temp);
    }
    
}