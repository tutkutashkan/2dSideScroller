using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public int Point;
    [SerializeField] private Transform target;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, (transform.position.y - 0.8f), transform.position.z);
        RaycastHit2D raycastleft = Physics2D.Raycast(targetPosition, Vector2.left, raycastDistance, player);

        if(raycastleft.collider != null)
        {
            FindObjectOfType<GameManager>().Point = Point;
        }
    }
}
