using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicCannonTower : TowerBase, IAimable
{
    [SerializeField] protected float projectileSpeedMultiplier = 1.1f;
    [SerializeField] protected float rotationSpeed = 5f;
    protected const float angleInDegrees = 0;

    protected override void Update()
    {
        base.Update();
        if (targets.Count != 0)
            AimAtTarget(targets[0]);
    }

    protected float GetShootAngleRadians(float angle)
    {
        return Mathf.Deg2Rad * angle;
    }

    protected float GetDeltaHeight(float targetHeight, float cannonHeight)
    {
        return (targetHeight - cannonHeight);
    }

    protected float GetProjectileFlyTime(float angleInRadians, float deltaY, float projectileSpeed, float g)
    {
        float vInitial = projectileSpeed;
        return (vInitial * Mathf.Sin(angleInRadians) + Mathf.Sqrt(Mathf.Pow(vInitial * Mathf.Sin(angleInRadians), 2) + 2 * g * deltaY)) / g;
    }

    protected Vector3 GetTargetVelocity(Transform target)
    {
        IMovable movable = target.GetComponent<IMovable>();
        return movable.Velocity * (1 / Time.fixedDeltaTime);
    }

    protected Vector3 GetTargetPredictedPosition(Vector3 targetCurrentPosition, Vector3 targetVelocity, float projectileFlyTime)
    {
        return targetCurrentPosition + targetVelocity * projectileFlyTime;
    }

    protected Vector3 GetShotDirection(Vector3 targetPredictedPosition, Vector3 shotPointPosition)
    {
        return (targetPredictedPosition - shotPointPosition).normalized;
    }

    public override GameObject Shoot(GameObject target)
    {
        float angleInRadians = GetShootAngleRadians(angleInDegrees);
        float deltaHeight = GetDeltaHeight(target.transform.position.y, shootPoint.position.y);
        Vector3 targetVelocity = GetTargetVelocity(target.transform);

        float projectileFlyTime = GetProjectileFlyTime(angleInRadians, deltaHeight, projectileSpeed, Physics.gravity.y);

        Vector3 targetPredictedPosition = GetTargetPredictedPosition(target.transform.position, targetVelocity, projectileFlyTime);
        float distanceToTarget = Vector3.Distance(shootPoint.position, targetPredictedPosition);

        float requiredSpeed = distanceToTarget / projectileFlyTime;

        Vector3 shotDirection = GetShotDirection(targetPredictedPosition, shootPoint.position);
        shotDirection = new Vector3(shotDirection.x, 0, shotDirection.z);

        Transform newProjectileTr = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation, null).transform;

        IProjectile projectile = newProjectileTr.GetComponent<IProjectile>();
        projectile.Initialize(target.transform, requiredSpeed, projectileDamage);

        Rigidbody newProjectileRb = newProjectileTr.GetComponent<Rigidbody>();
        newProjectileRb.AddForce(-shotDirection * requiredSpeed * projectileSpeedMultiplier, ForceMode.VelocityChange);

        return newProjectileTr.gameObject;
    }

    public void AimAtTarget(GameObject target)
    {
        if (target == null)
            return;

        float angleInRadians = GetShootAngleRadians(angleInDegrees);
        float deltaHeight = GetDeltaHeight(target.transform.position.y, shootPoint.position.y);
        Vector3 targetVelocity = GetTargetVelocity(target.transform);

        float projectileFlyTime = GetProjectileFlyTime(angleInRadians, deltaHeight, projectileSpeed, Physics.gravity.y);
        Vector3 predictedPosition = GetTargetPredictedPosition(target.transform.position, targetVelocity, projectileFlyTime);
        Vector3 direction = predictedPosition - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}