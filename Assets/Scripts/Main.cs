using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using UnityEngine.UI;

public class SlopeDetector : MonoBehaviour
{
    public Vector3 cameraRight = Vector3.right;
    public Vector3 cameraForward = Vector3.forward;
    public Vector3 groundUp = Vector3.up;
    public Vector3 groundRight = Vector3.right;
    public Vector3 groundForward = Vector3.forward;
    public bool isGrounded = false;
    public bool isDead = false;

    void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            Vector3 totalPosition = Vector3.zero;
            Vector3 totalNormal = Vector3.zero;
            int contactCount = 0;
            foreach (ContactPoint contact in collision.contacts)
            {
                contactCount++;
                totalNormal += contact.normal;
                totalPosition += contact.point;
            }

            GameObject center = GameObject.Find("Center");
            cameraForward = Quaternion.Euler(0, center.transform.localEulerAngles.y, 0) * Vector3.forward;
            cameraRight = Vector3.Cross(Vector3.up, cameraForward);

            groundUp = totalNormal.normalized;
            groundRight = Vector3.Cross(groundUp, cameraForward).normalized;
            groundForward = -Vector3.Cross(groundUp, cameraRight).normalized;
            totalPosition /= contactCount;
            Debug.DrawRay(totalPosition, groundUp * 1, Color.red);
            Debug.DrawRay(totalPosition, groundRight * 1, Color.green);
            Debug.DrawRay(totalPosition, groundForward * 1, Color.blue);
        }
        else if (other.CompareTag("Kill"))
        {
            isDead = true;
        }
    }
}

