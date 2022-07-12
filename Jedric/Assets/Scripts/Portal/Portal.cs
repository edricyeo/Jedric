using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject portalObject;
    private bool inRange;

    private void Awake()
    {
        // portal from boss room to main room
        if (sceneName == "MainRoom")
        {
            BossHealth.BossDeathEvent += OpenPortal;
            portalObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    private void OpenPortal()
    {
        if (portalObject != null)
        {
            portalObject.SetActive(true);
        }
    }

    private void PortPlayer()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            PortPlayer();
        }
    }
}
