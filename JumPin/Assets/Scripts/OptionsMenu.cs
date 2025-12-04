using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionsMenu : MonoBehaviour
{
    public static bool checkpointsOn = false;
    public static bool musicOn = true;
    public static bool soundsOn = true;


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
