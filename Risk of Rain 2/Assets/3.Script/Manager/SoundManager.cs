using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound  //AudioCilp을 깔끔하게 정리하기 위함
{
    public string name;    //곡의 이름
    public AudioClip clip;      //곡 파일
}


public class SoundManager : MonoBehaviour
{

    static public SoundManager instance;    //사운드 매니저를 불러오기 위해 변수 선언

    private void Awake()    
    {
        //사운드 매니저는 하나만 있는 것이 편리하기에 싱글톤으로 사용.
        if (instance == null)
        {
            instance = this;    //선언된 변수에 사운드매니저 자기자신을 넣음.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);    //씬 전환 등 새롭게 생성되는것을 방지하기 위해 null이 아니라면 파괴되도록 설정
        }
    }

    [Header("효과음 플레이어")]
    public AudioSource[] audioSourcesEffects;
    [Header("BGM 플레이어")]
    public AudioSource audioSourceBgm;

    public string[] playSoundName;
    
    [Header("사운드 등록")]
    public Sound[] effectSound;     //AudioCilp
    public Sound[] bgmSound;

    private void Start()
    {
        playSoundName = new string[audioSourcesEffects.Length];
    }

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgmSound.Length; i++)
        {
            if(_name == bgmSound[i].name)
            {
                audioSourceBgm.clip = bgmSound[i].clip;
                audioSourceBgm.Play();
                return;
            }
        }
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSound.Length; i++)    //이팩트사운드의 배열에 해당된 음악 검색
        {
            if(_name == effectSound[i].name)    //돌린 for문에서 이름이 일치하는 오디오 소스 찾고 재생시키기
            {
                for (int j = 0; j < audioSourcesEffects.Length; j++)    //재생중인 흐름이 끊기지 않게, 오디오소스이팩트에 할당된 오디오 클립을 검색하고 재생중이지 않는걸 찾는다.
                {
                    if(!audioSourcesEffects[j].isPlaying)   //오디오소스이펙트 배열에서 재생중이지 않은 노래를 찾는 조건문
                    {
                        playSoundName[j] = effectSound[i].name;
                        audioSourcesEffects[j].clip = effectSound[i].clip;  // j번째 클립이 effectSound[i]가 되고 재생되게 된다.
                        audioSourcesEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 AudioSource가 사용중입니다");        //이 로그가 찍혔다는것은 오디오 소스 부족
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다");     //이 로그가 찍혔다는 것은 이름을 틀렸거나 없는 사운드
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourcesEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생중인"+ _name + "사운드가 없습니다");
    }


    // 다른 클래스에서 오디오 클립 불러오는 법
    // 스크립트에 변수선언 - [SerializeField] private string 노래명;
    // SoundManager.instance.PlaySE(노래명);



}
