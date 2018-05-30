using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    public Text ammountBallsText;
    [SerializeField] private Vector3 ammountBallsPos;

    private BlockContainer blockContainer;

    public bool isBreakingStuff { get; set; }

    public int amountBalls { get; set; }
    public int ammountBallsLeft { get; set; }
    public int score;

    [SerializeField] private Transform firstBall;

    private List<BallBehaviour> ballsInScene = new List<BallBehaviour>();
    [SerializeField] private GameObject ballToClone;
    private int ballsToInstanciate;
    private int ballsToThrow;
    private Vector3 positionToShoot;
    [SerializeField] private float fireRate = 0.25f;

    private bool isAllBallLanded = false;
    [SerializeField] private float speedToMove = 5f;

    private void Awake()
    {
        blockContainer = FindObjectOfType<BlockContainer>();
    }

    void Start ()
    {
        amountBalls = ballsToThrow = 1;
        UpdateText();
        ammountBallsLeft = amountBalls;

        ballsInScene.Add(FindObjectOfType<BallBehaviour>());
    }

    private void Update()
    {
        if (isAllBallLanded)
        {
            int counter = 0;
            foreach (BallBehaviour ballToMove in ballsInScene)
            {
                ballToMove.transform.position = Vector3.MoveTowards(ballToMove.transform.position, firstBall.position, speedToMove * Time.deltaTime);
                if (ballToMove.transform.position == firstBall.position)
                {
                    counter += 1;
                }
            }
            if (counter == ballsInScene.Count)
            {
                isBreakingStuff = false;
            }
        }
    }

    public void PrepareBallsToShoot(Vector3 dir)
    {
        isAllBallLanded = false;

        ballsToInstanciate = amountBalls - ballsToThrow;
        ballsToThrow = amountBalls;
        positionToShoot = ballsInScene[0].transform.position;
        for (int i = 0; i < ballsToInstanciate; i++)
        {
            GameObject ballCloned = Instantiate(ballToClone, positionToShoot, Quaternion.identity);

            BallBehaviour ballClass = ballCloned.GetComponent<BallBehaviour>();
            ballsInScene.Add(ballClass);
        }
        StartCoroutine(ShootBalls(dir));
    }

    private IEnumerator ShootBalls(Vector3 dir)
    {
        foreach (BallBehaviour ballToShoot in ballsInScene)
        {
            ballToShoot.SendBallInDirection(dir);
            ballToShoot.rigid.simulated = true;
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void AllBallLanded()
    {
        isAllBallLanded = true;
        ammountBallsLeft = amountBalls;
        blockContainer.GenerateNewRow();
        score++;
        UpdateText();
        ammountBallsText.gameObject.SetActive(true);
    }

    public void UpdateText()
    {
        scoreText.text = score.ToString();
        ammountBallsText.text = 'x' + amountBalls.ToString();
        ammountBallsText.rectTransform.position = Camera.main.WorldToScreenPoint(firstBall.position) + ammountBallsPos;
    }

    public void CollectBall()
    {
        amountBalls++;
    }
}
