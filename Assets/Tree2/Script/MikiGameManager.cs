using System.Collections.Generic;
using UnityEngine;

public class MikiGameManager : MonoBehaviour
{
    public Transform mikiManagerParent;
    public List<MikiManager> mikiManagers;
    List<int> playOffMikiIndexList = new List<int>();

    public bool playOn;
    float timeCount;
    public float startTime;
    public float time;

    public float createMikiTime;

    void Start()
    {
        timeCount = startTime;

        foreach(Transform child in mikiManagerParent)
        {
            if(child.TryGetComponent(out MikiManager mikiManager))
            {
                mikiManagers.Add(mikiManager);
                mikiManager.createTime = this.createMikiTime;
            }
            
        }

        for(int i=0;i<mikiManagers.Count;i++)
        {
            playOffMikiIndexList.Add(i);
        }
        AllPlayOnOff(false);
    }

    public void AllPlayOnOff(bool state)
    {
        foreach (MikiManager mikiManager in this.mikiManagers)
        {
            mikiManager.playOn = state;
        }
    }


    void PlayOnOff(int mikiNumber, bool state=true)
    {
        mikiManagers[mikiNumber].playOn = state;
    }

    private void Update()
    {
        if(playOn)
        {
            timeCount += Time.deltaTime;
            if(timeCount >= time)
            {
                timeCount = 0f;
                if(playOffMikiIndexList.Count > 0)
                {
                    //まだ起動していないMikiManagerのIndexを取得する
                    int randomIndex = Random.Range(0, playOffMikiIndexList.Count);
                    int playOnIndex = playOffMikiIndexList[randomIndex];

                    //そのMikiManagerを起動させて,起動していないMikiManagerの番号リストから削除する
                    playOffMikiIndexList.Remove(playOnIndex);
                    PlayOnOff(playOnIndex);
                }
                
            }
        }
    }

}
