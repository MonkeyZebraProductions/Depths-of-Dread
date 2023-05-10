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
    public int AnchorPointNumber;
    public bool DashEnabled;
    public bool GrappleEnabled;
    public float CurrentArmour;
    public float CurrentAir;
    public float CurrentMaxArmour;
    public float CurrentMaxAir;
}

public class SaveLoadGame : MonoBehaviour
{

    public GameObject Player;
    private PlayerMovementScript _pms;
    private AirArmour _aA;
    private GrappleSystem _gH;

    public int CurrentAnchorPointNumber;

    public SaveGame saveGame;
    const string FILE_NAME = "SaveGame.txt";
    string filePath;

    public List<Transform> AreaAnchorPoints;

    public bool StartPoint;

    public float BaseAir,BaseArmour;



    private void Awake()
    {
        filePath = Application.persistentDataPath;
        saveGame = new SaveGame();

        _pms = FindObjectOfType<PlayerMovementScript>();
        _aA = FindObjectOfType<AirArmour>();
        _gH = FindObjectOfType<GrappleSystem>();
        Player = _pms.gameObject;
    }

    private void Start()
    {
        LoadGame();
    }

    public void SaveState()
    {
        Debug.Log("Game Saved");
        saveGame.AnchorPointNumber = CurrentAnchorPointNumber;
        saveGame.DashEnabled = _pms.DashEnabled;
        saveGame.GrappleEnabled = _gH.enabled;
        saveGame.CurrentAir = _aA.air;
        saveGame.CurrentArmour = _aA.damage;
        saveGame.CurrentMaxAir = _aA.MaxAir;
        saveGame.CurrentMaxArmour = _aA.AirDecreaceRate;
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

            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                SceneManager.LoadScene(1);
            }

            _pms.DashEnabled = saveGame.DashEnabled;
            _aA.damage = saveGame.CurrentArmour;
            _aA.air = saveGame.CurrentAir;
            _aA.MaxAir = saveGame.CurrentMaxAir;
            _aA.AirDecreaceRate = saveGame.CurrentMaxArmour;
            _gH.enabled=saveGame.GrappleEnabled;
            //Player.transform.position = AreaAnchorPoints[saveGame.AnchorPointNumber].position;
        }
        else
        {
            saveGame.AnchorPointNumber = CurrentAnchorPointNumber;
            saveGame.DashEnabled = _pms.DashEnabled;
            _aA.air = _aA.MaxAir;
            _aA.damage = _aA.BaseDamageMultiplier;
            _gH.enabled=false;
        }
        Player.transform.position = AreaAnchorPoints[saveGame.AnchorPointNumber].position;
    }
}
