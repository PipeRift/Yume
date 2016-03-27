using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ERockBarrier : MonoBehaviour
{
    public enum Mode
    {
        AND,
        OR
    }
    public Mode mode;
    public UnityEvent activation;


    public bool first
    {
        set
        {
            m_first = value;
            Check();
        }
    }
    private bool m_first = false;

    public bool second
    {
        set
        {
            m_second = value;
            Check();
        }
    }
    private bool m_second = false;

    private bool done = false;

    void Check()
    {
        if (done)
            return;
        done = true;

        if (mode == Mode.OR)
        {
            if (m_first || m_second)
            {
                activation.Invoke();
            }
        }
        else if (mode == Mode.AND)
        {
            if (m_first && m_second)
            {
                activation.Invoke();
            }
        }
    }

    public void Reset() {
        done = false;
        m_first = false;
        m_second = false;
    }
}
