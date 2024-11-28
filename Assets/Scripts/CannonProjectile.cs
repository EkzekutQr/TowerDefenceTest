using UnityEngine;
using System.Collections;

public class CannonProjectile : ProjectileBase
{
    protected Transform _target;
    protected float _speed;
    protected int _damage;

    public override void Initialize(Transform target, float speed, int damage)
    {
        _target = target;
        _speed = speed;
        _damage = damage;
    }

    protected void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<IDamagable>();
        if (target == null) return;

        target.TakeDamage(_damage);
        Destroy(gameObject);
    }
}