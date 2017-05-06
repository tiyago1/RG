using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    public GameObject GameObject;

    public List<Texture2D> mData;
    public int SegmentSize = 10;
    private int mWidth;
    private int mHeight;
    public List<ChampionItems> Elements;

	public void Start ()
    {
        //mTestData = new List<TestData>();
        //Elements = new List<LayoutElement>();
        //for (int i = 0; i <= 100; i++)
        //{
        //    mTestData.Add(new TestData() { SumoName = "asd", Piority = i });
        //}

        
    }
	
    public void FirstLayoutSet()
    {
        for (int i = 1; i <= SegmentSize; i++)
        {
            GameObject gm = Instantiate(GameObject) as GameObject;
            gm.transform.SetParent(this.transform, false);
            List<Texture2D> temp = new List<Texture2D>();
            for (int z = i - 1; z < i + 2; z++)
            {
                temp.Add(mData[z]);
            }
            gm.GetComponent<ChampionItems>().InitData(temp, i);
            Elements.Add(gm.GetComponent<ChampionItems>());
        }
    }

	public void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        GameObject gm = Instantiate(GameObject) as GameObject;
        //        gm.transform.SetParent(this.transform, false);
        //        gm.GetComponent<LayoutElement>().InitData(new TestData() {  SumoName = "mAHMYT " , Piority = i});
        //        Elements.Add(gm.GetComponent<LayoutElement>());
        //    }
        //}
	}

    public void OnScrollValueChange(float value)
    {
        Debug.Log(value);


        int showRangeStart = (int)Mathf.Lerp(100 - 10, 0, value);

        //if (showRangeStart >= 51)
        if (value < 0.5f)
        {
            showRangeStart++;

            //if (showRangeStart >= 71)
            if (value < 0.3f)
            {
                showRangeStart++;
            }
        }

        showRangeStart = Mathf.Clamp(showRangeStart, 0, mData.Count - SegmentSize);
        int showRangeEnd = showRangeStart + SegmentSize;

        //Debug.LogError("showRangeStart: " + showRangeStart + " showRangeEnd: " + showRangeEnd);

        //foreach (var element in Elements)
        //{
        //    //                            1
        //    if (element.Piority <= showRangeStart)
        //    {
        //        Debug.LogWarning("Apiortiy : " + element.Piority);
        //        List<Texture2D> temp = new List<Texture2D>();
        //        for (int i = (element.Piority + SegmentSize) -1; i < (element.Piority + SegmentSize) +2; i++)
        //        {
        //            temp.Add(mData[i]);
        //        }
        //
        //        element.InitData(temp, element.Piority);
        //
        //    }
        //    else if (element.Piority > showRangeEnd)
        //    {
        //        Debug.LogWarning("Bpiortiy : " + element.Piority);
        //      // element.InitData(mData[element.Piority - SegmentSize], element.Piority);
        //    }
        //}

    }

}
