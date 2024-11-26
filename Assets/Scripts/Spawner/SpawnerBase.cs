using UnityEngine;
using System.Collections.Generic;

public abstract class SpawnerBase : ISpawnHandler
{
    public abstract float Interval { get; }
    public abstract Transform MoveTarget { get; }
    public abstract Transform SpawnPoint { get; }
    public abstract List<Monster> Monsters { get; }

    public abstract Monster SpawnHandler();
    public abstract Monster SpawnMonster(Monster monsterPrefab);
}
