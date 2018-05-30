using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private float yToDie = -1.14f;
    private GameStateController gamesStateController;

	// Use this for initialization
	void Start ()
    {
        gamesStateController = FindObjectOfType<GameStateController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < yToDie)
        {
            gamesStateController.GameEnd();
        }
    }
}
