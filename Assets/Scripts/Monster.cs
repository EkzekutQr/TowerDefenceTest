using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour, ITarget
{
    public Transform MoveTarget;
    public float Speed = 0.1f;
    public int MaxHP = 30;
    private const float ReachDistance = 0.3f;

    private int _hp;

    public Vector3 Position => transform.position;

    void Start()
    {
        _hp = MaxHP;
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (MoveTarget == null) return;

        if (Vector3.Distance(transform.position, MoveTarget.transform.position) <= ReachDistance)
        {
            Destroy(gameObject);
            return;
        }

        var translation = MoveTarget.transform.position - transform.position;
        if (translation.magnitude > Speed)
        {
            translation = translation.normalized * Speed;
        }
        transform.Translate(translation);
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(Transform moveTarget)
    {
        MoveTarget = moveTarget;
    }
}

public interface ITarget
{
    Vector3 Position { get; }
    void TakeDamage(int damage);
}