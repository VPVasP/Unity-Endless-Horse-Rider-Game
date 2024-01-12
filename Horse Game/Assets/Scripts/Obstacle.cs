using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float certainDistance;
    public LayerMask horseMask; //our horse layermask
    private GameObject horse;//refrence to our horse
    private bool hasNumberBeenAdded = false;
    //we assign the layer and we find the object with tag horse and the certain distance
    private void Start()
    {
        horseMask = 1 << LayerMask.NameToLayer("Horse");
        horse = GameObject.FindGameObjectWithTag("Horse");
        certainDistance = 3;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Horse"))
        {
            //we trigger the horse death and we call the lost panel function from the game manager
            collision.gameObject.GetComponent<HorseController>().Dead();
            collision.gameObject.GetComponent<HorseController>().enabled = false;
            GameManager.instance.LostPanel();
        }
    }
    private void Update()
    {
        float distanceToHorse = Vector3.Distance(transform.position, horse.transform.position);
        // Debug.Log(distanceToHorse);
        //we check if the obstacle is within a certain distance to the horse
        if (distanceToHorse < certainDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up, out hit, certainDistance, horseMask))
            {
                //we check if the number has not been added yet
                if (!hasNumberBeenAdded)
                {
                    //we play the cheer sound and add the curentobstacle number 
                    GameManager.instance.PlayCheerSound();
                    ObstacleManager.instance.currentObstacleNumber += 0.5f;

                    ObstacleManager.instance.obstaclesPassedText.text = "Obstacles ";
                    ObstacleManager.instance.obstaclesPassedText.text += ObstacleManager.instance.currentObstacleNumber.ToString();
                    hasNumberBeenAdded = true;
                    Destroy(gameObject, 0.5f);
                }
                
            } //if the horse is not above it we stop the cheer sound
            else if(distanceToHorse > certainDistance)
            {
                GameManager.instance.StopCheerSound();
            }

        }

    }
}
