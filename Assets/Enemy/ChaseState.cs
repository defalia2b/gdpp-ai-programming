using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IBaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start chasing");
        enemy.Animator.SetTrigger("ChaseState");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player)
        {
            enemy.NavMeshAgent.destination = enemy.Player.transform.position;

            if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) > enemy.ChaseDistance)
                enemy.SwitchState(enemy.PatrolState);
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Not chasing anymore");
    }
}