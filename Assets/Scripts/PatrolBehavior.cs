using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : StateMachineBehaviour
{
    private GameObject[] patrolPoints;
    private int randomPoint;

    public float speed;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        randomPoint = GetRandomPatrolPoint();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 currentPosition = animator.transform.position;
        Vector2 nextPosition = patrolPoints[randomPoint].transform.position;

        animator.transform.position = Vector2.MoveTowards(currentPosition, nextPosition, speed * Time.deltaTime);

        if (Vector2.Distance(currentPosition, nextPosition) < .05f)
        {
            randomPoint = GetRandomPatrolPoint();
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private int GetRandomPatrolPoint()
    {
        return Random.Range(0, patrolPoints.Length);
    }
}
