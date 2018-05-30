using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBall : MonoBehaviour
{
    private GameObject block;

    private BallsController ballsController;

    private void Awake()
    {
        ballsController = FindObjectOfType<BallsController>();
    }

    private void Start()
    {
        block = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(block);
        ballsController.CollectBall();
    }
}
