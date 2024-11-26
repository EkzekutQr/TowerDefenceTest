using UnityEngine;
using System.Collections;

public class GuidedProjectile : MonoBehaviour, IProjectile
{
    private ITarget _target;
    private float _speed;
    private int _damage;

    public void Initialize(ITarget target, float speed, int damage)
    {
        _target = target;
        _speed = speed;
        _damage = damage;
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        var translation = _target.Position - transform.position;
        if (translation.magnitude > _speed)
        {
            translation = translation.normalized * _speed;
        }
        transform.Translate(translation);
    }

    void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<ITarget>();
        if (target == null) return;

        target.TakeDamage(_damage);
        Destroy(gameObject);
    }
}

public interface IProjectile
{
    void Initialize(ITarget target, float speed, int damage);
}
