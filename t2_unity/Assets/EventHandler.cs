using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class EventHandler : MonoBehaviour {

    public Text String;
    public Text Timestamp;

    public InputField MsgBox;

    public Text Error;
    public Image ErrorPic;

    private string JsonFile = "msg.json";
    private string BannedWord = "hello";

    private class MsgInfo
    {
        public string Msg { get; set; }
        public string TimeStamp { get; set; }
    }

    MsgInfo msgData = new MsgInfo();

    private void renewMessage()
    {
        string mString = "No previous message found";
        string mTimestamp = "No previous timestamp found";

        if (File.Exists(JsonFile))
        {
            using(var f = new StreamReader(JsonFile))
            {
                string data = f.ReadToEnd();
                msgData = JsonConvert.DeserializeObject<MsgInfo>(data);
                mString = msgData.Msg;
                mTimestamp = msgData.TimeStamp;
            }
        }

        String.text = mString;
        Timestamp.text = mTimestamp;
    }

    private void updateMessage(string newString)
    {
        msgData.Msg = newString;
        msgData.TimeStamp = System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");

        string jsonData = JsonConvert.SerializeObject(msgData);
        System.IO.File.WriteAllText(JsonFile, jsonData);

        String.text = msgData.Msg;
        Timestamp.text = msgData.TimeStamp;
    }

    IEnumerator updatePic()
    {
        string url = "https://picsum.photos/458/354/?image=356";
        WWW www = new WWW(url);
        yield return www;
        ErrorPic.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }

	// Use this for initialization
	void Start () {
        renewMessage();
        Error.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        if(MsgBox.isFocused && MsgBox.text != "" && Input.GetKey(KeyCode.Return))
        {
            if (MsgBox.text.Contains(BannedWord))
            {
                Error.text = string.Format("{0} is not allowed in input!!!", BannedWord);
                StartCoroutine(updatePic());
            }
            else
            {
                updateMessage(MsgBox.text);
                Error.text = "String updated successfully!!!";
                ErrorPic.sprite = null;
            }
            MsgBox.text = "";
        }
    }
}
