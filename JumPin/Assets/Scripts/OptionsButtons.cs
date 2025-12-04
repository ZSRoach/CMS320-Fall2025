using UnityEngine;
using UnityEngine.UI;

public class OptionsButtons : MonoBehaviour
{
    public Toggle toggle;
    [SerializeField]
    public int setting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (setting) {
            case 1:
                toggle.isOn = OptionsMenu.checkpointsOn;
                if (toggle.isOn) OptionsMenu.checkpointsOn = true;
                break;
            case 2:
                toggle.isOn = OptionsMenu.musicOn;
                if (toggle.isOn) OptionsMenu.musicOn = true;
                break;
            case 3:
                toggle.isOn = OptionsMenu.soundsOn;
                if (toggle.isOn) OptionsMenu.soundsOn = true;
                break;
        }

            
        
    }

    // Update is called once per frame
    
}
