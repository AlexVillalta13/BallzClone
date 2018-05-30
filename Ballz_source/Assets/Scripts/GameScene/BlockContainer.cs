 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockContainer : MonoBehaviour
{
    private const float DISTANCE_BETWEEN_BLOCKS = 0.34f;

    private BallsController ballsController;

    public Transform rowContainer;
    public GameObject rowPrefab;
    private Vector2 rowContainerStartingPosition;
    public bool rowIsMoving;

    private float currentSpawnY;
    private Vector2 desiredPosition;

    private int lastBallSpawn;

    private void Awake()
    {
        ballsController = FindObjectOfType<BallsController>();
    }

    private void Start()
    {
        rowContainerStartingPosition = rowContainer.transform.position;
        desiredPosition = rowContainerStartingPosition;
        GenerateNewRow();
    }

    private void Update()
    {
        if ((Vector2)rowContainer.position != desiredPosition)
        {
            rowContainer.transform.position = Vector3.MoveTowards(rowContainer.transform.position, desiredPosition, Time.deltaTime);
        }

        if (!ballsController.isBreakingStuff && (Vector2)rowContainer.position == desiredPosition)
        {
            rowIsMoving = false;
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
            ballSpawnIndex = Random.Range(0, 7);
        }

        Block[] bloackArray = go.GetComponentsInChildren<Block>();
        for (int i = 0; i < bloackArray.Length; i++)
        {
            if (ballSpawnIndex == i)
            {
                bloackArray[i].SpawnBall();
                lastBallSpawn = 0;
                continue;
            }

            if (Random.Range(0f, 1f) > 0.5f)
            {
                bloackArray[i].SpawnBlock();
            }
            else
            {
                bloackArray[i].SpawnNothing();
            }
        }
        lastBallSpawn++;
    }
}
