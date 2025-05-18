using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("WindEffect", 0f, 0.1f);
    }

    public void Play()
    {

    }
}
