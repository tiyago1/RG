using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Test Layout Element
/// </summary>

public class LayoutElement : MonoBehaviour
{
    private Text mPriortyText;
    private RectTransform mTransform;
    private int mHeight;
    public int Piority;

    public void Awake()
      {
        mPriortyText = this.transform.GetChild(0).GetComponent<Text>();
        mTransform = this.GetComponent<RectTransform>();
    }
    public void InitData(TestData data)
    {
        mPriortyText.text = data.Piority.ToString();
        //Debug.LogError("sizedelta.y : " + mTransform.sizeDelta.y + " Data piority " + data.Piority + " Sonuc :  " + (-1 * (mTransform.sizeDelta.y * (data.Piority - 1))));
        mTransform.anchoredPosition = new Vector2(0, (-1 * (mTransform.sizeDelta.y * (data.Piority -1))));
        Piority = data.Piority;
    }
}
