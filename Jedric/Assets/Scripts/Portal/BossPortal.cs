using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private bool inRange;

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

    private void PortToBoss()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            PortToBoss();
        }
    }

}
