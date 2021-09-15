using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadChargeCollider : MonoBehaviour
{
    public HeadCharge headCharge;
    public CreatePylon createPylon;
    public GameObject bluePylon;
    public SoundManager soundManager;



    private void Update()
    {
        transform.position = headCharge.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(headCharge.isInvincible == false)
            {
                Destroy(headCharge.particles[headCharge.nParticles - 1].gameObject);

                headCharge.nParticles--;

                Destroy(other.gameObject);

                soundManager.playAudioClip(2);

                if(headCharge.nParticles == 0)
                {
                    Lose();
                }
            }
            else
            {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        if (other.gameObject.tag == "Limit")
        {
            Destroy(other.gameObject);
            Lose();
        }
    }

    public void Lose()
    {
        soundManager.playAudioClip(3);

        GameObject newPylon;
        newPylon = Instantiate(bluePylon, headCharge.gameObject.transform.position, Quaternion.identity);
        newPylon.GetComponent<Pylon>().headCharge = headCharge;

        for (int i = 0; i < 255; i++)
        {
            if (PlayerPrefs.GetString(createPylon.nLevel.ToString() + "Blue" + i.ToString(), "Error") == "Error")
            {
                PlayerPrefs.SetString(createPylon.nLevel.ToString() + "Blue" + i, newPylon.transform.position.x.ToString() + "x" + newPylon.transform.position.y.ToString());

                StartCoroutine(End());

                return;
            }
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("SelectionLevel", LoadSceneMode.Single);
    }
}
