using UnityEngine;

public interface IBaseState
{
    public void EnterState(Enemy enemy);
    public void UpdateState(Enemy enemy);
    public void ExitState(Enemy enemy);
}
