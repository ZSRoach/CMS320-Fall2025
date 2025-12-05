using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (OptionsMenu.checkpointsOn == false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
        }
        else {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
