using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class asd
{

}

public class JASODSD : MonoBehaviour
{
    public string json;

	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            asd dd = JsonConvert.DeserializeObject<asd>(json);
            
        }
	}

    //private IEnumerator Post(string url)
    //{
    //    using (WWW request = new WWW(url))
    //    {
    //        yield return request;
    //        string response = request.text;

    //        try
    //        {
    //            Debug.Log(response);
    //        }
    //        catch (System.Exception)
    //        {
    //            Debug.Log("Error");
    //            throw;
    //        }
    //    }
    //}
}
