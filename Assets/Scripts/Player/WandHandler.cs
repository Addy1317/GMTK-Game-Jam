using UnityEngine;
using UnityEngine.UI;

namespace GameJam.GMTK.Wand
{
    public enum PlayerWandSelection
    {
        None = 0,
        ShrinkWand = 1,
        ExpandWand = 2
    }

    public class WandHandler : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;

        [SerializeField] private PlayerWandSelection playerWandSelection = PlayerWandSelection.None;

        [SerializeField] private GameObject shrinkWand;
        [SerializeField] private GameObject expandWand;

        [SerializeField] private float shrinkRate = 0.9f;
        [SerializeField] private float expandRate = 1.1f;

        [SerializeField] private Image wandIndicatorImage;
        [SerializeField] private Sprite[] wandSprites;

        private Transform selectedObject;

        void Start()
        {
            SelectWand();
        }

        void Update()
        {
            HandleWandSwitching();
            HandleObjectSelection();
            HandleObjectManipulation();
        }

        private void HandleWandSwitching()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                playerWandSelection = (PlayerWandSelection)(((int)playerWandSelection + 1) % 2);
                SelectWand();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                playerWandSelection--;
                if (playerWandSelection < 0) playerWandSelection = PlayerWandSelection.ExpandWand;
                SelectWand();
            }
        }

        private void HandleObjectSelection()
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button for selection
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    selectedObject = hit.transform;
                }
            }
        }

        private void HandleObjectManipulation()
        {
            if (selectedObject == null) return;

            if (Input.GetMouseButtonDown(1)) // Right mouse button for manipulation
            {
                switch (playerWandSelection)
                {
                    case PlayerWandSelection.ShrinkWand:
                        ShrinkObject(selectedObject);
                        break;

                    case PlayerWandSelection.ExpandWand:
                        ExpandObject(selectedObject);
                        break;
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
                    wandIndicatorImage.sprite = wandSprites[0];
                    break;

                case PlayerWandSelection.ExpandWand:
                    shrinkWand.SetActive(false);
                    expandWand.SetActive(true);
                    wandIndicatorImage.sprite = wandSprites[1];
                    break;
            }
        }

        private void ShrinkObject(Transform obj)
        {
            obj.localScale *= shrinkRate;
        }

        private void ExpandObject(Transform obj)
        {
            obj.localScale *= expandRate;
        }

        private void OnDrawGizmos()
        {
            if (selectedObject != null)
            {
                // Set the color for the Gizmo
                Gizmos.color = Color.yellow;

                // Draw a wireframe cube around the selected object to indicate selection
                Gizmos.DrawWireCube(selectedObject.position, selectedObject.localScale);
            }
        }
    }
}