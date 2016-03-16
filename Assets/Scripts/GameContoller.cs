using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameContoller : MonoBehaviour {
    [SerializeField]
    private Recogniser recogniser;

    [Header("UI")]
    [SerializeField]
    private Image UITargetImage;
    [SerializeField]
    private Text Score;
    [SerializeField]
    private GameObject GameOverWindow;
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private Texture barTex;

    [Header("Scenario")]
    [SerializeField]
    private TargetImage[] targets;


    private float barProgress;
    private float time_to_end = 12f;
    private const float minTime = 1f;
    
    private int currentRound = 1;
    private int playerScore = 0;
    private bool gameIsActive = false;


	void Start () 
    {
        recogniser.Recognised += OnRecognised;
	
	}
	
	void Update () 
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
  
            

        TimerTick();
	
	}

    void OnRecognised(string symbol)
    {
        if (targets[currentRound - 1].TargetSymbolName == symbol)
            EndRound();
    }

    void EndRound()
    {
        playerScore++;
        currentRound++;
        if (currentRound - 1 >= targets.Length - 1)
            currentRound = 1;
        StartNewRound();
    }

    void StartNewRound()
    {
        UITargetImage.sprite = targets[currentRound - 1].TargetSymbolImage;
        ResetTimer();
    }

    void OnGUI()
    {
        if(gameIsActive)
        {
            GUI.Box(new Rect(Screen.width / 2 - 250 / 2, Screen.height - 70, 250, 50), "Time to lose");
            GUI.DrawTexture(new Rect(Screen.width / 2 - 230 / 2, Screen.height - 40, 230 * barProgress, 10), barTex, ScaleMode.StretchToFill, true, 10.0F);
        }
        
    }

    void TimerTick()
    {
        float rate = 1 / time_to_end;
        
        if (barProgress < 1)
        {
            barProgress += Time.deltaTime * rate;
            if (barProgress >= 1)
            {
                EndGame();
                ResetTimer();
            }
        }
    }

    void ResetTimer()
    {
        barProgress = 0;
        time_to_end -= 1f;
        if (time_to_end <= minTime)
            time_to_end = minTime;
    }

    public void StartGame()
    {
        recogniser.ActivateRecogniser();
        StartButton.SetActive(false);
        StartNewRound();
        gameIsActive = true;
    }

    void EndGame()
    {
        gameIsActive = false;
        recogniser.isActive = false;
        GameOverWindow.SetActive(true);
        Score.text = playerScore.ToString();
    }

    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
