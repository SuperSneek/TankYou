using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    Transform transform;

    [SerializeField]
    private float maxTurn = 1;

    [SerializeField]
    private Transform gun;

    private AudioSource audio;


    private Vector2 desiredTurn;
    private Vector2 currentTurn;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
    }

    public void BeginTurnLeft()
    {
        StopAllCoroutines();
        desiredTurn = new Vector2(-maxTurn,0);
        audio.Play();
    }

    public void BeginTurnRight()
    {
        StopAllCoroutines();
        desiredTurn = new Vector2(maxTurn, 0);
        audio.Play();
    }

    public void StopTurn()
    {
        desiredTurn = Vector2.zero;
        StartCoroutine(WaitToStopTurret());
    }

    public void StartTurn(Vector2 input)
    {
        StopAllCoroutines();
        desiredTurn = maxTurn*input;
        audio.Play();
    }

    //Updates the turn factor to a new float
    public void UpdateTurn(Vector2 input) {
        desiredTurn = maxTurn * input;
    }

    private IEnumerator WaitToStopTurret()
    {
        yield return new WaitForSeconds(0.4f);
        audio.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTurn = Vector2.Lerp(desiredTurn, currentTurn, 0.7f);
        
        audio.pitch = Mathf.Abs(currentTurn.magnitude / maxTurn)*2;
        transform.Rotate(new Vector3(0f, 0f, currentTurn.x), Space.Self);
        gun.Rotate(new Vector3(currentTurn.y, 0f, 0f), Space.Self);
        float pitchAngle = gun.localRotation.eulerAngles.x + currentTurn.y;
        if (pitchAngle > 180)
            pitchAngle -= 360f;
        pitchAngle = Mathf.Clamp(pitchAngle, -30, 8);
        gun.localRotation = Quaternion.Euler(pitchAngle, 0f, 0f);

    }
}
