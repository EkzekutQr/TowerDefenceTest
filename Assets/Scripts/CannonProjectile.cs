using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour, IProjectile
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
        MoveForward();
    }

    private void MoveForward()
    {
        var translation = transform.forward * _speed;
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