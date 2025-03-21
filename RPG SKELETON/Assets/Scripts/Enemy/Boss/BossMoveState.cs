using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossGroundedState
{
    public BossMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Move State");
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited Move State");
    }
}
