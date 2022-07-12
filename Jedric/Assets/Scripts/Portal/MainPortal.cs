using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPortal : MonoBehaviour
{
    private bool inRange;
    [SerializeField] private GameObject portal;

    private void Awake()
    {
        BossHealth.BossDeathEvent += OpenPortal;
        portal.SetActive(false);
    }

    private void OpenPortal()
    {
        if (portal != null)
        {
            portal.SetActive(true);
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

    private void PortToMain()
    {
        SceneManager.LoadScene("MainRoom");
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            PortToMain();
        }
    }

}
