using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IProjectile
{
    public  Transform Target { get; }
    public  float Speed { get; }
    public  int Damage { get; }

    public abstract void Initialize(Transform target, float speed, int damage);
}
