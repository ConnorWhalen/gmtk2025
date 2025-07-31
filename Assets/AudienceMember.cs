using UnityEngine;

public class AudienceMember : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void SetAudience()
    {
        int bug_type = Random.Range(0, 3);

        _animator.play("Audience" + bug_type);
    }

    void SetEmpty()
    {
        _animator.StopPlayback();
    }
}
