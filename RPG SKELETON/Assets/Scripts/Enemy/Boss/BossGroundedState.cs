using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundedState : EnemyState
{
    protected Enemy_Boss enemy;
    protected Transform player;

    public BossGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss _enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        // Get reference to the player
        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        // If the boss detects the player or is within a 2-unit distance,
        // transition to the boss battle state.
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
