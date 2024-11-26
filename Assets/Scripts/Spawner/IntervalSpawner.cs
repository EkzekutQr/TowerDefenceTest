using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntervalSpawner : SpawnerBase
{
    protected float interval;
    protected Transform moveTarget;
    protected float _lastSpawn = -1f;
    protected Transform spawnPoint;
    protected Monster monsterPrefab;

    private List<Monster> monsters = new List<Monster>();

    public override float Interval => interval;
    public override Transform MoveTarget => moveTarget;
    public override Transform SpawnPoint => spawnPoint;
    public override List<Monster> Monsters => monsters;

    public IntervalSpawner(float interval, Transform moveTarget, Transform spawnPoint, Monster monsterPrefab)
    {
        this.interval = interval;
        this.moveTarget = moveTarget;
        this.spawnPoint = spawnPoint;
        this.monsterPrefab = monsterPrefab;
    }

    protected Monster IntervalSpawnMonster()
    {
        Monster monster = null;

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

    public override Monster SpawnMonster(Monster monsterPrefab)
    {
        var newMonster = GameObject.Instantiate(monsterPrefab.gameObject, spawnPoint.position, Quaternion.identity, spawnPoint);
        var monster = newMonster.GetComponent<Monster>();
        monsters.Add(monster);
        monster.Initialize(moveTarget);

        return monster;
    }

    public override Monster SpawnHandler()
    {
        return IntervalSpawnMonster();
    }
}
