using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] int moveSpeed = 1;

    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] Animator playerAnimator;

    public string transitionName;

    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;
    
    public bool deactivatedMovement;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);//.normalized;

        if (deactivatedMovement)
        {
            playerRigidBody.velocity = Vector2.zero;
        }
        else
        {
        playerRigidBody.velocity = movement * moveSpeed;
        }

        playerAnimator.SetFloat("movementX", playerRigidBody.velocity.x);
        playerAnimator.SetFloat("movementY", playerRigidBody.velocity.y);

        if (horizontalMovement == 1 || horizontalMovement == -1 || verticalMovement == 1 || verticalMovement == -1)
        {
            if (!deactivatedMovement)
            {
                playerAnimator.SetFloat("lastX", playerRigidBody.velocity.x);
                playerAnimator.SetFloat("lastY", playerRigidBody.velocity.y);
            }
        }
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftEdge.x, topRightEdge.x),
            Mathf.Clamp(transform.position.y, bottomLeftEdge.y, topRightEdge.y), 
            Mathf.Clamp(transform.position.z, bottomLeftEdge.z, topRightEdge.z)
            );
    }

    public void SetLimit(Vector3 bottomEdgeToSet, Vector3 topEdgeToSet)
    {
        bottomLeftEdge = bottomEdgeToSet;
        topRightEdge = topEdgeToSet;
    }
}
