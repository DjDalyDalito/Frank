using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 10f;
    public Camera playerCamera;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintMultiplier = 2f;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 moveDirection = playerCamera.transform.forward * inputDirection.z + playerCamera.transform.right * inputDirection.x;
            moveDirection.y = 0f;

            if (Input.GetKey(sprintKey))
            {
                rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime * sprintMultiplier);
            }
            else
            {
                rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }

    void Rotate()
    {
        Vector3 lookDirection = playerCamera.transform.forward;
        lookDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
    }
}
