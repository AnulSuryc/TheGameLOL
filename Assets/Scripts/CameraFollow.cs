using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y+0.5f, player.position.z);
    }
}
