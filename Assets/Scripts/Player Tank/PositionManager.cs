using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    


    public void SwitchToSeat(Transform newSocket)
    {
        player.transform.SetParent(newSocket, true);
        player.transform.localPosition = Vector3.zero;
    }
}
