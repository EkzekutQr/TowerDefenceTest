using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<Transform> moveTargets;
    [SerializeField] private List<Monster> monsters;

    private List<ISpawnHandler> spawners = new List<ISpawnHandler>();

    void Start()
    {
        InitializeSpawners();
    }

    protected void InitializeSpawners()
    {
        spawners.Add(new IntervalSpawner(3f, moveTargets[0], spawnPoints[0], monsters[0]));
    }

    void Update()
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
