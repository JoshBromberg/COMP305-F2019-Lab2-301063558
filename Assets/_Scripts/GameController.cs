using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;

public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject cloud;
    public GameObject island;
    public int numberOfClouds;
    public List<GameObject> clouds;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;

    public ScoreBoard scoreBoard;

    //public HighScoreSO highScoreSO;

    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    [Header("Scene Settings")]
    public SceneSettings activeSceneSettings;
    public List<SceneSettings> sceneSettings;

    // public properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            scoreBoard.lives = value;
            if (_lives < 1)
            {
                
                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }
           
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            scoreBoard.score = value;


            if (scoreBoard.highScore < _score)
            //if (highScoreSO.score < _score)
            {
                scoreBoard.highScore = _score;
                //highScoreSO.score = _score;
            }
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {

        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");

        //scoreBoard = Resources.FindObjectsOfTypeAll<ScoreBoard>()[0] as ScoreBoard;
    }


    private void SceneConfiguration()
    {
        Scene sceneToCompare = (Scene)Enum.Parse(typeof(Scene), value: SceneManager.GetActiveScene().name.ToUpper());

        var query = from settings in sceneSettings where settings.scene == sceneToCompare select settings;
        activeSceneSettings = query.ToList().FirstOrDefault();
        {
            scoreLabel.enabled = activeSceneSettings.scoreLabelEnabled;
            livesLabel.enabled = activeSceneSettings.livesLabelEnabled;
            highScoreLabel.enabled = activeSceneSettings.highScoreLabelEnabled;
            endLabel.SetActive(activeSceneSettings.endLabelActive);
            restartButton.SetActive(activeSceneSettings.restartButtonActive);
            activeSoundClip = activeSceneSettings.activeSoundClip;
            startLabel.SetActive(activeSceneSettings.startLabelActive);
            startButton.SetActive(activeSceneSettings.startButtonActive);
            livesLabel.text = "Lives: " + scoreBoard.lives;
            scoreLabel.text = "Score: "+scoreBoard.score;
            highScoreLabel.text = "High Score: " + scoreBoard.highScore;
        }

        if (activeSceneSettings.scene == Scene.MAIN) {
            Lives = 5;
            Score = 0;
        }


        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }



        // creates an empty container (list) of type GameObject
        clouds = new List<GameObject>();

        for (int cloudNum = 0; cloudNum < numberOfClouds; cloudNum++)
        {
            clouds.Add(Instantiate(cloud));
        }

        Instantiate(island);
    }

    // Event Handlers
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
