using UnityEngine;

public interface IProjectileCalculator
{
    Vector3 CalculatePredictedPosition(Vector3 targetPosition, Vector3 targetVelocity, Vector3 shotPointPosition);
    float CalculateRequiredSpeed(Vector3 shotPointPosition, Vector3 targetPredictedPosition);
}
