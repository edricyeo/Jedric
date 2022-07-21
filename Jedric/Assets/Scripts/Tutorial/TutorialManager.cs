using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private BossHealth dummy;
    //private float delay = 2f;
    private void Start()
    {
        popUps[0].SetActive(true);
    }

    private void Update()
    {
        if (popUpIndex == 0)
        {
            // movement
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                NextPopUp();
            }
        } else if (popUpIndex == 1)
        {
            // jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextPopUp();
            }
        } else if (popUpIndex == 2)
        {
            // wall climbing
            if (player.OnWall() && Input.GetKeyDown(KeyCode.Space))
            {
                NextPopUp();
            }
        } else if (popUpIndex == 3)
        {
            // shooting
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextPopUp();
                dummy.gameObject.SetActive(true);
            }
        } else if (popUpIndex == 4)
        {
            // defeating dummy
            if (dummy.currentHealth == 0)
            {
                NextPopUp();
            }
        } 
    }
    private void NextPopUp()
    {
        // deactivate current pop up
        popUps[popUpIndex].SetActive(false);
        popUpIndex++;
        // if there is a next popup, display it
        if (popUpIndex < popUps.Length)
        {
            popUps[popUpIndex].SetActive(true);
        }
    }
}
