using UnityEngine;
using System.Collections;
using System.Linq;

public class CannonTower : MonoBehaviour, ITower
{
    public float ShootInterval = 0.5f;
    public float Range = 4f;
    public GameObject ProjectilePrefab;
    public Transform ShootPoint;

    private float _lastShotTime = -0.5f;

    void Update()
    {
        TryShoot();
    }

    private void TryShoot()
    {
        if (ProjectilePrefab == null || ShootPoint == null) return;

        foreach (var target in FindObjectsOfType<MonoBehaviour>().OfType<ITarget>())
        {
            if (Vector3.Distance(transform.position, target.Position) > Range) continue;
            if (_lastShotTime + ShootInterval > Time.time) continue;

            Shoot(target);
            _lastShotTime = Time.time;
        }
    }

    private void Shoot(ITarget target)
    {
        var projectileObject = Instantiate(ProjectilePrefab, ShootPoint.position, ShootPoint.rotation);
        var projectile = projectileObject.GetComponent<IProjectile>();
        projectile.Initialize(target, 0.2f, 10);
    }
}

public interface ITower
{
}