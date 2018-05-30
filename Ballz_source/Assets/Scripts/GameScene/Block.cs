using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int hp;

    public GameObject blockObject;
    public GameObject ballObject;
    private TextMeshPro hpText;

    private BallsController ballsController;

    private void Awake()
    {
        ballsController = FindObjectOfType<BallsController>();
        hpText = GetComponent<TextMeshPro>();
    }

    public void SpawnBlock()
    {
        blockObject.SetActive(true);
        int amountBalls = ballsController.amountBalls;
        hp = Random.Range(amountBalls - 3, amountBalls + 3);
        if (hp <= 0)
        {
            hp = 1;
        }
        UpdateHpText();
    }

    public void SpawnBall()
    {
        ballObject.SetActive(true);
        hpText.enabled = false;
    }

    public void SpawnNothing()
    {
        Destroy(gameObject);
    }

    public void ReceiveHit()
    {
        hp--;
        if (hp == 0)
        {
            Destroy(this.gameObject);
        }
        UpdateHpText();
    }

    private void UpdateHpText()
    {
        hpText.text = hp.ToString();
    }
}
