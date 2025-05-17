using UnityEngine;
using static Define;

public class InGameScene : BaseScene
{
    public override void Init()
    {
        SceneType = SceneType.InGameScene;
        Managers.Sound.PlayBGM(BGM.InGameScene);
        Debug.Log("인게임 씬 초기화");
    }

    public override void Clear()
    {
        // 씬 정리
        //Managers.Sound.Stop();
        Managers.Input.CancelAction();
        Debug.Log("인게임 씬 정리");
    }
}