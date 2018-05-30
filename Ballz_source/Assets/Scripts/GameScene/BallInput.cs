using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInput : MonoBehaviour
{
    private BallsController ballsController;
    private MobileInput mobileInput;
    private BlockContainer blockContainer;

    private Vector3 sd;
    [SerializeField] private float yAimingLimit = 0.05f;

    public Transform ballsPreview;
    public GameObject tutorialContainer;

    private int ballsToThrow;
    private Vector3 positionNextShoot;

    private void Awake()
    {
        mobileInput = FindObjectOfType<MobileInput>();
        ballsController = FindObjectOfType<BallsController>();
        blockContainer = FindObjectOfType<BlockContainer>();
        ballsPreview.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!ballsController.isBreakingStuff && !blockContainer.rowIsMoving)
        {
            PoolInput();
        }
    }

    private void PoolInput()
    {
        sd = mobileInput.swipeDelta;

        if (sd != Vector3.zero)
        {
            if(sd.y < yAimingLimit)
            {
                ballsPreview.gameObject.SetActive(false);
            }
            else
            {
                ballsPreview.up = sd.normalized;
                ballsPreview.gameObject.SetActive(true);

                if (mobileInput.release)
                {
                    tutorialContainer.SetActive(false);
                    ballsPreview.gameObject.SetActive(false);

                    ballsController.isBreakingStuff = true;
                    blockContainer.rowIsMoving = true;

                    ballsController.ammountBallsText.gameObject.SetActive(false);

                    ballsController.PrepareBallsToShoot(sd.normalized);
                    mobileInput.swipeDelta = Vector3.zero;
                }
            }
        }
    }
}
