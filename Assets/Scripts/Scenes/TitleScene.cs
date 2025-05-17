using UnityEngine;

public class TitleScene : BaseScene
{
    public override void Init()
    {
        SceneType = Define.SceneType.TitleScene;
        Managers.Sound.PlayBGM(Define.BGM.TitleScene);
        Debug.Log("타이틀 씬 초기화");
    }

    public override void Clear()
    {
        Debug.Log("타이틀 씬 종료");
    }
}