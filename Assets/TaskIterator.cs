using UnityEngine;
using System.Collections;
using IteratorTasks;

public class TaskIterator : MonoBehaviour
{
    private TaskScheduler _scheduler = Task.DefaultScheduler;
	
    private void Start()
    {
        _scheduler.UnhandledException += UnhandledException;
    }

    private void UnhandledException(Task obj)
    {
        Debug.LogError(obj.Error.ToString());
    }

	private void Update ()
    {
        _scheduler.Update();
	}
}
