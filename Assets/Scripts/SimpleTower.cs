using UnityEngine;
using System.Collections;

public class SimpleTower : MonoBehaviour
{
    public float ShootInterval = 0.5f;
    public float Range = 4f;
    public GameObject ProjectilePrefab;

    private float _lastShotTime = -0.5f;

    void Update()
    {
        TryShoot();
    }

    private void TryShoot()
    {
        if (ProjectilePrefab == null) return;

        foreach (var monster in FindObjectsOfType<Monster>())
        {
            if (Vector3.Distance(transform.position, monster.transform.position) > Range) continue;
            if (_lastShotTime + ShootInterval > Time.time) continue;

            Shoot(monster);
            _lastShotTime = Time.time;
        }
    }

    private void Shoot(Monster monster)
    {
        var projectileObject = Instantiate(ProjectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        var projectile = projectileObject.GetComponent<GuidedProjectile>();
        projectile.Initialize(monster, 0.2f, 10);
    }
}
