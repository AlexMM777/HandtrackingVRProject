using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DogNPC : MonoBehaviour
{
    public float hunger_Level, thirst_Level, clean_level, timer;
    public GameObject dogHead, dogNeck, foodBowl, waterBowl, player;
    public Transform foodLoc, waterLoc;

    public NavMeshAgent agent;
    public float range; // radius of sphere

    public Image hungerBar;

    //public bool lowerFood;
    public bool recentlyFed;

    public bool isWalking, isLayingDown, isHungry, isEating, tempGiveMeal;

    void Start()
    {
        hunger_Level = 100f;
        //hunger_Level = 40f;
        //GiveTreat();
        //GiveMeal();
        //recentlyFed = true;

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Detect all levels
        transform.LookAt(foodBowl.transform);

        if (hunger_Level < 30f)
        {
            isHungry = true;
            isWalking = false;
            // Dog goes to dog bowl and lays down/stares/nudges it
            print("FOOOOOOOOD");
            GoToTarget(foodLoc);

            // if (player is not near){
            HeadLookAt(foodBowl);
            NeckLookAt(foodBowl);
            BodyLookAt(foodBowl.transform);
        }

        // Lower food hunger over time
        if (hunger_Level > 0)
        {
            if (!isEating)
            {
                if (recentlyFed)
                {
                    timer += Time.deltaTime;
                    if (timer >= 10) // Have 10 second rest time
                    {
                        timer = 0;
                        recentlyFed = false;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                    if (timer >= 5f) // Lowers every 5 seconds
                    {
                        hunger_Level -= 10;
                        hungerBar.fillAmount = hunger_Level / 100f;
                        timer = 0;
                    }
                }
            }
            //print(timer);
        }
        // Fix food increase cus decreases while still filling up

        #region Idle Actions 
        // Things the dog will do while not interacting with the player
        if (!isHungry)
        {
            if (agent.remainingDistance <= agent.stoppingDistance && isWalking) //done with path
            {
                Vector3 point;
                if (RandomPoint(agent.transform.position, range, out point)) //pass in our centre point and radius of area
                {
                    int choice = 0;
                    choice = Random.Range(1, 5); // 1 to 2
                    if (choice == 1)
                    {
                        // Walk to random location
                        print("isWalking");
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                    else if (choice == 2)
                    {
                        // Sit or lay down for a random period of time
                        isWalking = false;
                        StartCoroutine(SitLayDownForPeriodOfTime());
                    }
                    else
                    {
                        print("idle");
                    }
                }
            }
        }
        #endregion

        if (tempGiveMeal)
        {
            GiveMeal();
        }
    }

    public void GiveMeal()
    {
        // Fill up dog bowl
        // Have dog go to bowl
        // Play eating animation
        // Raise hunger level over time
        // Once full, dog stop eating

        tempGiveMeal = false;
        isEating = true;
        isHungry = false;

        HeadLookAt(foodBowl);
        NeckLookAt(foodBowl);
        BodyLookAt(foodBowl.transform);
        GoToTarget(foodLoc);
        StartCoroutine(RaiseFoodLevel());
    }

    public void GiveTreat()
    {
        if (hunger_Level < 100)
        {
            hunger_Level += 5f;
            if (hunger_Level > 100)
            {
                hunger_Level = 100;
            }
        }
    }

    public void GiveWater()
    {
        // Fill up dog bowl
        // Check if dog is thirsty
        // If yes, dog will go to bowl and drink
        // Will not finish bowl if not that thirsty
        // Randomly will go and drink water

    }

    public void HeadLookAt(GameObject target)
    {
        dogHead.GetComponent<PersonLookAtTarget>().target = target.transform;
    }

    public void NeckLookAt(GameObject target)
    {
        dogNeck.GetComponent<PersonLookAtTarget>().target = target.transform;
    }

    public void BodyLookAt(Transform target)
    {
        transform.LookAt(target);
    }

    public void GoToTarget(Transform target)
    {
        GetComponent<NavMeshAgent>().destination = target.position;
    }

    IEnumerator RaiseFoodLevel()
    {
        while (hunger_Level < 100f)
        {
            hunger_Level += Time.deltaTime * 3f; // speed
            hunger_Level = Mathf.Clamp(hunger_Level, 0f, 100f);
            print(hunger_Level);
            hungerBar.fillAmount = hunger_Level / 100f;
            yield return null;
        }
        recentlyFed = true;
        // Stop eating animation
        // Leave bowl
        isWalking = true;
        isEating = false;
    }

    #region Idle Actions Methods

    IEnumerator SitLayDownForPeriodOfTime()
    {
        int choice = 0;
        choice = Random.Range(1, 3);

        if(choice == 1)
        {
            // Sit
            print("isSitting");
        }
        else if(choice == 2) 
        {
            // Laydown
            print("isLaying");
        }
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        isWalking = true;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    #endregion
}
