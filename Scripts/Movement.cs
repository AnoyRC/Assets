using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool HasStarted = false;
    public float speed = 1f;
    public bool IsDead = false;
    public bool TurnHandler = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasStarted) return;
        if (IsDead) return;
        if (IsGrounded(0.6f))
        {
            if (Input.GetMouseButtonDown(0) || UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(TurnHandler)
                {
                    gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                TurnHandler = !TurnHandler;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!HasStarted) return;
        if (IsDead) return;
        gameObject.transform.Translate(-Vector3.forward * speed);
    }

    public bool IsGrounded(float DistFromGround)
    {
        return Physics.Raycast(gameObject.transform.position, Vector3.down, DistFromGround);
    }
}
