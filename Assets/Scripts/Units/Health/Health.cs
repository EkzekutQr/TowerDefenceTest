using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : HealthBase
{
    [SerializeField] protected int maxHP = 30;
    [SerializeField] protected int _hp;

    public override int MaxHP => maxHP;
    public override int Hp => _hp;

    protected void Start()
    {
        _hp = maxHP;
    }
    public override void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
