using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    // Este método se llamará cuando el botón sea clicado
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

