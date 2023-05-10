using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class RestartScene : MonoBehaviour
{
    // Start is called before the first frame update
    public SaveLoadGame SLG;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.gKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(1);
            SLG.LoadGame();
        }
    }
}
