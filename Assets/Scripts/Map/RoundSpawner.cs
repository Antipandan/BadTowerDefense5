using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RoundSpawner : MonoBehaviour
{
    [SerializeField] private MapEvent mapEvent;
    [SerializeField] private List<Round> rounds = new List<Round>();
    [SerializeField] private SplineContainer bloonPath;
    private void SpawnEnemies()
    {
        for (int i = 0; i < rounds.Count; i++)
        {
            Round round = rounds[i];
            for (int j = 0; j < rounds[i].SpawnData.Count; j++)
            {
                StartCoroutine(round.SpawnData[j].SpawnEnemies(mapEvent, bloonPath));
            }
        }
    }
    

    private void Start()
    {
        SpawnEnemies();
    }
}