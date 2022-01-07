using System;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject EnemyPrefab;

    private Character _player;
    private Character _enemy;

    public Character Player => _player;
    public Character Enemy => _enemy;

    void Start()
    {
        _player = new Character() {
            MaxHealth = 100,
            CurrentHealth = 100
        };
        _enemy = new Character() {
            MaxHealth = 100,
            CurrentHealth = 100
        };
    }

    public void OnAttack() {
        Attack();
    }

    private void Attack() {
        Enemy.CurrentHealth -= Player.Power;
        Debug.Log(Enemy.CurrentHealth);
    }

}
