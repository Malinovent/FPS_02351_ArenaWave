using System;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemyTest : AIEnemyBase
{
    [SerializeField] private AIEnemyTestStates currentState;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private HearingBehaviour hearingBehaviour;
    [SerializeField] private PatrolBehaviour patrolBehaviour;
    [SerializeField] private Animator animator;

    void Start()
    {
        patrolBehaviour.Initialize(navMeshAgent);
        SetState(AIEnemyTestStates.PATROL);

        //patrolBehaviour.onWaypointReached += OnPatrolWaypointReached;
    }

    //Callback
    /*private void OnPatrolWaypointReached()
    {
        navMeshAgent.SetDestination(patrolBehaviour.currentPatrolWaypoint.position);
    }*/

    private void Update()
    {
        UpdateState();

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {

        float speed = navMeshAgent.velocity.sqrMagnitude;

        float normalizedSpeed = Mathf.Lerp(-1, 1f, speed);

        animator.SetFloat("forwardVelocity", normalizedSpeed);
        animator.SetFloat("horizontalVelocity", 0);        
    }


    //Update State 
    private void UpdateState()
    {
        switch (currentState)
        {
            case AIEnemyTestStates.CHASE:
                ChaseBehaviour();
                break;

            case AIEnemyTestStates.PATROL:
                PatrolBehaviour();
                break;

            case AIEnemyTestStates.ATTACK:
                AttackBehaviour();
                break;

            case AIEnemyTestStates.DIE:
                DieBehaviour();
                break;
        }
    }

    //Enter state
    private void SetState(AIEnemyTestStates newState)
    {
        if (currentState == AIEnemyTestStates.DIE)
            return;

        switch (newState)
        {               
            case AIEnemyTestStates.CHASE:
                //navMeshAgent.SetDestination();
                break;

            case AIEnemyTestStates.PATROL:
                animator.SetBool("Idle", true);
                hearingBehaviour.onHeardPlayer += OnHeardPlayer;
                patrolBehaviour.EnterState();
                break;

            case AIEnemyTestStates.ATTACK:
                break;
        }

        currentState = newState;

    }

    private void OnHeardPlayer()
    {
        SetState(AIEnemyTestStates.CHASE);
        hearingBehaviour.onHeardPlayer -= OnHeardPlayer;
    }


    #region BEHAVIOURS

    private void PatrolBehaviour()
    {
        patrolBehaviour.UpdateBehaviour();
    }

    private void AttackBehaviour()
    {

    }

    private void ChaseBehaviour()
    {

    }

    private void DieBehaviour()
    {

    }

    #endregion

    private void OnDestroy()
    {
        hearingBehaviour.onHeardPlayer -= OnHeardPlayer;
    }


    private enum AIEnemyTestStates
    {
        PATROL,
        CHASE,
        ATTACK,
        DIE
    }
}
