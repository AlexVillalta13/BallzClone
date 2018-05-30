using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private BallsController ballsController;

    public Rigidbody2D rigid { get; set; }
    [SerializeField] private float speed = 4f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ballsController = FindObjectOfType<BallsController>();
    }

    public void SendBallInDirection(Vector3 dir)
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
        rigid.simulated = false;
        rigid.velocity = Vector2.zero;

        if (ballsController.ammountBallsLeft <= 0)
        {
            ballsController.AllBallLanded();
        }
    }

    //private void OnCollisionEnter2D(Collision2D wall)
    //{
    //    Vector2 wallPoint = wall.contacts[0].normal;
    //    Vector2 newDirection = Vector2.Reflect(rigid.velocity, wallPoint);
    //    SendBallInDirection(newDirection);
    //}
}
