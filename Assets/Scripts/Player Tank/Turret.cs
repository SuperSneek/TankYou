using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    Transform transform;

    [SerializeField]
    private float maxTurn = 1;

    private float desiredTurn;
    private float currentTurn;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void BeginTurnLeft()
    {
        desiredTurn = -maxTurn;
    }

    public void BeginTurnRight()
    {
        desiredTurn = maxTurn;
    }

    public void StopTurn()
    {
        desiredTurn = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTurn = Mathf.Lerp(desiredTurn, currentTurn, 0.5f);
        transform.Rotate(new Vector3(0f, 0f, 1f), currentTurn, Space.Self);
    }
}
