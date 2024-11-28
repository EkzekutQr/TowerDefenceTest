using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicCannonTower : TowerBase, IAimable
{
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] protected float angleInDegrees = 45f;

    protected override void Update()
    {
        base.Update();
        if (targets.Count != 0)
            AimAtTarget(targets[0]);
    }
    protected float GetShootAngleRadians(float angle)
    {
        angle = Mathf.Deg2Rad * angleInDegrees;

        return angle;
    }

    protected float GetDeltaHeight(float targetHeight, float cannonHeight)
    {
        float deltaY = targetHeight - cannonHeight;

        return deltaY;
    }

    protected float GetProjectileFlyTime(float angleInRadians, float deltaY, float projectileSpeed, float g)
    {
        float vInitial = projectileSpeed;
        float t = (vInitial * Mathf.Sin(angleInRadians) + Mathf.Sqrt(Mathf.Pow(vInitial * Mathf.Sin(angleInRadians), 2) + 2 * g * deltaY)) / g;
        return t;
    }

    protected bool GetTargetVelocity(Transform target, out Vector3 velocity)
    {
        //velocity = targetOldPosition - target.position;
        if (target.TryGetComponent<IMovable>(out IMovable movable))
        {
            velocity = movable.Velocity;
            return true;
        }
        else
        {
            velocity = Vector3.zero;
            return false;
        }
    }

    protected Vector3 GetTargetPredictedPosition(Vector3 targetCurrentPosition, Vector3 targetVelocity, float projectileFlyTime)
    {
        Vector3 targetPredictedPosition = targetCurrentPosition + ((targetVelocity * (projectileFlyTime * (1 / Time.fixedDeltaTime)) * 0.5f));

        return targetPredictedPosition;
    }

    protected Vector3 GetShotDirection(Vector3 targetPredictedPosition, Vector3 shotPointPosition)
    {
        Vector3 direction = (targetPredictedPosition - shotPointPosition).normalized;

        return direction;
    }

    public override GameObject Shoot(GameObject target)
    {
        float angleInRadians = GetShootAngleRadians(angleInDegrees);
        float deltaHeight = GetDeltaHeight(target.transform.position.y, shootPoint.position.y);
        float projectileFlyTime = GetProjectileFlyTime(angleInRadians, deltaHeight, projectileSpeed, Physics.gravity.y);
        Vector3 targetVelocity;
        if (!GetTargetVelocity(target.transform, out targetVelocity)) return null;
        Vector3 targetPredictedPosition = GetTargetPredictedPosition(target.transform.position, targetVelocity, projectileFlyTime);
        Vector3 shotDirection = GetShotDirection(targetPredictedPosition, shootPoint.position);

        Transform newProjectileTr = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation, null).transform;

        IProjectile projectile = newProjectileTr.GetComponent<IProjectile>();
        projectile.Initialize(target.transform, projectileSpeed, projectileDamage);

        Rigidbody newProjectileRb = newProjectileTr.GetComponent<Rigidbody>();
        newProjectileRb.AddForce(shotDirection * projectileSpeed, ForceMode.VelocityChange);

        GameObject newProjectile = newProjectileTr.gameObject;

        return newProjectile;
    }

    public void AimAtTarget(GameObject target)
    {
        if (target == null)
            return;

        float angleInRadians = GetShootAngleRadians(angleInDegrees);
        float deltaHeight = GetDeltaHeight(target.transform.position.y, shootPoint.position.y);
        float projectileFlyTime = GetProjectileFlyTime(angleInRadians, deltaHeight, projectileSpeed, Physics.gravity.y);
        Vector3 targetVelocity;
        if (!GetTargetVelocity(target.transform, out targetVelocity)) return;
        Vector3 predictedPosition = GetTargetPredictedPosition(target.transform.position, targetVelocity, projectileFlyTime);
        Vector3 direction = predictedPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
