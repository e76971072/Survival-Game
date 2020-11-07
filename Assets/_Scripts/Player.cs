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
    private Rigidbody m_RigidBody;

    public static float jumpForce = 50f; 
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
    public void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        song.GetComponent<AudioSource>().Play();
        hp.text = "LIFE: " + health;
    }

    // Update is called once per frame
    public void Update()
    {


        if ( transform.position.y < 0  ) 
        {
            Destroy(gameObject);
            deathCam.SetActive(true);
            ui.SetActive(false);
            diedUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            song.GetComponent<AudioSource>().Stop();

        }
        healthBar.GetComponent<HealthBar>().SetHealth(health / 100);
        hp.text = "LIFE: " + health;
    }
    public void JumpHigher ()
    {
        m_RigidBody.drag = 0f;
        m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
        m_RigidBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }
}
