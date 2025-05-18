using UnityEngine;

public class GoalArea : MonoBehaviour
{
    float _existTime = 0.0f;
    float _successTime = 5f;

    bool _isPlayerIn = false;

    void Update()
    {
        if(_isPlayerIn)
        {
            _existTime += Time.deltaTime;
            Debug.Log("플레이어 체류 중 " + _existTime);
            if (_existTime >= 10f)
            {
                // 성공
                Debug.Log("Success");
                Managers.Scene.LoadScene("EndingScene");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어 감지됨");
            _existTime = 0f;
            _isPlayerIn = true;
        }
    }
}
