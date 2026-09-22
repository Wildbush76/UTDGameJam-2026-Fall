using UnityEngine;

public class ParkingCheck : MonoBehaviour
{
    [SerializeField] private short pointsInSpot = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
    {
        pointsInSpot++; //Adds new colider to amount in spot

        //Car includes 3 coliders: Body, TL corner, BR corner
        if(pointsInSpot == 3)
        {
            Debug.Log("You Parked!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pointsInSpot--; // Removes exiting colider from total

        if (pointsInSpot < 0)
        {
            pointsInSpot = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
