using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePylon : MonoBehaviour
{
    // =================== VARIABLES ===================

    public HeadCharge headCharge;

    public GameObject bluePylon;
    public GameObject redPylon;
    public GameObject greenPylon;

    public GameObject particleChargePref;

    public int nLevel = 0;

    private int i = 0;

    // =================================================

    public void Create()
    {
        if(headCharge.particles[headCharge.nParticles - 1].color == ParticleCharge.ColorType.Blue)
        {
            GameObject newPylon;
            newPylon = Instantiate(bluePylon, headCharge.gameObject.transform.position, Quaternion.identity);
            newPylon.GetComponent<Pylon>().headCharge = headCharge;

            for( i = 0; i < 255; i++)
            {
                if(PlayerPrefs.GetString(nLevel.ToString() + "Blue" + i.ToString(), "Error") == "Error")
                {
                    PlayerPrefs.SetString(nLevel.ToString() + "Blue" + i, newPylon.transform.position.x.ToString() + "x" + newPylon.transform.position.y.ToString());

                    Destroy(headCharge.particles[headCharge.nParticles - 1].gameObject);

                    headCharge.nParticles = headCharge.nParticles - 1;

                    return;
                }
            }
        }
        else if (headCharge.particles[headCharge.nParticles - 1].color == ParticleCharge.ColorType.Red)
        {
            GameObject newPylon;
            newPylon = Instantiate(redPylon, headCharge.gameObject.transform.position, Quaternion.identity);
            newPylon.GetComponent<Pylon>().headCharge = headCharge;

            for (i = 0; i < 255; i++)
            {
                if (PlayerPrefs.GetString(nLevel.ToString() + "Red" + i.ToString(), "Error") == "Error")
                {
                    PlayerPrefs.SetString(nLevel.ToString() + "Red" + i, newPylon.transform.position.x.ToString() + "x" + newPylon.transform.position.y.ToString());

                    Destroy(headCharge.particles[headCharge.nParticles - 1].gameObject);

                    headCharge.nParticles = headCharge.nParticles - 1;

                    return;
                }
            }
        }
        else if (headCharge.particles[headCharge.nParticles - 1].color == ParticleCharge.ColorType.Green)
        {
            GameObject newPylon;
            newPylon = Instantiate(greenPylon, headCharge.gameObject.transform.position, Quaternion.identity);
            newPylon.GetComponent<Pylon>().headCharge = headCharge;

            for (i = 0; i < 255; i++)
            {
                if (PlayerPrefs.GetString(nLevel.ToString() + "Green" + i.ToString(), "Error") == "Error")
                {
                    PlayerPrefs.SetString(nLevel.ToString() + "Green" + i, newPylon.transform.position.x.ToString() + "x" + newPylon.transform.position.y.ToString());

                    Destroy(headCharge.particles[headCharge.nParticles - 1].gameObject);

                    headCharge.nParticles = headCharge.nParticles - 1;

                    return;
                }
            }
        }
    }

    private void Awake()
    {
        CreatePylonBlue();
        CreatePylonRed();
        CreatePylonGreen();

        if(nLevel != 0)
        {
            SetParticles();
        }
    }

    public void CreatePylonBlue()
    {
        for (i = 0; i < 255; i++)
        {
            if (PlayerPrefs.GetString(nLevel.ToString() + "Blue" + i.ToString(), "Error") != "Error")
            {
                string value = PlayerPrefs.GetString(nLevel.ToString() + "Blue" + i.ToString(), "Error");
                string[] substrings = value.Split('x');
                float xPos = float.Parse(substrings[0]);
                float yPos = float.Parse(substrings[1]);

                GameObject newPylon;
                newPylon = Instantiate(bluePylon, new Vector3(xPos, yPos, 0), Quaternion.identity);
                newPylon.GetComponent<Pylon>().headCharge = headCharge;
            }
            else
            {
                return;
            }
        }
    }

    public void CreatePylonRed()
    {
        for (i = 0; i < 255; i++)
        {
            if (PlayerPrefs.GetString(nLevel.ToString() + "Red" + i.ToString(), "Error") != "Error")
            {
                string value = PlayerPrefs.GetString(nLevel.ToString() + "Red" + i.ToString(), "Error");
                string[] substrings = value.Split('x');
                float xPos = float.Parse(substrings[0]);
                float yPos = float.Parse(substrings[1]);

                GameObject newPylon;
                newPylon = Instantiate(redPylon, new Vector3(xPos, yPos, 0), Quaternion.identity);
                newPylon.GetComponent<Pylon>().headCharge = headCharge;
            }
            else
            {
                return;
            }
        }
    }

    public void CreatePylonGreen()
    {
        for (i = 0; i < 255; i++)
        {
            if (PlayerPrefs.GetString(nLevel.ToString() + "Green" + i.ToString(), "Error") != "Error")
            {
                string value = PlayerPrefs.GetString(nLevel.ToString() + "Green" + i.ToString(), "Error");
                string[] substrings = value.Split('x');
                float xPos = float.Parse(substrings[0]);
                float yPos = float.Parse(substrings[1]);

                GameObject newPylon;
                newPylon = Instantiate(greenPylon, new Vector3(xPos, yPos, 0), Quaternion.identity);
                newPylon.GetComponent<Pylon>().headCharge = headCharge;
            }
            else
            {
                return;
            }
        }
    }

    public void SetParticles()
    {
        string prefName = "ParticleLevel" + nLevel.ToString();
        int limit = PlayerPrefs.GetInt(prefName, 0);

        for(i = 0; i < limit; i++)
        {
            headCharge.nParticles++;
            GameObject ParticleCharge;
            ParticleCharge = Instantiate(particleChargePref, new Vector3(0, 0, 0), Quaternion.identity);
            headCharge.particles[headCharge.nParticles - 1] = ParticleCharge.GetComponent<ParticleCharge>();
        }

        PlayerPrefs.SetInt(prefName, 0);
    }
}
