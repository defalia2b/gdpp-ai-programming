using UnityEngine;

public class RetreatState : IBaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start retreating");
        enemy.Animator.SetTrigger("RetreatState");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
            enemy.NavMeshAgent.destination = enemy.transform.position - enemy.Player.transform.position;
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Not retreating anymore");
    }
}