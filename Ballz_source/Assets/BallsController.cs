using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsController : MonoSingleton<BallsController>
{
    [SerializeField] private Text scoreText;
    public Text amountBallsText;

    public bool isBreakingStuff { get; set; }
    public bool firstBallLanded { get; set; }

    public int amountBalls { get; set; }
    public int ammountBallsLeft { get; set; }
    private int score;

    [SerializeField] private Transform firstBall;

    // Use this for initialization
    void Start ()
    {
        firstBallLanded = false;
        amountBalls = 1;
        UpdateText();
        ammountBallsLeft = amountBalls;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AllBallLanded()
    {
        isBreakingStuff = false;
        firstBallLanded = false;
        ammountBallsLeft = amountBalls;
        BlockContainer.Instance.GenerateNewRow();
        score++;
        UpdateText();
        amountBallsText.gameObject.SetActive(true);
    }

    public void UpdateText()
    {
        scoreText.text = score.ToString();
        amountBallsText.text = 'x' + amountBalls.ToString();
        amountBallsText.rectTransform.position = Camera.main.WorldToScreenPoint(firstBall.position) + new Vector3(20, 5, 0);
    }

    public void CollectBall()
    {
        amountBalls++;
    }
}
