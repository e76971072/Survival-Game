using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySwitching : MonoBehaviour
{
    public int invSelect = 0;
    //public Collider currItem;
    // Start is called before the first frame update
    void Start()
    {
        SelectInventory();
        if (gameObject.name.Equals("HeavyPowerUp") || gameObject.name.Equals("HeavyPowerUp(Clone)"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (gameObject.name.Equals("PistolPowerUp") || gameObject.name.Equals("PistolPowerUp(Clone)"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.name.Equals("HeavyPowerUp") || gameObject.name.Equals("HeavyPowerUp(Clone)"))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (gameObject.name.Equals("PistolPowerUp") || gameObject.name.Equals("PistolPowerUp(Clone)"))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (gameObject.name.Equals("Bottle_Health") || gameObject.name.Equals("Bottle_Health(Clone)"))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }

        int previousInventoryItem = invSelect;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (invSelect >= transform.childCount - 1)
                {
                    invSelect = 0;
                }
                else
                {
                    invSelect++;
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (invSelect <= 0)
                {
                    invSelect = transform.childCount - 1;
                }
                else
                {
                    invSelect--;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                invSelect = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                invSelect = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            {
                invSelect = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
            {
                invSelect = 3;
            }



            if (previousInventoryItem != invSelect)
            {
                SelectInventory();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (gameObject.name.Equals("HeavyPowerUp") || gameObject.name.Equals("HeavyPowerUp(Clone)"))
            {
                other.gameObject.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(8).gameObject.SetActive(true);
                Destroy(gameObject);
            }

            if (gameObject.name.Equals("PistolPowerUp") || gameObject.name.Equals("PistolPowerUp(Clone)"))
            {
                other.gameObject.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(9).gameObject.SetActive(true);
                Destroy(gameObject);
            }

            if (gameObject.name.Equals("Bottle_Health") || gameObject.name.Equals("Bottle_Health(Clone)"))
            {
                other.GetComponent<Player>().health = 100f;
                Destroy(gameObject);
            }
            if (gameObject.name.Equals("Bottle_Mana") || gameObject.name.Equals("Bottle_Mana(Clone)"))
            {
                UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController.JumpForce = 60f;
                Debug.Log(UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController.JumpForce); 
                Destroy(gameObject);
            }
    }

        void SelectInventory()
        {
            int index = 0;
            foreach (Transform inventory in transform)
            {
                if (index == invSelect)
                {
                    inventory.gameObject.SetActive(true);

                }
                else
                {
                    inventory.gameObject.SetActive(false);
                }

                index++;
            }
        }
}
