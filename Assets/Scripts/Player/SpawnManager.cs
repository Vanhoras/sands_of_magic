using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject DefaultPlayer;

    public static SpawnManager SharedInstance { get; private set; }

    public string spawnPosition { private get; set; }

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Transform spawn;
        GameObject playerParent;

        playerParent = GameObject.Find("PlayerParent");


        if (spawnPosition == null)
        {
            spawn = GameObject.Find("DefaultSpawnpoint").transform;
        }
        else
        {
            spawn = GameObject.Find(spawnPosition).transform;
            spawnPosition = null;
            if (spawn == null)
            {
                spawn = GameObject.Find("DefaultSpawnpoint").transform;
            }
        }

        Instantiate(DefaultPlayer, spawn.position, spawn.rotation, playerParent.transform);
    }
}
