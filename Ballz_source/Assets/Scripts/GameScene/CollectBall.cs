using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
        BallsController.Instance.CollectBall();
    }
}
