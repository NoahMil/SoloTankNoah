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
    private float securityDistance;
    private bool rallyNearestWaypoint;
    private float fireCooldown;
    private bool isAlreadyFiring;
    [HideInInspector] public bool injured = false;
    public float lifePoint = 3;

    
    
    private Transform nearestTarget;
    private bool targetInRange;
    private BaseState currentState;
    private Transform nearestWaypoint = null;
    private float minDistance = Mathf.Infinity;
    private float timeSinceLastShot;
    
    public static int NearestWaypointIndex { get; set; }
    
    private void Start() {
        currentState = new WanderingState(this);
    }

    private void Update() {
        Debug.Log(currentState);
        // State system
        BaseState nextState = currentState.GetNextState();
        if (nextState != null)
        {
            currentState = nextState;
        }
        else currentState.Execute();
        
        // Handling cooldown
        timeSinceLastShot += Time.deltaTime;
        
        // Strange stuff
        for (int i = 0; i < Waypoints.List.Count; i++)
        {
            Transform waypoint = Waypoints.List[i];
            float dist = Vector3.Distance(transform.position, waypoint.position);

            if (dist < minDistance)
            {
                minDistance = dist;
                nearestWaypoint = waypoint;
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
    
    public void RotateToTarget(Vector3 targetPos)
    { 
        temp.position = new Vector3(targetPos.x, turretHead.position.y, targetPos.z);
        turretHead.transform.LookAt(temp);
    }
    
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