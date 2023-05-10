using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Movement PlayerMovement;
    public AudioSource Music;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement =  GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerMovement.HasStarted)
        {
            if (Input.GetMouseButtonDown(0) || UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
                Music.Play();
                PlayerMovement.HasStarted = true;
            }
        }
    }
}
