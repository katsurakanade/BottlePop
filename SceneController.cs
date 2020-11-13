using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static int FirstStageIndex = 2;
    public static int LastStageIndex = 13;

    public static SceneController Instance { get; private set; }
    public int lastClearedScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
