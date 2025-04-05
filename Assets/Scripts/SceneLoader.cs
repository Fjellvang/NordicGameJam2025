using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneAsset scene;
    public void Play()
    {
        SceneManager.LoadScene(scene.name);
    }
}
