using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadCharge : MonoBehaviour
{
    private const float DELAYEFFECTOR = 7f;

    // ================= VARIABLES =================

    public Pylon firstPylon;
    public Pylon actualPylon;
    public Pylon nextPylonToMove;

    public float speed = 3.5f;

    public bool isOnMovement = false;

    public int nParticles = 3;
    public ParticleCharge[] particles;
    public GameObject particlePref;

    public bool isInvincible = false;

    public SoundManager soundManager;

    // =============================================


    private void Start()
    {
        transform.position = firstPylon.transform.position;

        SetNParticles();

        for (int i = 0; i < nParticles; i++)
        {
            particles[i].transform.position = firstPylon.transform.position;
        }
    }

    public void Move()
    {
        StartCoroutine(MoveHeadToNextPylon());

        for (int i = 0; i < nParticles; i++)
        {
            StartCoroutine(MoveToNextPylon(particles[i].transform, i));
        }

        soundManager.playAudioClip(0);
    }

    IEnumerator MoveHeadToNextPylon()
    {
        Vector2 target = nextPylonToMove.transform.position;

        isOnMovement = true;

        //while the distance between the player and the target is greater than 0.01f, move towards it. 
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return null;
        }

        actualPylon = nextPylonToMove;
        isOnMovement = false;

        if (nextPylonToMove.isLast == true)
        {
            End();
        }

        if (actualPylon.color == Pylon.ColorType.Red)
        {
            AddNewParticle();
        }

        if (actualPylon.color == Pylon.ColorType.Green)
        {
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }
    }

    IEnumerator MoveToNextPylon(Transform _particle, int _delay)
    {
        yield return new WaitForSeconds(_delay / DELAYEFFECTOR);

        Vector2 target = nextPylonToMove.transform.position;

        //while the distance between the player and the target is greater than 0.01f, move towards it. 
        while (particles[_delay] != null && Vector2.Distance(_particle.position, target) > 0.01f)
        {
            _particle.position = Vector2.MoveTowards(_particle.position, target, speed * Time.deltaTime);

            yield return null;
        }
    }

    // Set the number of particles with the number of particles of the last level
    private void SetNParticles()
    {

    }

    private void AddNewParticle()
    {
        GameObject newParticle;
        newParticle = Instantiate(particlePref, particles[nParticles - 1].transform.position, Quaternion.identity);

        particles[nParticles] = newParticle.GetComponent<ParticleCharge>();

        int choosenColor = 0;
        choosenColor = UnityEngine.Random.Range(0, 100);

        if(choosenColor < 70)
        {
            newParticle.GetComponent<ParticleCharge>().color = ParticleCharge.ColorType.Blue;
            newParticle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        }
        else if (choosenColor < 90)
        {
            newParticle.GetComponent<ParticleCharge>().color = ParticleCharge.ColorType.Red;
            newParticle.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            newParticle.GetComponent<ParticleCharge>().color = ParticleCharge.ColorType.Green;
            newParticle.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        }

        nParticles++;

        StartCoroutine(MoveToNextPylon(newParticle.transform, 1));

        soundManager.playAudioClip(1);
    }

    public void End()
    {
        if (PlayerPrefs.GetInt("MaxLevel", 0) == GameObject.Find("GameManager").GetComponent<CreatePylon>().nLevel)
        {
            PlayerPrefs.SetInt("MaxLevel", (PlayerPrefs.GetInt("MaxLevel", 0) + 1));
        }

        // On rajoute le nombre de particules restantes au nombre de particules du prochain niveau
        string prefName = "ParticleLevel" + (GameObject.Find("GameManager").GetComponent<CreatePylon>().nLevel + 1).ToString();
        PlayerPrefs.SetInt(prefName, (PlayerPrefs.GetInt(prefName, 0) + nParticles));

        SceneManager.LoadScene("SelectionLevel", LoadSceneMode.Single);
    }
}
