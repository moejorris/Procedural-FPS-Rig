using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    enum UpdateMode { Fixed, Normal, Late }

    [SerializeField] UpdateMode updateMode = UpdateMode.Normal;
    [SerializeField] Transform target;
    void Follow(UpdateMode _updateMode)
    {
        if (target != null && updateMode == _updateMode)
        {
            transform.position = target.position;
        }
    }

    void Update()
    {
        Follow(UpdateMode.Normal);
    }

    void FixedUpdate()
    {
        Follow(UpdateMode.Fixed);
    }
    void LateUpdate()
    {
        Follow(UpdateMode.Late);
    }
}
