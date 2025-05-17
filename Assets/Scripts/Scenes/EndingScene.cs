using UnityEngine;
using static Define;

public class EndingScene : BaseScene
{
    public override void Init()
    {
        SceneType = SceneType.EndingScene;
        Managers.Sound.PlayBGM(BGM.EndingScene);
        Debug.Log("엔드 씬 초기화");
    }
}