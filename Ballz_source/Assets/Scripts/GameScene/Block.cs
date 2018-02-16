using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int hp;

    public GameObject blockObject;
    public GameObject ballObject;

    public void Spawn()
    {
        int amountBalls = BallsController.Instance.amountBalls;
        hp = Random.Range(amountBalls - 3, amountBalls + 3);
        if (hp <= 0)
        {
            hp = 1;
        }
    }

    public void SpawnBall()
    {
        Hide();
        ballObject.SetActive(true);
    }

    public void Hide()
    {
        blockObject.SetActive(false);
    }

    public void ReceiveHit()
    {
        hp--;
        if (hp == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
