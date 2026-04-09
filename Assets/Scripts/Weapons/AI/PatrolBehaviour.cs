using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float distanceThreshold = 1f;

    private int currentPatrolIndex = 0;
    private Transform currentPatrolWaypoint;
    private NavMeshAgent navMeshAgent; 

    //Callback
    //public event Action onWaypointReached;

    public void Initialize(NavMeshAgent agent)
    {
        navMeshAgent = agent;
    }

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentPatrolWaypoint = patrolPoints[currentPatrolIndex];
        }
    }

    public void EnterState()
    {
        navMeshAgent?.SetDestination(currentPatrolWaypoint.position);
    }

    public void UpdateBehaviour()
    {
        float distance = Vector3.Distance(transform.position, currentPatrolWaypoint.position);

        if(distance <= distanceThreshold)
        {
            NextWaypoint();
            navMeshAgent.SetDestination(currentPatrolWaypoint.position);
        }
    }

    private void NextWaypoint()
    {
        currentPatrolIndex++;
        if (currentPatrolIndex >= patrolPoints.Length)
        {
            currentPatrolIndex = 0;
        }

        currentPatrolWaypoint = patrolPoints[currentPatrolIndex];
    }

    private void OnDrawGizmos()
    {

        for(int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(patrolPoints[i].position, 0.3f);

            if(i < patrolPoints.Length - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
            }
        }
        Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1].position, patrolPoints[0].position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(patrolPoints[0].position, 0.5f);
        
    }
}