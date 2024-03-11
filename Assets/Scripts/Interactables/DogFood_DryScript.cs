using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DogFood_DryScript : MonoBehaviour
{
    public bool isInteractable;
    public TextMeshPro amountLeftText;
    public int amountLeft;
    public bool pickUpRange;
    public HandsScript rHandDetect;
    private bool grabbedFood;
    public GameObject scoop;

    void Start()
    {
        isInteractable = false;
        scoop.SetActive(false);

        //Temporary amount since it will be set when restock
        amountLeft = 10;
    }

    void Update()
    {
        if(this.GetComponent<EnableInteractText>().isInteractable)
        {
            isInteractable = true;
        }
        else
        {
            isInteractable = false;
        }

        if(this.GetComponent<EnableInteractText>().canGrab)
        {
            pickUpRange = true;
        }
        else
        {
            pickUpRange = false;
        }

        //Show amount of dry food left
        amountLeftText.text = "Servings: " + amountLeft;
        if (!grabbedFood)
        {
            if (isInteractable)
            {
                //Have option to grab food

                //Enable hand object of food
                //Once have food in hand dog will look at player

                if (pickUpRange && rHandDetect.isDoingGrab)
                {
                    // Hold some food
                    grabbedFood = true;
                    scoop.SetActive(true);
                    print("HOLDING FOOD");
                }
            }
        }
        else if(grabbedFood)
        {
            if(!rHandDetect.isDoingGrab)
            {
                // Drop the food and put it back
                grabbedFood = false;
                scoop.SetActive(false);
                print("OOPS");

            }
        }
    }
}
