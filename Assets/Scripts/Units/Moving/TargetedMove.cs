using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedMove : UnitMoveBase, ITargetedMovable
{
    [SerializeField] protected float speed = 0.1f;

    protected Transform moveTarget;
    protected const float reachDistance = 0.3f;
    protected Vector3 velocity;

    public Transform MoveTarget { get => moveTarget; set => moveTarget = value; }
    public float ReachDistance { get => reachDistance; }
    public override float Speed => speed;
    public Vector3 Velocity { get => velocity; }

    protected void FixedUpdate()
    {
        Move();
    }

    protected void MoveTowardsTarget()
    {
        TryReachTarget();

        var moveTargetCurrentDistance = moveTarget.transform.position - transform.position;
        var direction = moveTargetCurrentDistance.normalized;
        var translation = direction.normalized * Speed * Time.fixedDeltaTime;
        velocity = -translation;
        transform.Translate(translation);
    }

    protected void TryReachTarget()
    {
        if (Vector3.Distance(transform.position, moveTarget.transform.position) <= reachDistance)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Initialize(Transform moveTarget)
    {
        this.moveTarget = moveTarget;
    }

    public override void Move()
    {
        MoveTowardsTarget();
    }

    public override void MoveHandler()
    {
        Move();
    }
}
