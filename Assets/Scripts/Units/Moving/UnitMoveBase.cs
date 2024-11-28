using UnityEngine;

public abstract class UnitMoveBase : MonoBehaviour, IMoveHandler
{
    public abstract float Speed { get; }
    public abstract void Move();

    public abstract void MoveHandler();
}
