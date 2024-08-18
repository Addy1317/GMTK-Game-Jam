using UnityEngine;
using UnityEngine.UI;

public enum PlayerWandSelection
{
    ShrinkWand = 0,
    ExpandWand = 1
}

public class WandController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    [SerializeField] private PlayerWandSelection playerWandSelection = PlayerWandSelection.ShrinkWand;

    [SerializeField] private GameObject shrinkWand;
    [SerializeField] private GameObject expandWand;

    [SerializeField] private float shrinkRate = 0.9f;
    [SerializeField] private float expandRate = 1.1f;

    [SerializeField] private Image wandIndicatorImage;
    [SerializeField] private Sprite[] wandSprites;

    void Start()
    {
        SelectWand();
    }

    void Update()
    {
        HandleWandSwitching();
        PlayerWandShoot();
    }

    private void HandleWandSwitching()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            playerWandSelection = (PlayerWandSelection)(((int)playerWandSelection + 1) % 2);
            wandIndicatorImage.sprite = wandSprites[0];

            SelectWand();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            playerWandSelection--;
            if (playerWandSelection < 0) playerWandSelection = PlayerWandSelection.ExpandWand;
            wandIndicatorImage.sprite = wandSprites[1];

            SelectWand();
        }
    }

    private void PlayerWandShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                switch (playerWandSelection)
                {
                    case PlayerWandSelection.ShrinkWand:
                        ShrinkObject(hit.transform);
                        break;

                    case PlayerWandSelection.ExpandWand:
                        ExpandObject(hit.transform);
                        break;
                }
            }
        }
    }

    private void SelectWand()
    {
        switch (playerWandSelection)
        {
            case PlayerWandSelection.ShrinkWand:
                shrinkWand.SetActive(true);
                expandWand.SetActive(false);
                break;

            case PlayerWandSelection.ExpandWand:
                shrinkWand.SetActive(false);
                expandWand.SetActive(true);
                break;
        }
    }

    void ShrinkObject(Transform obj)
    {
        obj.localScale *= shrinkRate; // Shrink by shrinkRate factor
    }

    void ExpandObject(Transform obj)
    {
        obj.localScale *= expandRate; // Expand by expandRate factor
    }
}
