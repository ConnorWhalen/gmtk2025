using UnityEngine;

public class AudienceMember : MonoBehaviour
{
    private Animator _animator;
    private int bug_type = -1;

    void Start()
    {
        _animator = GetComponent<Animator>();

        SetAudience();
    }

    void Update()
    {
        // DEBUG: press space to toggle bug
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log("BUG TYPE: " + bug_type);
        //     if (bug_type == -1)
        //     {
        //         SetAudience();
        //     }
        //     else
        //     {
        //         SetEmpty();
        //     }
        // }
    }

    public void SetAudience()
    {
        bug_type = Random.Range(0, 3);
        _animator.Play(Animator.StringToHash("Audience" + bug_type));
    }

    public void SetEmpty()
    {
        _animator.Play(Animator.StringToHash("Idle"));
        bug_type = -1;
    }
}
