using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleAble;
    public Transform gunTip, player;
    public float maxDistance = 100f;
    private SpringJoint joint;

    public new Transform camera;

    public AudioSource lazerSound;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleAble))

        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // The distance grapple will try to keep from grapple point
            joint.maxDistance = distanceFromPoint * 0.5f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // Change these values to fit your game
            joint.spring = 27f;
            joint.damper = 3.5f;
            joint.massScale = 2.25f;

            lr.positionCount = 2;

            lazerSound.Play();


        }
    }

    void DrawRope()
    {
        // if not grappling, dont draw rope
        if (!joint) return;


        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool isGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

}
