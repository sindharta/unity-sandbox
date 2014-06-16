using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameData;
using System.IO;


public class ProtoBufNetTest : MonoBehaviour {

    HeroDataDic m_gameData;
    GameDataSerializer m_gameDataSerializer;

    // Use this for initialization
    void Start () {
        TextAsset textAsset = (TextAsset) Resources.Load("GameData", typeof(TextAsset));
        if (null==textAsset) {
            Debug.LogError("GameData not detected. Run GameData|Create first");
            return;
        }
        
        GameDataSerializer gameDataSerializer = new GameDataSerializer();
        if (null == gameDataSerializer) {
            Debug.LogError("Failed to initialize game data serializer");
            return;
        }
        
        MemoryStream s = new MemoryStream(textAsset.bytes);
        
        object data = gameDataSerializer.Deserialize(s,null,typeof(HeroDataDic));
        m_gameData = (HeroDataDic) (data);
        s.Close();
    } 

    //
    void OnGUI() {
        if (null == m_gameData)
            return;
         
        var enumerator = m_gameData.Items.GetEnumerator();
        while (enumerator.MoveNext()) {
            var cur = enumerator.Current;
            HeroData cur_data = cur.Value;
            string info = string.Format("{0} {1} {2} {3}",cur_data.ID, cur_data.Name, cur_data.HP, cur_data.MP);
            GUI.Label(new Rect(10, 30 * cur.Key, 100, 20), info);
        }         
    }
}
