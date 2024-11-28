using UnityEngine;
using System.Collections.Generic;

public abstract class SpawnerBase : ISpawnHandler
{
    public abstract float Interval { get; }
    public abstract Transform MoveTarget { get; }
    public abstract Transform SpawnPoint { get; }
    public abstract List<GameObject> Monsters { get; }

    public abstract GameObject SpawnHandler();
    public abstract GameObject SpawnMonster(GameObject monsterPrefab);
}
