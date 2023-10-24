using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.InputSystem.Controls;
using System.Linq;

public class UtilityFunctions : MonoBehaviour
{
    public static UtilityFunctions Instance;
    [SerializeField] GameObject TempToTurnOff;



    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

        TempToTurnOff.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    
    
}
