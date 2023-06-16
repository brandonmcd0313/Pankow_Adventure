using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle CTE;
    public Toggle MST;
    public Toggle PPA;

    private Toggle previousToggle;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        // Add listeners to each toggle
        CTE.onValueChanged.AddListener(OnToggleChanged);
        MST.onValueChanged.AddListener(OnToggleChanged);
        PPA.onValueChanged.AddListener(OnToggleChanged);

        //turn all off exceot cTE
        CTE.isOn = true;
        MST.isOn = false;
        PPA.isOn = false;
        previousToggle = CTE;
    }

    private void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            // If CTE is toggled on, turn off the other toggles
            if (CTE.isOn && previousToggle != CTE)
            {
                previousToggle.isOn = false;

                previousToggle = CTE;
                MST.isOn = false;
                PPA.isOn = false;
            }
            // If MST is toggled on, turn off the other toggles
            else if (MST.isOn && previousToggle != MST)
            {
               previousToggle.isOn = false;

                previousToggle = MST;
                CTE.isOn = false;
                PPA.isOn = false;
            }
            // If PPA is toggled on, turn off the other toggles
            else if (PPA.isOn && previousToggle != PPA)
            {
                previousToggle.isOn = false;

                previousToggle = PPA;
                CTE.isOn = false;
                MST.isOn = false;
            }
        }
        else
        {
            // Turn back on the toggle that was just turned off
            if (!CTE.isOn && !MST.isOn && !PPA.isOn)
            {
                previousToggle.isOn = true;
            }
        }
    }

    public void StartGame()
    {
        if (CTE.isOn)
        {
            PlayerController.program = "CTE";
        }
        else if (MST.isOn)
        {
            PlayerController.program = "MST";
        }
        else if (PPA.isOn)
        {
            PlayerController.program = "PPA";
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
