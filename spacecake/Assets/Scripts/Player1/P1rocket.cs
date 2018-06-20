using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1rocket : MonoBehaviour
{
    public GameObject BackgroundMusic;

    public bool part1 = false;
    public bool part2 = false;
    public bool part3 = false;
    public bool part4 = false;
    public bool part5 = false;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (part1 && part2 && part3 && part4 && part5 && collision.gameObject.tag == "player1")
        {
            BackgroundMusic.GetComponent<MusicManager>().transDefaultRunning = true;
            part1 = false;
            part2 = false;
            part3 = false;
            part4 = false;
            part5 = false;
            print("p1 win");
            SceneManager.LoadScene(2);
        }
    }
}
