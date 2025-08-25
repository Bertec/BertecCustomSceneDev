using UnityEngine;

/// <summary>
/// All this class does is provide a function to 'look at' the camera, so the bubble planes always face towards the viewer.
/// </summary>

public class BubblePrefab : MonoBehaviour
{
	public Transform target;

    public void Init(Transform _target)
	{
		target = _target;
	}

	// Each update point the bubble plane at the camera, while keeping the same z angle
	void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) > 1)
            {
                float z = transform.eulerAngles.z;
                transform.LookAt(target);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
            }
        }
    }
}
