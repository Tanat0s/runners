using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private Transform cube;
    [SerializeField] private Vector3[] points;

    [SerializeField] private float speed;
    private bool _forward = false;

    private Queue<Vector3> _queue;
    private Vector3 _nextPoint;

    // Start is called before the first frame update
    void Start()
    {
        _queue = new Queue<Vector3>(points.Length);
    }

    private void FillPath()
    {
        _forward = !_forward;

        foreach (var point in points)
        {
            _queue.Enqueue(point);
        }

        points = points.Reverse().ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(cube.transform.position.Equals(_nextPoint))
        {
            if (_queue.Count == 0)
            {
                FillPath();
            }

            _nextPoint = _queue.Dequeue();
            cube.transform.LookAt(_nextPoint);
        }
        cube.transform.position = Vector3.MoveTowards(cube.transform.position, _nextPoint, speed*Time.deltaTime);        
        cube.Rotate(new Vector3(0, 0, 5));
    }
}
