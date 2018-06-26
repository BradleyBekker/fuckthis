using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercam : MonoBehaviour {

    public GameObject player;
     float xMin = -19.59f;
     float xMax = 13.59f;
     float yMin = -28;
     float yMax = 6;
    void LateUpdate()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

        transform.position = new Vector3(x, y, transform.position.z);
            }
}
