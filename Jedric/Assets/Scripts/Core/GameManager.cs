using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;
  [SerializeField] private int sceneLimit;
  [SerializeField] private Transform playerPrefab;
  [SerializeField] private Transform spawnPoint;

  void Awake()
  {
      if (instance == null)
      {
          instance = this;
      }
      else if (instance != this)
      {
         Destroy(gameObject);
      }

      //Sets this to not be destroyed when reloading scene
      DontDestroyOnLoad(gameObject);

  }

  public void RespawnPlayer() 
  {
    Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
  }

  void Update()
  {
    //Checks if build index is greater than ("#") 
    if (SceneManager.GetActiveScene().buildIndex > sceneLimit)
    {
      Destroy(GameObject.FindWithTag("GameManager"));
    }
  }
}
