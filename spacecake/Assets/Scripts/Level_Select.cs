using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Select : MonoBehaviour {
    [SerializeField]
    Image controlimage;
    [SerializeField]
    Image introimage;
    bool timed = false;
    float timer = 0;
    void Start () {
        GameObject.Find("start_button").GetComponentInChildren<Text>().text = "";
    }

  public void Onclick()
    {

        introimage.enabled = true;


    }
    private void Update()
    {

            if(Input.GetMouseButtonDown(0) && introimage.enabled)
            {
                
                introimage.enabled = false;
                controlimage.enabled = true;
                timed = true;
            }



        if (Input.GetMouseButtonDown(0) && controlimage.enabled && timer > 4)
        {
            SceneManager.LoadScene("ExpandingSpace");
        }
        if (timed)
        {
            timer++;
        }
    }
}
