using UnityEngine;
using UnityEngine.SceneManagement;

public class Bugs : MonoBehaviour
{
    float3 LeftHandReset = float3(0.1f, 1.0f, 0.0f);
    float3 RightHadReset = float3(-0.1f, 1.0f, 0.0f);
    float3 LeftFootReset = float3(0.1f, 0.0f, 0.0f);
    float3 RightFootReset = float3(-0.1f, 0.0f, 0.0f);
    float3 BodyReset = float3(0.0f, 1.0f, 0.0f);

    List<bool> Exists[100];    
    List<float3> LeftHandWorldPosition[100];
    List<float3> RightHandWorldPosition[100];
    List<float3> LeftFootWorldPosition[100];
    List<float3> RightFootWorldPosition[100];
    List<float3> BodyWorldPosition[100];
    List<float3> TargetVelocity[100];
    void Start()
    {
        for (int BugIndex = 0; BugIndex < 100; BugIndex++)
        {
            int Row = BugIndex / 10;
            int Column = BugIndex % 10;
            Exists[BugIndex] = true;
            LeftHandWorldPosition[BugIndex] = float3(0.0f, 0.0f, 0.0f);
            RightHandWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            LeftFootWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            RightFootWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            BodyWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            TargetVelocity[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
        }
        BodyWorldPosition = float3(0.0, 1.0, 0.0);
        LeftHandWorldPosition = float3()
    }
    void Update()
    {
    }
}

public class RootScript : MonoBehaviour
{
    string SceneToLoad;
    public double Money = 100.0;
    public Bugs;

    const string StartScreen = "Assets/Scenes/StartScreen.unity";
    const string Circus = "Assets/Scenes/MakeMoney.unity";
    const string SpendMoney = "Assets/Scenes/SpendMoney.unity";

    void Start()
    {
        SceneToLoad = StartScreen;
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
    }

    void Update()
    {
    }

    public void LoadMakeMoney()
    {
        SceneManager.UnloadSceneAsync(SceneToLoad);
        SceneToLoad = MakeMoney;
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
    }
    
    public void LoadSpendMoney()
    {
        SceneManager.UnloadSceneAsync(SceneToLoad);
        SceneToLoad = SpendMoney;
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
    }
}

public class Bugs : MonoBehaviour
{
    float3 LeftHandReset = float3(0.1f, 1.0f, 0.0f);
    float3 RightHadReset = float3(-0.1f, 1.0f, 0.0f);
    float3 LeftFootReset = float3(0.1f, 0.0f, 0.0f);
    float3 RightFootReset = float3(-0.1f, 0.0f, 0.0f);
    float3 BodyReset = float3(0.0f, 1.0f, 0.0f);

    List<bool> Exists[100];    
    List<float3> LeftHandWorldPosition[100];
    List<float3> RightHandWorldPosition[100];
    List<float3> LeftFootWorldPosition[100];
    List<float3> RightFootWorldPosition[100];
    List<float3> BodyWorldPosition[100];
    List<float3> TargetVelocity[100];
    void Start()
    {
        for (int BugIndex = 0; BugIndex < 100; BugIndex++)
        {
            int Row = BugIndex / 10;
            int Column = BugIndex % 10;
            Exists[BugIndex] = true;
            LeftHandWorldPosition[BugIndex] = float3(0.0f, 0.0f, 0.0f);
            RightHandWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            LeftFootWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            RightFootWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            BodyWorldPosition[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
            TargetVelocity[BugIndex] =  float3(0.0f, 0.0f, 0.0f);
        }
        BodyWorldPosition = float3(0.0, 1.0, 0.0);
        LeftHandWorldPosition = float3()
    }
    void Update()
    {
    }
}
