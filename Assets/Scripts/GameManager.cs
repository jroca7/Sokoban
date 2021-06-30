using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static int Score = 0;
    public string ScoreString = "Score";

    public Text TextScore;

    public static GameManager Gamemanager;

    public LevelBuilder levelBuilder;
    public GameObject nextButton;
    private bool readyForInput;
    private Player player;

    void Awake()
    {
        Gamemanager = this;
    }
    void Start() {
        nextButton.SetActive(false);
        ResetScene();
        GameManager.Score = 0;
    }

    void Update() {

        if(TextScore != null)
        {
            TextScore.text = ScoreString + Score.ToString();
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();

        if (input.sqrMagnitude > 0.5) {
            if (readyForInput) {
                readyForInput = false;
                player.Move(input);
                nextButton.SetActive(IsLevelComplete());
            }
        }
        else {
            readyForInput = true;
        }
    }

    public void NextLevel() {
        nextButton.SetActive(false);
        levelBuilder.NextLevel();
        GameManager.Score = 0;
        StartCoroutine(ResetSceneAsync());
    }

    public void ResetScene() {
        StartCoroutine(ResetSceneAsync());
    }

    bool IsLevelComplete() {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes) {
            if (!box.onCrox) return false;
        }
        return true;
    }

    IEnumerator ResetSceneAsync() {
        if (SceneManager.sceneCount > 1) {
            AsyncOperation async = SceneManager.UnloadSceneAsync("LevelScene");
            while (!async.isDone) {
                yield return null;
                
            }
            
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
            
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        levelBuilder.Build();
        player = FindObjectOfType<Player>();
        
    }
}
