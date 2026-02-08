using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneEnd : MonoBehaviour
{
    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
