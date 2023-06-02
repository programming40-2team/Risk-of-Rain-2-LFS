using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : BaseScene
{
    [SerializeField]
    private Slider ProgreesSlider;
    [SerializeField]
    private TextMeshProUGUI ProgressText;
    private void Start()
    {
        Init();
        StartCoroutine(nameof(LoadSceneProcess));
    }

    protected override void Init()
    {
        base.Init();
        
    }


    public override void Clear()
    {
        Managers.Event.ClearEventList();
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(Managers.Scene.GetSceneName( Managers.Scene.NextScene));
        op.allowSceneActivation = false;


        float time = 0f;
        while (!op.isDone)
        {
            if(op.progress < 0.9f)
            {
                yield return null;
                ProgreesSlider.value = op.progress;
                ProgressText.text = $"...{op.progress:00}%";
            }
            else
            {

                time += Time.unscaledDeltaTime;
                ProgreesSlider.value = Mathf.Lerp(0.9f, 1f, time);
                if (ProgreesSlider.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }


            }



        }



    }

}
