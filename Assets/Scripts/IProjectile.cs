using UnityEngine;

public interface IProjectile
{
    Transform Target { get; }
    float Speed { get; }
    int Damage { get; }

    void Initialize(Transform target, float speed, int damage);
}
