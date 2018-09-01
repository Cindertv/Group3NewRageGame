using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    private string masterVolume = "Master";
    private string sfxVolume = "SFX";
    private string ambientVolume = "Ambient";
    private string musicVolume = "Music";


    private void Update()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
