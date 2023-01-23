using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialTextSwap : MonoBehaviour
{
    public TextMeshProUGUI TutorialText;
    public string Action, ActionPrompt,DefaultString;
    public bool ForceDefaultPC, ForceDefaultController;

    private string buttonPrompt;

    public PlayerInput PI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TutorialText.text = ActionPrompt + buttonPrompt;
        UpdateUI();
    }

    void UpdateUI()
    {
        
        if (PI.currentControlScheme=="Keyboard&Mouse" && ForceDefaultPC)
        {
            buttonPrompt = DefaultString;
        }
        else if (PI.currentControlScheme == "Gamepad" && ForceDefaultController)
        {
            buttonPrompt = DefaultString;
        }
        else
        {
            buttonPrompt = PI.actions[Action].GetBindingDisplayString();
        }

    }
}
