using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float shootInterval;
    [SerializeField] protected int projectileDamage;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected float lastShotTime;
    [SerializeField] protected List<GameObject> targets = new List<GameObject>();

    public float ShootInterval { get => shootInterval; set => shootInterval = value; }
    public float Range { get => range; set => range = value; }
    public GameObject ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }

    public abstract GameObject Shoot(GameObject target);

    protected virtual void Update()
    {
        DetectTarget();
    }

    protected bool IsTargetInRange(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition) <= Range;
    }

    protected void DetectTarget()
    {
        if (targets.Count == 0) return;
        if (!ShootTimer()) return;

        if (targets[0] == null)
        {
            targets.RemoveAt(0);
            return;
        }
        GameObject target = targets[0];

        GameObject newProjectile = Shoot(target);
        if (newProjectile == null) return;
        lastShotTime = Time.time;
    }

    protected bool ShootTimer()
    {
        return Time.time > lastShotTime + ShootInterval;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
            targets.Add(other.gameObject);
    }
    protected void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.gameObject))
            targets.Remove(other.gameObject);
    }
}