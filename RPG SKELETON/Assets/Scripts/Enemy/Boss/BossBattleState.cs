using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleState : EnemyState
{
    private Enemy_Boss enemy;
    private Transform player;
    private int moveDir;
    // Threshold to avoid frequent flipping when the positions are nearly identical
    private float directionThreshold = 0.5f;

    public BossBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss _enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        Debug.Log("Entered Battle State");
    }

    public override void Update()
    {
        base.Update();

        // Store detection result in a local variable
        RaycastHit2D hit = enemy.IsPlayerDetected();

        // Fallback: if the raycast didn't detect the player, check distance manually
        float distance = Vector2.Distance(player.position, enemy.transform.position);
        bool playerInRange = distance < enemy.attackDistance;
        Debug.Log(distance);
        bool inRange = hit.collider != null || playerInRange;

        if (inRange)
        {
            //Debug.Log("HitColl" + hit.collider);
            //Debug.Log("Player Detected");
            stateTimer = enemy.battleTime;

            // Use the distance from the raycast if available, otherwise fall back to calculated distance
            float rayDistance = hit.collider != null ? hit.distance : Vector2.Distance(player.position, enemy.transform.position);
            if (rayDistance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                    Debug.Log("Attacking");
                }
            }

        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.position, enemy.transform.position) > 7)
            {
                Debug.Log("Returning to Idle State");
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        // Calculate horizontal difference between player and boss
        float deltaX = player.position.x - enemy.transform.position.x;
        if (Mathf.Abs(deltaX) > directionThreshold)
        {
            // Update moveDir only if difference exceeds threshold
            moveDir = deltaX > 0 ? 1 : -1;
            Debug.Log(moveDir == 1 ? "Moving Right" : "Moving Left");
        }
        else
        {
            // When the difference is very small, do not change direction
            moveDir = 0;
            Debug.Log("No significant horizontal change");
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.linearVelocity.y);
    }


    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        Debug.Log("Attack is on cooldown");
        return false;
    }
}