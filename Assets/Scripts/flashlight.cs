using UnityEngine;

public class flashlight : MonoBehaviour
{
    public GameObject light;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