public class Main : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject body;
    public GameObject hand;
    public GameObject foot;
    public GameObject occupiedAudience;
    public GameObject startButton;
    public GameObject curtain;
    public PhysicsMaterial bugPhysicsMaterial;
    public GameObject center;

    private float InputForce = 12.0f;
    private float JumpForce = 1.0f;

    public int playerScore;
    public Text scoreText;

    public int TotalBugs = 50;
    
    List<GameObject> bugs = new List<GameObject>();
    List<Vector3> bugGroundNormals = new List<Vector3>();
    List<Rigidbody> rigidBodies = new List<Rigidbody>();

    List<GameObject> bodys = new List<GameObject>();
    List<GameObject> leftHands = new List<GameObject>();
    List<GameObject> rightHands = new List<GameObject>();
    List<GameObject> leftFoots = new List<GameObject>();
    List<GameObject> rightFoots = new List<GameObject>();
    List<GameObject> audience = new List<GameObject>();
    List<GameObject> curtains = new List<GameObject>();


    void Start()
    {
        int root = (int) Mathf.Sqrt(TotalBugs);
        float centerX = 0.0f;
        float centerZ = -200.0f;
        float step = 2.0f / 3.0f;
        float width = (root - 1.0f) * step;
        float length = width;
        for (int bugIndex = 0; bugIndex < TotalBugs; bugIndex++)
        {
            int row = bugIndex / root;
            int column = bugIndex % root;

            float xPosition = centerX - (row * step) + width/2.0f;
            float yPosition = 1.0f;
            float zPosition = centerZ + (column * step) - length/2.0f;

            bugs.Add(new GameObject("bug" + bugIndex.ToString()));
            bugs[bugIndex].transform.position = new Vector3(xPosition, yPosition, zPosition);
            SphereCollider sphereCollider = bugs[bugIndex].AddComponent<SphereCollider>();
            sphereCollider.radius = 0.3f;
            sphereCollider.material = bugPhysicsMaterial;
            bugs[bugIndex].AddComponent<SlopeDetector>();

            rigidBodies.Add(bugs[bugIndex].AddComponent<Rigidbody>());
            rigidBodies[bugIndex].useGravity = true;
            rigidBodies[bugIndex].mass = 0.1f;
            rigidBodies[bugIndex].isKinematic = false;
            rigidBodies[bugIndex].linearDamping = 1.5f;
            rigidBodies[bugIndex].angularDamping = 0.0f;

            bodys.Add(Instantiate(body));
            leftHands.Add(Instantiate(hand));
            rightHands.Add(Instantiate(hand));
            leftFoots.Add(Instantiate(foot));
            rightFoots.Add(Instantiate(foot));
        }
        mainCamera.transform.parent = center.transform;

        // backrow
        for (int audienceIndex = 0; audienceIndex < 18; audienceIndex++)
        {
            float xPosition = -1.2f + audienceIndex * 0.14f;
            audience.Add(Instantiate(occupiedAudience));
            audience[audienceIndex].transform.parent = mainCamera.transform;
            audience[audienceIndex].transform.localPosition = new Vector3(xPosition, -0.42f, 0.5f);
            audience[audienceIndex].transform.localRotation = Quaternion.identity;
            audience[audienceIndex].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            audience[audienceIndex].SetActive(true);
            AudienceMember audienceScript = audience[audienceIndex].GetComponent<AudienceMember>();
            audienceScript.enabled = true;
        }
        // middlerow
        for (int audienceIndex = 18; audienceIndex < 18 + 20; audienceIndex++)
        {
            float xPosition = -1.35f + (audienceIndex - 18) * 0.14f;
            audience.Add(Instantiate(occupiedAudience));
            audience[audienceIndex].transform.parent = mainCamera.transform;
            audience[audienceIndex].transform.localPosition = new Vector3(xPosition, -0.448f, 0.55f);
            audience[audienceIndex].transform.localRotation = Quaternion.identity;
            audience[audienceIndex].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            audience[audienceIndex].SetActive(true);
            AudienceMember audienceScript = audience[audienceIndex].GetComponent<AudienceMember>();
            audienceScript.enabled = true;
        }
        // frontrow
        for (int audienceIndex = 18 + 20; audienceIndex < 18 + 20 + 22; audienceIndex++)
        {
            float xPosition = -1.5f + (audienceIndex - 18 - 20) * 0.14f;
            audience.Add(Instantiate(occupiedAudience));
            audience[audienceIndex].transform.parent = mainCamera.transform;
            audience[audienceIndex].transform.localPosition = new Vector3(xPosition, -0.466f, 0.6f);
            audience[audienceIndex].transform.localRotation = Quaternion.identity;
            audience[audienceIndex].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            audience[audienceIndex].SetActive(true);
            AudienceMember audienceScript = audience[audienceIndex].GetComponent<AudienceMember>();
            audienceScript.enabled = true;
        }

        for (int curtainIndex = 0; curtainIndex < 3; curtainIndex++)
        {
            curtains.Add(Instantiate(curtain));
            curtains[curtainIndex].transform.parent = mainCamera.transform;
            curtains[curtainIndex].transform.localRotation = Quaternion.identity;
        }
        curtains[0].transform.localPosition = new Vector3(-3.0f, -2.2f, 2.21f);
        curtains[1].transform.localPosition = new Vector3(3.0f, -2.2f, 2.2f);
        curtains[2].transform.localPosition = new Vector3(0, 2.0f, 2.1f);

        Button button = startButton.GetComponent<Button>();
        button.onClick.AddListener(() => StartCoroutine(OpenCurtains()));
    }

    IEnumerator OpenCurtains()
    {
        float currentTime = 0.0f;
        float endTime = 1.0f; 
        while(currentTime < endTime)
        {
            currentTime = Mathf.Min(currentTime + Time.deltaTime, endTime);
            curtains[0].transform.localPosition = new Vector3(-3.0f - 2.0f * currentTime, -2.2f, 2.21f);
            curtains[1].transform.localPosition = new Vector3(3.0f + 2.0f * currentTime, -2.2f, 2.2f);
            curtains[2].transform.localPosition = new Vector3(0, 2.0f - 0.7f * currentTime, 2.0f);

            curtains[0].transform.localScale = new Vector3(endTime - 0.7f * currentTime, 1.0f, 1.0f);
            curtains[1].transform.localScale = new Vector3(endTime - 0.7f * currentTime, 1.0f, 1.0f);

            startButton.transform.localScale = new Vector3(1.0f - currentTime, 1.0f - currentTime, 1.0f - currentTime);

            yield return null; // resume execution on the next frame
        }
    }

    IEnumerator CloseCurtains()
    {
        float currentTime = 0.0f;
        float endTime = 1.0f; 
        while(currentTime < endTime)
        {
            currentTime = Mathf.Min(currentTime + Time.deltaTime, endTime);
            curtains[0].transform.localPosition = new Vector3(-5.0f + 2.0f * currentTime, -2.2f, 2.21f);
            curtains[1].transform.localPosition = new Vector3(5.0f - 2.0f * currentTime, -2.2f, 2.2f);
            curtains[2].transform.localPosition = new Vector3(0, 1.3f + 0.7f * currentTime, 2.1f);

            curtains[0].transform.localScale = new Vector3(0.3f + 0.7f * currentTime, 1.0f, 1.0f);
            curtains[1].transform.localScale = new Vector3(0.3f + 0.7f * currentTime, 1.0f, 1.0f);

            startButton.transform.localScale = new Vector3(currentTime, currentTime, currentTime);

            yield return null; // resume execution on the next frame
        }
    }


    void Update()
    {
        Vector3 bugCentroid = new Vector3(0.0f, 0.0f, 0.0f);
        for (int bugIndex = 0; bugIndex < bugs.Count; bugIndex++)
        {
            bodys[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.8f, 0.0f);
            leftFoots[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.0f, 0.1f);
            rightFoots[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.0f, -0.1f);
            leftHands[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.8f, 0.1f);
            rightHands[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.8f, -0.1f);

            bugCentroid += bugs[bugIndex].transform.position;
        }
        bugCentroid /= bugs.Count;

        playerScore = bugs.Count;
        scoreText.text = playerScore.ToString();

        if (bugs.Count > 0)
        {
            float leaderAngle = Mathf.Rad2Deg * Mathf.Atan2(-bugCentroid.x, -bugCentroid.z);
            if (leaderAngle - center.transform.localEulerAngles.y > 180.0f) leaderAngle -= 360.0f;
            if (leaderAngle - center.transform.localEulerAngles.y < -180.0f) leaderAngle += 360.0f;
            center.transform.localEulerAngles = new Vector3(0.0f, leaderAngle*0.01f + center.transform.localEulerAngles.y*0.99f, 0.0f);
        }

        foreach (GameObject bug in bugs)
        {
            SlopeDetector sd = bug.GetComponent<SlopeDetector>();
            sd.cameraRight = center.transform.right;
            sd.cameraForward = center.transform.forward;
        }
    }

    void FixedUpdate()
    {
        for (int bugIndex = bugs.Count - 1; bugIndex >= 0; bugIndex--)
        {
            if (bugs[bugIndex].GetComponent<SlopeDetector>().isDead)
            {
                Destroy(bugs[bugIndex]);
                bugs.RemoveAt(bugIndex);
            }
        }

        bool doLeft = Input.GetKey(KeyCode.A);
        bool doRight = Input.GetKey(KeyCode.D);
        bool doUp = Input.GetKey(KeyCode.W);
        bool doDown = Input.GetKey(KeyCode.S);
        bool doJump = Input.GetKey(KeyCode.Space);
        foreach (GameObject bug in bugs)
        {
            Rigidbody rb = bug.GetComponent<Rigidbody>();
            SlopeDetector sd = bug.GetComponent<SlopeDetector>();
            if (sd.isGrounded)
            {

                if (doLeft)
                {
                    rb.AddForce(-sd.groundRight * InputForce, ForceMode.Force);
                }
                if (doRight)
                {
                    rb.AddForce(sd.groundRight * InputForce, ForceMode.Force);
                }
                if (doUp)
                {
                    rb.AddForce(sd.groundForward * InputForce, ForceMode.Force);
                }
                if (doDown)
                {
                    rb.AddForce(-sd.groundForward * InputForce, ForceMode.Force);
                }
            }

            if (sd.isGrounded && doJump)
            {
                var jumpVector = sd.groundUp;
                if (doLeft)
                {
                    jumpVector -= sd.groundRight;
                }
                if (doRight)
                {
                    jumpVector += sd.groundRight;
                }
                if (doUp)
                {
                    jumpVector -= sd.groundForward;
                }
                if (doDown)
                {
                    jumpVector += sd.groundForward;
                }
                rb.AddForce(JumpForce * sd.groundUp, ForceMode.Impulse);
                sd.isGrounded = false;
                sd.groundUp = Vector3.up;
                sd.groundRight = Vector3.right;
                sd.groundForward = Vector3.forward;
            }
        }

        Debug.Log("CAMERA FORWARD: " + bugs[0].GetComponent<SlopeDetector>().cameraForward);
        Debug.Log("CAMERA RIGHT: " + bugs[0].GetComponent<SlopeDetector>().cameraRight);

        // Demo curtain close
        if (Input.GetKey(KeyCode.P))
        {
            StartCoroutine(CloseCurtains());
        }
    }
}
