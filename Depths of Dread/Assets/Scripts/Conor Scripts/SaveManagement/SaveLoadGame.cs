using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Cinemachine;

[Serializable]
public struct SaveGame
{
    public int AreaNumber;
    public int AnchorPointNumber;
    public bool DashEnabled;
    public bool JetpackEnabled;
    public float CamRotationY;
}

public class SaveLoadGame : MonoBehaviour
{

    public GameObject Player,Top,Bottom;
    private PlayerMovementScript _pms;

    public int CurrentAreaNumber;
    public int CurrentAnchorPointNumber;

    public SaveGame saveGame;
    const string FILE_NAME = "SaveGame.txt";
    string filePath;

    public List<Transform> AreaAnchorPoints;

    public CinemachineVirtualCamera PlayerCam;

    private void Awake()
    {
        filePath = Application.persistentDataPath;
        saveGame = new SaveGame();

        _pms = FindObjectOfType<PlayerMovementScript>();
        Player = _pms.gameObject;
    }

    private void Start()
    {
        LoadGame();
    }

    public void SaveState()
    {
        Debug.Log("Game Saved");
        saveGame.AreaNumber = CurrentAreaNumber;
        saveGame.AnchorPointNumber = CurrentAnchorPointNumber;
        saveGame.DashEnabled = _pms.DashEnabled;
        saveGame.JetpackEnabled = _pms.JetpackEnabled;
        saveGame.CamRotationY = PlayerCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;
        string gameStatusJson = JsonUtility.ToJson(saveGame);
        File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);
    }

    public void LoadGame()
    {
        if (File.Exists(filePath + "/" + FILE_NAME))
        { //load the file content as string
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            //deserialise the loaded string into a GameStatus struct   
            saveGame = JsonUtility.FromJson<SaveGame>(loadedJson);

            if (SceneManager.GetActiveScene().buildIndex != saveGame.AreaNumber)
            {
                SceneManager.LoadScene(saveGame.AreaNumber);
            }

            _pms.DashEnabled = saveGame.DashEnabled;
            _pms.JetpackEnabled = saveGame.JetpackEnabled;
            Player.transform.position = AreaAnchorPoints[saveGame.AnchorPointNumber].position;
            PlayerCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value= saveGame.CamRotationY;
            Bottom.transform.localRotation = Top.transform.rotation;
        }
        else
        {
            saveGame.AreaNumber = CurrentAreaNumber;
            saveGame.AnchorPointNumber = CurrentAnchorPointNumber;
            saveGame.DashEnabled = _pms.DashEnabled;
            saveGame.JetpackEnabled = _pms.JetpackEnabled;
            saveGame.CamRotationY = Top.transform.localRotation.eulerAngles.y;
        }
    }
}
