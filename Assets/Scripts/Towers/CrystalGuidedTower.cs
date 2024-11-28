using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGuidedTower : TowerBase
{
    public override GameObject Shoot(GameObject target)
    {
        Transform newProjectileTr = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation, null).transform;
        IProjectile guidedProjectile = newProjectileTr.GetComponent<IProjectile>();
        guidedProjectile.Initialize(target.transform, projectileSpeed, projectileDamage);
        return newProjectileTr.gameObject;
    }
}
