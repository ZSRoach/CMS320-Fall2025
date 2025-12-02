using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionsMenu
{
    public bool checkpointsOn = false;
    public bool musicOn = true;
    public bool soundsOn = true;
    public void showOptions() {
        SceneManager.LoadScene("Options");
    }

    public void showMainMenu() {
        SceneManager.LoadScene("Start Screen");    
    }

    public void toggleMusic() {
        musicOn = !musicOn;
    }
    public void toggleSounds()
    {
        soundsOn = !soundsOn;
    }
    public void toggleCheckpoints()
    {
        checkpointsOn = !checkpointsOn;
    }
}
