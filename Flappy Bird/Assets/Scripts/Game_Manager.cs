using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public delegate void Timer(float _time);
    public static event Timer OnTime;

    [SerializeField]
    private AudioSource _audioSourceJump;
    [SerializeField]
    private AudioSource _audioSourceDead;
    [SerializeField]
    private AudioSource _audioSourceScore;
    private AudioListener _audioListener;

    public delegate void MoveSpeed(float _movespeed);
    public static event MoveSpeed OnSpeedGain;

    private float movespeed = 1;

    private float time;
    private bool _pause = false;
    private float countdown = 4;

    [SerializeField]
    private GameObject _ui;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _highscoreText;
    [SerializeField]
    private Text _highscoreMenuText;

    [SerializeField]
    private Text _countdown;


    private Vector3 PlayerInitialPosition;
    private float _score = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Pipe_Collision.OnDead += PlayerDead;
        Scoing_collision.OnScore += PlayerScore;
        Player.OnJump += PlayerJump;
    }

    private void OnDisable()
    {
        Pipe_Collision.OnDead -= PlayerDead;
        Scoing_collision.OnScore -= PlayerScore;
        Player.OnJump -= PlayerJump;
    }
    void Start()
    {
        //_audioSourceJump = GetComponent<AudioSource>();
        //_audioSourceDead = GetComponent<AudioSource>();
        //_audioSourceScore = GetComponent<AudioSource>();
        _audioListener = GetComponent<AudioListener>();

        _scoreText.text = _score.ToString();
        _highscoreText.text = "Highscore:" + PlayerPrefs.GetFloat("HighScore:").ToString();
        _highscoreMenuText.text = "Highscore:" + PlayerPrefs.GetFloat("HighScore:").ToString();

        PlayerInitialPosition = transform.position;

        //StartGame();
        _pause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _pause == true)
        {
            
            StartGame();

        }
        if (Input.GetKeyDown(KeyCode.Escape) && _pause == false)
        {
            _pause = !_pause;
            PauseGame();

        }
        if (_pause == true)
        {
            OnPause();
        }
        else
        {
            StartGame();
           
        }
        OnSpeedGain(movespeed);
        movespeed += 0.05f * time;

    }

    private void PlayerJump()
    {
        if(_pause == false)
        {
            _audioSourceJump.Play();

        }
        
    }

    private void OnPause()
    {
        
        time = 0;
        OnTime(time);
    }
    private void OnStart()
    {
        time = Time.deltaTime;
        OnTime(time);
    }

    private void PlayerDead()
    {
        _pause = true;

        _audioSourceDead.Play();
        Debug.Log("dead");
        _score = 0;
        _scoreText.text = "Score:" + _score.ToString();
        PauseGame();
    }

    private void PlayerScore()
    {

        _audioSourceScore.Play();
        _score++;
        _scoreText.text = "Score:" +_score.ToString();

        CheckHighScore();
    }


    public void StartGame()
    {
        _countdown.enabled = true;
        StartCoroutine(StartCountdown());

    }
    private IEnumerator StartCountdown()
    {
        while (countdown > 1)
        {
            countdown--;
            _countdown.text = countdown.ToString();
            yield return new WaitForSeconds(1);
        }
        if( countdown == 1)
        {
            _pause = false;
            _countdown.enabled = false;
             OnStart();
        }
    }
    public void PauseGame()
    {
        _pause = true;
        countdown = 4;
        _ui.SetActive(true);

    }

    private void CheckHighScore()
    {
        if(_score > PlayerPrefs.GetFloat("HighScore:", 0))
        {
            PlayerPrefs.SetFloat("HighScore:", _score);
            _highscoreText.text = "Highscore:" + PlayerPrefs.GetFloat("HighScore:").ToString();
            _highscoreMenuText.text = "Highscore:" + PlayerPrefs.GetFloat("HighScore:").ToString();
        }
    }

}
