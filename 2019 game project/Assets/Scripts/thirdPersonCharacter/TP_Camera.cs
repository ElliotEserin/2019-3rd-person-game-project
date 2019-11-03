using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Camera : MonoBehaviour
{

    public static TP_Camera Instance;
    public Transform TargetLookAt;
    public float SlowMotionSpeed = 0.25f;
    public float Distance = 5f;
    public float DistanceMin = 1.5f;
    public float DistanceMax = 8f;
    public float DistanceSmooth = 0.05f;
    public float DistanceResumeSmooth = 1f;
    public float X_MouseSensitivity = 5f;
    public float Y_MouseSensitivity = 5f;
    public float MouseWheelSensitivity = 5f;
    public float Y_MinLimit = -40f;
    public float Y_MaxLimit = 80f;
    public float X_Smooth = 0.05f;
    public float Y_Smooth = 0.1f;
    public float OcclusionDistanceStep = 0.5f;
    public int MaxOcclusionChecks = 10;
    public bool inControl = false;

    private float mouseX = 0f;
    private float mouseY = 0f;
    private float velX = 0f;
    private float velY = 0f;
    private float velZ = 0f;
    private float velDistance = 0f;
    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 position = Vector3.zero;
    private float startDistance = 0f;
    private float desiredDistance = 0f;
    private float distanceSmooth = 0f;
    private float preOccludedDistance = 0f;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
        startDistance = Distance;
        inControl = true;
        reset();
    }

    void LateUpdate()
    {
        if (TargetLookAt == null)
            return;

        HandlePlayerInput();

        var count = 0;
        do
        {
            CalculateDesiredPosition();
            count++;
        } while (CheckIfOccluded(count));
        
        UpdatePosition();

        TimeSlowDown();

    }

    void HandlePlayerInput()
    {
        var deadZone = 0.01f; 

        if(inControl)
        {
            //The RMB is down. get mouse axis input
            mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;
        }

        //This is where we will limit mouseY
        mouseY = Helper.ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);

        if(Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
        {
            desiredDistance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity, DistanceMin, DistanceMax);

            preOccludedDistance = desiredDistance;
            distanceSmooth = DistanceSmooth;
        }
    }

    void CalculateDesiredPosition()
    {
        //Evaluate distance
        ResetDesiredDistance();
        Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velDistance, distanceSmooth);

        //Calculate desired position
        desiredPosition = CalculatePosition(mouseY, mouseX, Distance);
    }

    Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0,0,-distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return TargetLookAt.position + rotation * direction;
    }

    bool CheckIfOccluded(int count)
    {
        var isOccluded = false;

        var nearDistance = CheckCameraPoints(TargetLookAt.position, desiredPosition);

        if(nearDistance != -1)
        {
            if(count < MaxOcclusionChecks)
            {
                isOccluded = true;
                Distance -= OcclusionDistanceStep;

                if (Distance < 0.25f) //adjust after character is added
                    Distance = 0.25f;
            }
            else
                Distance = nearDistance - Camera.main.nearClipPlane;

            desiredDistance = Distance;
            distanceSmooth = DistanceResumeSmooth;
        }

        return isOccluded;
    }

    float CheckCameraPoints(Vector3 from, Vector3 to)
    {
        var nearDistance = -1f;

        RaycastHit hitInfo;

        Helper.ClipPlanePoints clipPlanePoints = Helper.ClipPlaneAtNear(to);

        if (Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "TriggerBox")
            nearDistance = hitInfo.distance;

        if (Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "TriggerBox")
            if(hitInfo.distance < nearDistance || nearDistance == -1)
                nearDistance = hitInfo.distance;

        if (Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "TriggerBox")
            if (hitInfo.distance < nearDistance || nearDistance == -1)
                nearDistance = hitInfo.distance;

        if (Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "TriggerBox")
            if (hitInfo.distance < nearDistance || nearDistance == -1)
                nearDistance = hitInfo.distance;

        if (Physics.Linecast(from, to + (transform.forward * Camera.main.nearClipPlane), out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "TriggerBox")
            if (hitInfo.distance < nearDistance || nearDistance == -1)
                nearDistance = hitInfo.distance;



        return nearDistance;
    }

    void ResetDesiredDistance()
    {
        if(desiredDistance < preOccludedDistance)
        {
            var pos = CalculatePosition(mouseY, mouseX, preOccludedDistance);

            var nearestDistance = CheckCameraPoints(TargetLookAt.position, pos);

            if(nearestDistance == -1 || nearestDistance > preOccludedDistance)
            {
                desiredDistance = preOccludedDistance;
            }
        }
    }

    void UpdatePosition()
    {
        var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
        var posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
        var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);

        position = new Vector3(posX, posY, posZ);

        transform.position = position;

        transform.LookAt(TargetLookAt);
    }

    public void reset()
    {
        mouseX = 0;
        mouseY = 10;
        Distance = startDistance;
        desiredDistance = Distance;
        preOccludedDistance = Distance;
    }

    public static void UseExistingOrCreateNewMainCamera()
    {
        GameObject tempCamera;
        GameObject targetLookAt;
        TP_Camera myCamera;
        GameObject playerInstance;

        if (Camera.main != null)
        {
            tempCamera = Camera.main.gameObject;
        }
        else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent<Camera>(); //double check
            tempCamera.tag = "MainCamera";
        }

        tempCamera.AddComponent<TP_Camera>(); //double check
        myCamera = tempCamera.GetComponent("TP_Camera") as TP_Camera;

        targetLookAt = GameObject.Find("targetLookAt") as GameObject;

        if(targetLookAt == null)
        {
            targetLookAt = new GameObject("targetLookAt");
            playerInstance = GameObject.Find("Player");
            targetLookAt.transform.parent = playerInstance.transform;
            targetLookAt.transform.position = playerInstance.transform.position;
        }

        myCamera.TargetLookAt = targetLookAt.transform;
    }

    public void TimeSlowDown()
    {
        Time.timeScale = 1;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = SlowMotionSpeed;
        }
    }
}
