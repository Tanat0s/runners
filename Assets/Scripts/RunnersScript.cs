using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RunnersScript : MonoBehaviour
{
    [SerializeField] private Transform _currentRunner;
    [SerializeField] private Transform _ball;
    [SerializeField] private Vector3[] points;
    [SerializeField] private Transform[] runners;

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

    private Transform GatAnotherRunner(Transform runner)
    {
        var result = runners.Where(r => r.position.Equals(runner.position));        

        return (result.Count() == 1) ? result.First() : result.First(r => !r.name.Equals(runner.name));
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentRunner.transform.position.Equals(_nextPoint))
        {
            if (_queue.Count == 0)
            {
                FillPath();
            }

            _nextPoint = _queue.Dequeue();
            _currentRunner = GatAnotherRunner(_currentRunner);
            _ball.SetParent(_currentRunner);
        }
        _currentRunner.transform.position = Vector3.MoveTowards(_currentRunner.transform.position, _nextPoint, speed * Time.deltaTime);
    }
}
