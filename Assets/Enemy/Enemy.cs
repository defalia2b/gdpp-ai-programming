using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private IBaseState _currentState;
    public List<Transform> Waypoints = new();
    public float ChaseDistance;
    public PlayerController Player;

    [HideInInspector] public PatrolState PatrolState = new();
    [HideInInspector] public ChaseState ChaseState = new();
    [HideInInspector] public RetreatState RetreatState = new();
    [HideInInspector] public NavMeshAgent NavMeshAgent;
    [HideInInspector] public Animator Animator;

    public void SwitchState(IBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }


    private void Awake()
    {
        Animator = GetComponent<Animator>();
        _currentState = PatrolState;
        _currentState.EnterState(this);
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (_currentState != null) _currentState.UpdateState(this);
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_currentState != RetreatState)
            if (other.gameObject.CompareTag("Player"))
                other.gameObject.GetComponent<PlayerController>().Dead();
    }
}