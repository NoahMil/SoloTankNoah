using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private PlayerDatas playerData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform temp;
    public List<GameObject> playersInAttackRange = new List<GameObject>();
    public List<GameObject> playersInSightRange = new List<GameObject>();
    public List<GameObject> playersInFleeRange = new List<GameObject>();

    

    [Header("Parameters")]
    private int _lifePoint = 3;
    private bool _rallyNearestWaypoint;
    private float _fireCooldown;
    private bool _isAlreadyFiring;
    public bool injured = false;
    
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
    
    public void EnterFleeCollider(Collider other) {
        if (!other.CompareTag("Player")) return;
        playersInFleeRange.Add(other.gameObject);
    }
    
    public void ExitFleeCollider(Collider other) {
        if (!other.CompareTag("Player") || !playersInFleeRange.Contains(other.gameObject)) return;
        playersInFleeRange.Remove(other.gameObject);
    }
    
    public void Fire()
    {
        if (!_isAlreadyFiring)
        {
            _isAlreadyFiring = true;
            Invoke(nameof(FireDelay), 2f);
        }
    }
    private void FireDelay()
    {
        Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);   
        _isAlreadyFiring = false;
    }
    
    public void RotateToTarget(Vector3 targetPos)
    { 
        temp.position = new Vector3(targetPos.x, turretHead.position.y, targetPos.z);
        turretHead.transform.LookAt(temp);
    }
    
    public void ApplyDamage(int damage)
    {
        _lifePoint -=  damage;
        if (_lifePoint <= 1)
        {
            injured = true;
        }
        if (_lifePoint <= 0)
        {
            Destruction();
        }
    }
    
    private void Destruction()
    {
        Destroy(gameObject);
    }
}