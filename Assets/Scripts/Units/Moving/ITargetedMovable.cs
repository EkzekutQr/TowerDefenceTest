using UnityEngine;

public interface ITargetedMovable : IMovable
{
    Transform MoveTarget { get; set; }
}
public interface IMovable
{
    Vector3 Velocity { get; }
}