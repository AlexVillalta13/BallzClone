using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoSingleton<Ball>
{
    //public Text scoreText, amountBallsText;

    private const float DEADZONE = 30f;
    private const float MAXIMUM_PULL = 50f;
    private const float LOSE_Y = -1.05f;

    //private bool isBreakingStuff;
    //private bool firstBallLanded = false;

    private Vector3 sd;

    private Rigidbody2D rigid;
    public Transform ballsPreview;
    public GameObject tutorialContainer;
    private BallsController ballsController;

    [SerializeField] private float speed = 4f;
    //public int amountBalls = 1;
    //private int ammountBallsLeft;
    //private int score;

    private void Start()
    {
        ballsController = FindObjectOfType<BallsController>();
        rigid = GetComponent<Rigidbody2D>();
        ballsPreview.parent.gameObject.SetActive(false);
        //ammountBallsLeft = amountBalls;
        //UpdateText();
        BlockContainer.Instance.GenerateNewRow();
    }

    private void Update()
    {
        if(!BallsController.Instance.isBreakingStuff)
        {
            PoolInput();
        }
    }

    private void PoolInput()
    {
        sd = MobileInput.Instance.swipeDelta;

        if (sd != Vector3.zero)
        {
            if(sd.y < 0)
            {
                ballsPreview.parent.gameObject.SetActive(false);
            }
            else
            {
                ballsPreview.parent.up = sd.normalized;
                ballsPreview.parent.gameObject.SetActive(true);
                //ballsPreview.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(1, 3, 1), sd.magnitude / MAXIMUM_PULL);

                if (MobileInput.Instance.release)
                {
                    tutorialContainer.SetActive(false);
                    ballsPreview.parent.gameObject.SetActive(false);
                    BallsController.Instance.isBreakingStuff = true;

                    SendBallInDirection(sd.normalized);

                    ballsController.amountBallsText.gameObject.SetActive(false);
                    //amountBallsText.gameObject.SetActive(false);
                    rigid.simulated = true;
                }
            }
        }
    }

    private void SendBallInDirection(Vector2 dir)
    {
        rigid.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            TouchFloor();
        }
        else if (coll.gameObject.tag == "Block")
        {
            coll.transform.parent.SendMessage("ReceiveHit");
        }
    }

    private void TouchFloor()
    {
        ballsController.ammountBallsLeft--;
        //ammountBallsLeft--;
        if (!BallsController.Instance.firstBallLanded)
        {
            BallsController.Instance.firstBallLanded = true;
            rigid.velocity = Vector2.zero;
            rigid.simulated = false;
        }

        if (ballsController.ammountBallsLeft <= 0)
        {
            ballsController.AllBallLanded();
            //AllBallLanded();
        }
    }

    //private void AllBallLanded()
    //{
    //    isBreakingStuff = false;
    //    firstBallLanded = false;
    //    ammountBallsLeft = amountBalls;
    //    BlockContainer.Instance.GenerateNewRow();
    //    score++;
    //    UpdateText();
    //    amountBallsText.gameObject.SetActive(true);
    //}

    //public void UpdateText()
    //{
    //    scoreText.text = score.ToString();
    //    amountBallsText.text = 'x' + amountBalls.ToString();
    //    amountBallsText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(20, 5, 0);
    //}

    //public void CollectBall()
    //{
    //    amountBalls++;
    //}

    //private void OnCollisionEnter2D(Collision2D wall)
    //{
    //    Vector2 wallPoint = wall.contacts[0].normal;
    //    Vector2 newDirection = Vector2.Reflect(rigid.velocity, wallPoint);
    //    SendBallInDirection(newDirection);
    //}
}
