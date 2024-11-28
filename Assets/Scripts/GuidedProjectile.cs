using UnityEngine;
using System.Collections;

public class GuidedProjectile : ProjectileBase
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

    protected void FixedUpdate()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        MoveTowardsTarget();
    }

    protected void MoveTowardsTarget()
    {
        var translation = _target.position - transform.position;
        translation = translation.normalized * _speed * Time.fixedDeltaTime;
        transform.Translate(translation);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.transform == _target.transform)
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}