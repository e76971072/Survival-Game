using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public Text hp;
    public GameObject healthBar;
    public GameObject deathCam;
    public GameObject ui, diedUI;
    public GameObject song;
    public void DamageTaken(float dmg)
    {
        health -= dmg;

        if (health <= 0f)
        {
            Debug.Log("You died!");
            Destroy(gameObject);
            deathCam.SetActive(true);
            //ui.GetComponent<Canvas>().enabled = false;
            //diedUI.GetComponent<Canvas>().enabled = true;
            ui.SetActive(false);
            diedUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            song.GetComponent<AudioSource>().Stop();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        song.GetComponent<AudioSource>().Play();
        hp.text = "LIFE: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<HealthBar>().SetHealth(health / 100);
        hp.text = "LIFE: " + health;
    }
}
