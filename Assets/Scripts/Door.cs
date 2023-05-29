using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        Destroy(other.gameObject);
        SceneManager.LoadSceneAsync(sceneName);
    }

}
