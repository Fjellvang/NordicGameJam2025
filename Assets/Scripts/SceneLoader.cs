using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset scene;
#endif
    string sceneName;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
#if UNITY_EDITOR
    public void OnAfterDeserialize ( ) => FillScene ( );
    public void OnBeforeSerialize ( ) => FillScene ( );
    public void OnValidate ( ) => FillScene ( );

    private void FillScene ( )
    {
        if ( scene != null )
        {
            sceneName = scene.name;
        }
        else
        {
            sceneName = string.Empty;
        }
    }
#endif
}
