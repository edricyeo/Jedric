using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private bool opened;
    [SerializeField] private GameObject portalClosed;
    [SerializeField] private GameObject portalOpen;

    // Start is called before the first frame update
    void Start()
    {   
        DontDestroyOnLoad(gameObject);
        BossHealth.BossDeathEvent += OpenPortal;
        opened = false;
        portalClosed.SetActive(true);
        portalOpen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && opened == true)
        {
            // player runs to portal and boss is dead, go to next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OpenPortal()
    {
        portalClosed.SetActive(false);
        portalOpen.SetActive(true);
        opened = true;
    }

    
}
