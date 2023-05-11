using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public string TrackName;
    private Movement PlayerMovement;
    public AudioSource Music;
    public int Progress = 0;
    private float FloatingProgress = 0;
    public int HighScore = 0;
    private bool hasCompleted = false;
    private Movement _movement;

    public GameObject Details;
    public float DetailPos;
    public GameObject Progression;
    public float ProgressionPos;

    public TextMeshProUGUI Current;
    public TextMeshProUGUI Best;

    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetInt(TrackName,0);
        PlayerMovement =  GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        _movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCompleted && !_movement.IsDead)
        {
            FloatingProgress = Music.time / Music.clip.length * 100;
            Progress = (int)FloatingProgress;
        }

        if (_movement.HasStarted)
        {
            var step = 400 * Time.deltaTime;
            Details.transform.localPosition = Vector2.MoveTowards(Details.transform.localPosition, new Vector2(DetailPos, Details.transform.localPosition.y), step);

            Progression.transform.localPosition = Vector2.MoveTowards(Progression.transform.localPosition, new Vector2(ProgressionPos, Progression.transform.localPosition.y), step);
        }

        if(Progress > HighScore)
        {
            HighScore = Progress;
            PlayerPrefs.SetInt(TrackName,Progress);
        }

        if (!PlayerMovement.HasStarted)
        {
            if (Input.GetMouseButtonDown(0) || UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
                Music.Play();
                PlayerMovement.HasStarted = true;
            }
        }

        if(Progress == 100)
        {
            hasCompleted = true;
            Progress = 100;
        }

        Current.text = Progress.ToString() + "%";
        Best.text = HighScore.ToString() + "%";
    }
}
