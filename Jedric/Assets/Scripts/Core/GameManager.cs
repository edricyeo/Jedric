using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
  public static GameManager instance = null;
  public int progressLevel;
  [SerializeField] private Transform playerPrefab;
  [SerializeField] private Transform spawnPoint;

  private void Awake()
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

  //public void RespawnPlayer() 
  //{
  //  Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
  //}

    public void LoadData(GameData data)
    {
        this.progressLevel = data.progressLevel;
    }

    public void SaveData(GameData data)
    {
        data.progressLevel = this.progressLevel;
    }
}
