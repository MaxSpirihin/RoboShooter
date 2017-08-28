using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


/// <summary>
/// определение пути из нескольких точек
/// </summary>
public class PathDefinition : MonoBehaviour
{
	public Transform[] points;
    public bool cycle;
    public bool getPointsFromChilds;
    public bool isComplete { get; private set; }
	
	public IEnumerator<Transform> GetPathEnumerator()
	{
        isComplete = false;

		if(points == null || points.Length < 1)
		{
			yield break;
		}
						
		int direction = 1;
		int index = 0;
		while (true)
		{
			yield return points[index];
			
			if(points.Length == 1)
				continue;

            if (!cycle)
            {
                if (index <= 0)
                    direction = 1;
                else if (index >= points.Length - 1)
                    direction = -1;
                index = index + direction;
            }
            else
            {
                index++;
                if (index >= points.Count())
                    index -= points.Count();
            }

            if (index == points.Length - 1)
                isComplete = true;
		}
	}
	

    /// <summary>
    /// отрисовка пути в редакторе
    /// </summary>
	public void OnDrawGizmos()
	{

        if (getPointsFromChilds)
            this.points = Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i)).ToArray();

		if(this.points == null || this.points.Length < 2)
			return;			
		
		var _points = points.Where(t=> t != null).ToList();
		
		if (_points.Count < 2)
			return;
		
		for (var i = 1; i < _points.Count; i++)
		{
			Gizmos.DrawLine(_points[i - 1].position, _points[i].position);
		}
		
	}
}
