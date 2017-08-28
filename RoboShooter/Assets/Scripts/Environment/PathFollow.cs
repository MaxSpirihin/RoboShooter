using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// объект следующий по пути PathFollow
/// </summary>
public class PathFollow : MonoBehaviour//,IRespawnListener
{
	
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}
	
	public FollowType type = FollowType.MoveTowards;
	public PathDefinition path;
	public float speed = 1;
	public float maxDistanceToGoal = .1f;
    public bool onRespawnAgain = false;
    public bool stopOnEnd = false;
	
	private IEnumerator<Transform> _currentPoint;
	
	public void Start ()
	{
		if(path == null)
		{
			Debug.LogError("Path Cannot be null", gameObject);
			return;
		}
		
		_currentPoint = path.GetPathEnumerator();
		_currentPoint.MoveNext();
        
		
		if(_currentPoint.Current == null)
			return;
			
		transform.position = _currentPoint.Current.position;
	}
	
	public void Update ()
	{
        if (stopOnEnd && path.isComplete)
            return;

		if(_currentPoint == null || _currentPoint.Current == null)
			return;
		
		if(type == FollowType.MoveTowards)
			transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
		else if(type == FollowType.Lerp)
			transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
		
		var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
		if(distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
			_currentPoint.MoveNext();
	}


    //void IRespawnListener.OnRespawn()
    void OnRespawn()
    {
        if (onRespawnAgain)
        {
            _currentPoint = path.GetPathEnumerator();
            _currentPoint.MoveNext();
            transform.position = _currentPoint.Current.position;
        }
    }

    //void IRespawnListener.OnRespawnEnd()
    void OnRespawnEnd()
    {
    }
}
