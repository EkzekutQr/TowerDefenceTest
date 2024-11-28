using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntervalSpawner : SpawnerBase
{
    protected float interval;
    protected Transform moveTarget;
    protected float _lastSpawn = -1f;
    protected Transform spawnPoint;
    protected GameObject monsterPrefab;

    private List<GameObject> monsters = new List<GameObject>();

    public override float Interval => interval;
    public override Transform MoveTarget => moveTarget;
    public override Transform SpawnPoint => spawnPoint;
    public override List<GameObject> Monsters => monsters;

    public IntervalSpawner(float interval, Transform moveTarget, Transform spawnPoint, GameObject monsterPrefab)
    {
        this.interval = interval;
        this.moveTarget = moveTarget;
        this.spawnPoint = spawnPoint;
        this.monsterPrefab = monsterPrefab;
    }

    protected GameObject IntervalSpawnMonster()
    {
        GameObject monster = null;

        if (SpawnTimer())
            monster = SpawnMonster(monsterPrefab);

        return monster;
    }

    protected bool SpawnTimer()
    {
        if (Time.time > _lastSpawn + interval)
        {
            _lastSpawn = Time.time;
            return true;
        }
        else return false;
    }

    public override GameObject SpawnMonster(GameObject monsterPrefab)
    {
        var newMonster = GameObject.Instantiate(monsterPrefab.gameObject, spawnPoint.position, Quaternion.identity, spawnPoint);
        var movable = newMonster.GetComponent<ITargetedMovable>();
        movable.MoveTarget = moveTarget;

        return newMonster;
    }

    public override GameObject SpawnHandler()
    {
        return IntervalSpawnMonster();
    }
}
