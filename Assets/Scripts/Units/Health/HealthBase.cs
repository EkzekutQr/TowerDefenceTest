using UnityEngine;

public abstract class HealthBase : MonoBehaviour, IDamagable
{
    public abstract int MaxHP { get; }
    public abstract int Hp { get; }
    public abstract void TakeDamage(int damage);
}
