 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockContainer : MonoSingleton<BlockContainer>
{
    private const float DISTANCE_BETWEEN_BLOCKS = 0.34f;

    public Transform rowContainer;
    public GameObject rowPrefab;
    private Vector2 rowContainerStartingPosition;

    private float currentSpawnY;
    private Vector2 desiredPosition;

    private int lastBallSpawn;

    //private List<Block> blocks = new List<Block>();

    private void Start()
    {
        rowContainerStartingPosition = rowContainer.transform.position;
        desiredPosition = rowContainerStartingPosition;
    }

    private void Update()
    {
        if ((Vector2)rowContainer.position != desiredPosition)
        {
            rowContainer.transform.position = Vector3.MoveTowards(rowContainer.transform.position, desiredPosition, Time.deltaTime);
        }
    }

    public void GenerateNewRow()
    {
        GameObject go = Instantiate(rowPrefab) as GameObject;
        go.transform.SetParent(rowContainer);
        go.transform.localPosition = Vector2.down * currentSpawnY;
        currentSpawnY -= DISTANCE_BETWEEN_BLOCKS;

        desiredPosition = rowContainerStartingPosition + Vector2.up * currentSpawnY;

        int ballSpawnIndex = -1;
        if (lastBallSpawn * 0.3f > 1f)
        {
            // Force spawn a ball
            ballSpawnIndex = Random.Range(0, 7);
        }

        Block[] bloackArray = go.GetComponentsInChildren<Block>();
        for (int i = 0; i < bloackArray.Length; i++)
        {
            if (ballSpawnIndex == i)
            {
                bloackArray[i].SpawnBall();
                lastBallSpawn = 0;
                return;
            }

            if (Random.Range(0f, 1f) > 0.5f)
            {
                bloackArray[i].Spawn();
            }
            else
            {
                bloackArray[i].Hide();
            }
        }
        lastBallSpawn++;
    }
}
