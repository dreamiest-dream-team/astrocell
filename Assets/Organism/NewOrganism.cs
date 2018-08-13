using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewOrganism : MonoBehaviour
{
    public void LoadPreset(int preset)
    {
        Organism.preset = preset;
        Organism.eventDifficulty = preset;

        switch(preset)
        {
            case 0:
                Organism.money = 20000;
                break;
            case 1:
                Organism.money = 40000;
                break;
            case 2:
                Organism.money = 60000;
                break;
        }

        SceneManager.LoadScene(1);
    }
}
