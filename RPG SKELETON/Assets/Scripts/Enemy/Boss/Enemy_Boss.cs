using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
{
    #region States

    public BossIdleState idleState { get; private set; }
    public BossMoveState moveState { get; private set; }
    public BossBattleState battleState { get; private set; }
    public BossAttackState attackState { get; private set; }
    public BossStunnedState stunnedState { get; private set; }
    public BossDeadState deadState { get; private set; }
    

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        moveState = new BossMoveState(this, stateMachine, "Move", this);
        battleState = new BossBattleState(this, stateMachine, "Move", this);
        attackState = new BossAttackState(this, stateMachine, "Attack", this);
        stunnedState = new BossStunnedState(this, stateMachine, "Stunned", this);
        deadState = new BossDeadState(this, stateMachine, "Dead", this);
        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(stunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}