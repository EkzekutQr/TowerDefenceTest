using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicCannonTower : TowerBase, IAimable
{
    [SerializeField] protected float projectileSpeedMultiplier = 1.1f;
    [SerializeField] protected float rotationSpeed = 5f;
    protected const float angleInDegrees = 0;

    protected IProjectileCalculator _projectileCalculator;

    protected virtual void Awake()
    {
        _projectileCalculator = new ParabolicProjectileCalculator(angleInDegrees, projectileSpeed, Physics.gravity.y);
    }

    protected override void Update()
    {
        base.Update();
        if (targets.Count != 0)
            AimAtTarget(targets[0]);
    }

    public override GameObject Shoot(GameObject target)
    {
        Vector3 targetVelocity = GetTargetVelocity(target.transform);
        Vector3 targetPredictedPosition = _projectileCalculator.CalculatePredictedPosition(target.transform.position, targetVelocity, shootPoint.position);
        float requiredSpeed = _projectileCalculator.CalculateRequiredSpeed(shootPoint.position, targetPredictedPosition);

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

        Vector3 targetVelocity = GetTargetVelocity(target.transform);
        Vector3 predictedPosition = _projectileCalculator.CalculatePredictedPosition(target.transform.position, targetVelocity, shootPoint.position);
        Vector3 direction = predictedPosition - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private Vector3 GetTargetVelocity(Transform target)
    {
        IMovable movable = target.GetComponent<IMovable>();
        return movable.Velocity * (1 / Time.fixedDeltaTime);
    }

    private Vector3 GetShotDirection(Vector3 targetPredictedPosition, Vector3 shotPointPosition)
    {
        return (targetPredictedPosition - shotPointPosition).normalized;
    }
}
