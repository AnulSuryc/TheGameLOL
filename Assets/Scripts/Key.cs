using UnityEngine;

public class Key : MonoBehaviour
{

    public GameObject sphere;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sphere.gameObject.SetActive(false);

        }
    }
}
