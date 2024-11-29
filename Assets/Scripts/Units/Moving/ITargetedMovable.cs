using UnityEngine;

public interface ITargetedMovable : IMovable
{
    Transform MoveTarget { get; set; }
}
