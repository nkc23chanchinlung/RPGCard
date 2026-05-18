using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
    static public SceneManager Instance;
    GameSceneManager gameSceneManager;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GameSceneExit()
    {
        if (gameSceneManager != null) Destroy(gameSceneManager);
    }
}
