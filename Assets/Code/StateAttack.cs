﻿// Only here for the Debug.Log function
using UnityEngine;

public class StateAttack
{
    private bool _isBlocking;
    private bool _isDodging;
    private bool _isCountering;

    public bool IsBlocking { get => _isBlocking; set => _isBlocking = value; }
    public bool IsDodging { get => _isDodging; set => _isDodging = value; }
    public bool IsCountering { get => _isCountering; set => _isCountering = value; }

    public void Start_StateAttack() 
    {
        // Check to see if the opponent is doing anything that complicates attacking
        if (IsBlocking) 
        {
            Debug.Log("They are blocking");
        }
        if (IsDodging) 
        {
            Debug.Log("They are dodging");
        }
        if (IsCountering) 
        {
            Debug.Log("They are countering");
        }

        // If the opponent wasn't doing anything, go to the attack function
        Attack();
    }

    private void Attack() 
    {
        CombatStateMachine.Instance.Enemy.CurrentHealth -= CombatStateMachine.Instance.Player.Power;
        Debug.Log(CombatStateMachine.Instance.Enemy.CurrentHealth);
        CombatStateMachine.Instance.sEndTurn.Start_StateEndTurn();
    }

    public StateAttack() 
    {
        _isBlocking = false;
        _isDodging = false;
        _isCountering = false;
    }
}