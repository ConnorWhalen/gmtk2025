using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject body;
    public GameObject hand;
    public GameObject foot;
    public GameObject occupiedAudience;
    public GameObject startButton;
    public GameObject curtain;
    private float InputForce = 100.0f;
    
    List<GameObject> bugs = new List<GameObject>();
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
        for (int bugIndex = 0; bugIndex < 100; bugIndex++)
        {
            int row = bugIndex / 10;
            int column = bugIndex % 10; 

            float xPosition = -12.0f + 6.0f * column / 9.0f;
            float yPosition = 1.0f;
            float zPosition = 3.0f + 6.0f * row / 9.0f;
            

            bugs.Add(new GameObject("bug" + bugIndex.ToString()));
            bugs[bugIndex].transform.position = new Vector3(xPosition, yPosition, zPosition);
            SphereCollider sphereCollider = bugs[bugIndex].AddComponent<SphereCollider>();
            sphereCollider.radius = 0.025f;

            rigidBodies.Add(bugs[bugIndex].AddComponent<Rigidbody>());
            rigidBodies[bugIndex].useGravity = true;
            rigidBodies[bugIndex].mass = 1f;
            rigidBodies[bugIndex].isKinematic = false;

            bodys.Add(Instantiate(body));
            leftHands.Add(Instantiate(hand));
            rightHands.Add(Instantiate(hand));
            leftFoots.Add(Instantiate(foot));
            rightFoots.Add(Instantiate(foot));
        }

        // backrow
        for (int audienceIndex = 0; audienceIndex < 18; audienceIndex++)
        {
            float xPosition = -1.2f + audienceIndex * 0.14f;
            audience.Add(Instantiate(occupiedAudience));
            audience[audienceIndex].transform.parent = mainCamera.transform;
            audience[audienceIndex].transform.localPosition = new Vector3(xPosition, -0.5f, 0.5f);
            audience[audienceIndex].transform.localRotation = Quaternion.identity;
            audience[audienceIndex].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //audience[audienceIndex].GetComponent<Animator>().Play(Animator.StringToHash("Audience" + UnityEngine.Random.Range(0, 3)));
        }
        // middlerow
        for (int audienceIndex = 18; audienceIndex < 18 + 20; audienceIndex++)
        {
            float xPosition = -1.35f + (audienceIndex - 18) * 0.14f;
            audience.Add(Instantiate(occupiedAudience));
            audience[audienceIndex].transform.parent = mainCamera.transform;
            audience[audienceIndex].transform.localPosition = new Vector3(xPosition, -0.54f, 0.55f);
            audience[audienceIndex].transform.localRotation = Quaternion.identity;
            audience[audienceIndex].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //audience[audienceIndex].GetComponent<Animator>().Play(Animator.StringToHash("Audience" + UnityEngine.Random.Range(0, 3)));
        }
        // frontrow
        for (int audienceIndex = 18 + 20; audienceIndex < 18 + 20 + 22; audienceIndex++)
        {
            float xPosition = -1.5f + (audienceIndex - 18 - 20) * 0.14f;
            audience.Add(Instantiate(occupiedAudience));
            audience[audienceIndex].transform.parent = mainCamera.transform;
            audience[audienceIndex].transform.localPosition = new Vector3(xPosition, -0.58f, 0.6f);
            audience[audienceIndex].transform.localRotation = Quaternion.identity;
            audience[audienceIndex].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //audience[audienceIndex].GetComponent<Animator>().Play(Animator.StringToHash("Audience" + UnityEngine.Random.Range(0, 3)));
        }

        for (int curtainIndex = 0; curtainIndex < 2; curtainIndex++)
        {
            curtains.Add(Instantiate(curtain));
            curtains[curtainIndex].transform.parent = mainCamera.transform;
            curtains[curtainIndex].transform.localRotation = Quaternion.identity;
        }
        curtains[0].transform.localPosition = new Vector3(-3.0f, -2.2f, 2.2f);
        curtains[1].transform.localPosition = new Vector3(3.0f, -2.2f, 2.2f);

        Button button = startButton.GetComponent<Button>();
        button.onClick.AddListener(() => StartCoroutine(OpenCurtains()));
    }

    //Debug.Log(currentTime.ToString("n2"));
    IEnumerator OpenCurtains()
    {
        float currentTime = 0.0f;
        float endTime = 1.0f; 
        while(currentTime < endTime)
        {
            currentTime = Mathf.Min(currentTime + Time.deltaTime, endTime);
            curtains[0].transform.localPosition = new Vector3(-3.0f - 4.0f * currentTime, -2.2f, 2.2f);
            curtains[1].transform.localPosition = new Vector3(3.0f + 4.0f * currentTime, -2.2f, 2.2f);
            startButton.transform.localScale = new Vector3(1.0f - currentTime, 1.0f - currentTime, 1.0f - currentTime);
            foreach(GameObject curtain in curtains)
            {
                curtain.transform.localScale = new Vector3(endTime - currentTime, 1.0f, 1.0f);
            }
            yield return null; // resume execution on the next frame
        }
    }

    void Update()
    {
        for(int bugIndex = 0; bugIndex < 100; bugIndex++)
        {
            bodys[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.8f, 0.0f);
            leftFoots[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.0f, 0.1f);
            rightFoots[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.0f, -0.1f);
            leftHands[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.8f, 0.1f);
            rightHands[bugIndex].transform.position = bugs[bugIndex].transform.position + new Vector3(0.0f, 0.8f, -0.1f);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            foreach (Rigidbody rb in rigidBodies)
            {
                rb.AddForce(new Vector3(InputForce, 0.0f, 0.0f), ForceMode.Force);
            }
        }
    }
}
