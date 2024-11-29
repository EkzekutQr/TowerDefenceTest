using UnityEngine;

public class ParabolicProjectileCalculator : IProjectileCalculator
{
    private float _angleInDegrees;
    private float _projectileSpeed;
    private float _gravity;

    public ParabolicProjectileCalculator(float angleInDegrees, float projectileSpeed, float gravity)
    {
        _angleInDegrees = angleInDegrees;
        _projectileSpeed = projectileSpeed;
        _gravity = gravity;
    }

    public Vector3 CalculatePredictedPosition(Vector3 targetPosition, Vector3 targetVelocity, Vector3 shotPointPosition)
    {
        float angleInRadians = Mathf.Deg2Rad * _angleInDegrees;
        float deltaHeight = targetPosition.y - shotPointPosition.y;
        float projectileFlyTime = GetProjectileFlyTime(angleInRadians, deltaHeight, _projectileSpeed, _gravity);
        return targetPosition + targetVelocity * projectileFlyTime;
    }

    public float CalculateRequiredSpeed(Vector3 shotPointPosition, Vector3 targetPredictedPosition)
    {
        float distanceToTarget = Vector3.Distance(shotPointPosition, targetPredictedPosition);
        float angleInRadians = Mathf.Deg2Rad * _angleInDegrees;
        float deltaHeight = targetPredictedPosition.y - shotPointPosition.y;
        float projectileFlyTime = GetProjectileFlyTime(angleInRadians, deltaHeight, _projectileSpeed, _gravity);
        return distanceToTarget / projectileFlyTime;
    }

    private float GetProjectileFlyTime(float angleInRadians, float deltaY, float projectileSpeed, float g)
    {
        float vInitial = projectileSpeed;
        return (vInitial * Mathf.Sin(angleInRadians) + Mathf.Sqrt(Mathf.Pow(vInitial * Mathf.Sin(angleInRadians), 2) + 2 * g * deltaY)) / g;
    }
}