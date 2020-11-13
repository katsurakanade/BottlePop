using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    
    //public Canvas ClearCanvas;
    public Canvas PauseCanvas;
    public GameObject firstselect;

    private GameObject LL;

    public static GameManager instance;
    public PlayerController playerController;
    public PlayerCollision playerCollision;
    public PlayerStatus playerStatus;
    public ClearMovie clearmovie;
    public BarController barController;
    public SoundManger soundManger;
    public HUD hud;
    public GameObject Player;
    public Scoring scoring;

    public bool GameIsClear;

    public float sceneStartTime;
    public float sceneClearTime;
    public float medianTime = 60;

    [SerializeField]
    private GameObject sceneManager = null;

    private void Awake()
    {
        instance = this;

        if (SceneController.Instance == null)
        {
            Instantiate(sceneManager);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;

        //ClearCanvas = GameObject.Find("ClearCanvas").GetComponent<Canvas>();
        PauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();

        LL = GameObject.Find("LevelLoader");
        LL.transform.GetChild(0).gameObject.SetActive(true);

        GameIsClear = false;
        sceneStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Controller_Menu"))
        {
            if (!GameIsClear)
            {
                GamePause();
            }
        }

        if (GameIsClear && !GetComponent<VideoPlayer>().isPlaying)
        {
            Invoke("PlayFireWorks", 0.2f);
        }
    }

    public void GameClear()
    {
        SceneController.Instance.lastClearedScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LL.GetComponent<LevelLoader>().LoadLevel(1));
    }

    public void GamePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0f;
            PauseCanvas.enabled = true;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            PauseCanvas.enabled = false;

        }

        firstselect = GameObject.Find("continue");

        SelectObject(firstselect);

    }

    public void SelectObject(GameObject Select)
    {
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();

        eventSystem.enabled = false;

        // Animation

        eventSystem.enabled = true;

        eventSystem.SetSelectedGameObject(Select);
    }

    void PlayFireWorks()
    {
        Vector3 position = Player.transform.position;
        Quaternion rotation = Player.transform.rotation;
        GameObject FireWorks = Resources.Load("Effect/FireWorks") as GameObject;
        Instantiate(FireWorks, position + new Vector3(20, -30, 0), rotation);
        Instantiate(FireWorks, position + new Vector3(-20, -30, 0), rotation);
        Instantiate(FireWorks, position + new Vector3(40, -20, 0), rotation);
        Instantiate(FireWorks, position + new Vector3(-40, -20, 0), rotation);
        
        if (!soundManger.GetComponent<AudioSource>().isPlaying)
        {
            soundManger.PlaySE(soundManger.Fireworks);
        }
        
        CancelInvoke();
    }
}
