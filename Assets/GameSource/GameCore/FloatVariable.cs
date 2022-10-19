using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cpu/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            if (useEvent)
            {
                for (int i = 0; i < listeners.Count; i++)
                {
                    listeners[i].Raise();
                }
            }
        }
    }

    public bool useEvent;
    [SerializeField] private int _value;
    [SerializeField] private List<IListener> listeners = new List<IListener>();

    public void Register(IListener listener)
    {
        listeners.Add(listener);
    }

    public void ClearList()
    {
        listeners.Clear();
    }

    public void RemoveMe(IListener listener)
    {
        listeners.Remove(listener);
    }
}

public interface IListener
{
    void Raise();
}