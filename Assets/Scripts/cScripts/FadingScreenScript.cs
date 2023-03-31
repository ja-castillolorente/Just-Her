using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FadingScreenScript : MonoBehaviour
{
    GameObject screenInstance;
    public Canvas screenCanvas;
    void Start()
    {
        if (screenInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            screenInstance = gameObject;
            if (GameObject.Find("NewUICamera(Clone)") == true)
            {
                screenCanvas = GameObject.Find(gameObject.name).GetComponent<Canvas>();
                screenCanvas.worldCamera = GameObject.Find("NewUICamera(Clone)").GetComponent<Camera>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
