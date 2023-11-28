using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask boundary;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask underMap;
    [SerializeField] private bool checkGround;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        if (checkGround){
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

        // Check for collisions with the environment using a raycast
        
        RaycastHit2D raycastright = Physics2D.Raycast(targetPosition, Vector2.right, raycastDistance +5, boundary);
        RaycastHit2D raycastleft = Physics2D.Raycast(targetPosition, Vector2.left, raycastDistance+5, boundary);
        RaycastHit2D raycastdownMap = Physics2D.Raycast(targetPosition, Vector2.down, raycastDistance+4, underMap);

        RaycastHit2D raycastdown = Physics2D.Raycast(targetPosition, Vector2.down, raycastDistance, ground);


        if (raycastdown.collider != null ||raycastdownMap.collider != null ) 
        {
            targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
        if (raycastright.collider != null)
        {
             return;
        }
        else if(raycastleft.collider != null)
        {
            return;
        }

        // If no collision was detected, smoothly move the camera towards the target position
        //Debug.DrawRay(transform.position, targetPosition - transform.position, Color.green);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
