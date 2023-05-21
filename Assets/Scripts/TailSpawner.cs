using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSpawner : MonoBehaviour
{
    private GameObject Clone;
    private Movement _movement;
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        _renderer = gameObject.GetComponent<MeshRenderer>();
        SpawnClone();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_movement.HasStarted) return;
        if (_movement.IsDead) return;
        if (!_movement.IsGrounded(0.6f)) return;
        if (Input.GetMouseButtonDown(0) || UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
        {
            SpawnClone(); 
        }
    }

    private void FixedUpdate()
    {
        if (!_movement.HasStarted) return;
        if (_movement.IsDead) return;
        if (!_movement.IsGrounded(0.6f)) return;
        if (_movement.TurnHandler)
        {
            Clone.transform.Translate(-Vector3.forward * _movement.speed / 2);
            Clone.transform.localScale += new Vector3(0f, 0f, _movement.speed);
        }
        else
        {
            Clone.transform.Translate(-Vector3.right * _movement.speed / 2);
            Clone.transform.localScale += new Vector3(_movement.speed, 0f, 0f);
        }

    }

    void SpawnClone()
    {
        Clone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Clone.transform.position = gameObject.transform.position;
        Clone.GetComponent<BoxCollider>().isTrigger = true;
        Clone.GetComponent<MeshRenderer>().material = _renderer.material;
        Clone.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            SpawnClone();
        }
    }
}
