using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Subscriber Event")]
public class SubscriberEvent : ScriptableObject
{
    private List<Func<Task>> taskList = new List<Func<Task>>();

    public async Task RaiseEvent()
    {
        Queue<Func<Task>> taskQueue = new Queue<Func<Task>>(taskList);
        while (taskQueue.Count > 0)
        {
            var task = taskQueue.Dequeue();
            await task();
        }
    }

    public void Subscribe(Func<Task> task)
    {
        taskList.Add(task);
    }
    public void Unsubscribe(Func<Task> task)
    {
        taskList.Remove(task);
    }
}