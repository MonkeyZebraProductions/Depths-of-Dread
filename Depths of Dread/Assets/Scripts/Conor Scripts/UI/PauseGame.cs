using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PauseGame : MonoBehaviour
{
    public PlayerInput PlayerInput;
    private InputAction PauseButton;
    public GameObject PauseCanvas,PostProcessing;
    private CinemachineBrain cMB;
    public bool _isPasued, TitleScreem;
    private UnderWaterEffect uWE;
    private WeaponSwitching wS;
    public AudioSource StageOneTheme;

    public List<Canvas> ToggleCanvas;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if(!TitleScreem)
        {
            PauseButton = PlayerInput.actions["Pause"];
            PauseButton.performed += _ => Pause();
        }
        
        cMB = FindObjectOfType<CinemachineBrain>();
        uWE = FindObjectOfType<UnderWaterEffect>();
        wS = FindObjectOfType<WeaponSwitching>();
    }

    private void OnEnable()
    {
        PauseButton.Enable();
    }

    private void OnDisable()
    {
        PauseButton.Disable();
    }

    public void Pause()
    {
        _isPasued = !_isPasued;
        if(_isPasued)
        {
            foreach (Canvas canvas in ToggleCanvas)
            {
                canvas.enabled = false;
            }
            //PlayerInput.SwitchCurrentActionMap("UI");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            cMB.enabled = false;
            uWE.enabled = false;
            wS.enabled = false;
            PostProcessing.SetActive(false);
            PauseCanvas.SetActive(true);
            StageOneTheme.Pause();

        }
        else
        {
            //PlayerInput.SwitchCurrentActionMap("Player");
            foreach (Canvas canvas in ToggleCanvas)
            {
                canvas.enabled = true;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            cMB.enabled = true;
            uWE.enabled = true;
            wS.enabled = true;
            PostProcessing.SetActive(true);
            PauseCanvas.SetActive(false);
            StageOneTheme.Play();
            
        }
    }
}
