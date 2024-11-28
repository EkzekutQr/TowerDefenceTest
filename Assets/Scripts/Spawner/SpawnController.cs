using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] protected List<Transform> spawnPoints;
    [SerializeField] protected List<Transform> moveTargets;
    [SerializeField] protected List<GameObject> monsters;

    protected List<ISpawnHandler> spawners = new List<ISpawnHandler>();

    protected void Start()
    {
        InitializeSpawners();
    }

    protected void InitializeSpawners()
    {
        spawners.Add(new IntervalSpawner(3f, moveTargets[0], spawnPoints[0], monsters[0]));
    }

    protected void Update()
    {
        HandleSpawners();
    }

    protected void HandleSpawners()
    {
        foreach (var item in spawners)
        {
            item.SpawnHandler();
        }
    }
}
