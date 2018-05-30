using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject ammountBalls;

    [SerializeField] private GameObject GameElements;

    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject bestScoreObject;

    private BallsController ballsController;
    private MobileInput mobileInput;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        mobileInput = GetComponent<MobileInput>();
        ballsController = FindObjectOfType<BallsController>();
    }

    private void Start()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        bestScoreText = bestScoreObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        mobileInput.enabled = false;
        inGameUI.SetActive(false);
        ammountBalls.SetActive(false);
        GameElements.SetActive(false);

        scoreUI.SetActive(true);
        scoreText.text = ballsController.score.ToString();
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
}
