using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance; // singleton
    public SaveData activeSave;
    public bool startGame = false;

    public GetSaveValues getSaveValues;

    void Awake() {

        DontDestroyOnLoad(this.gameObject);
        instance = this;

        Load();

        if (activeSave.autosave){
            StartCoroutine(AutoSave());
        }

    }

    void Start() {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.SaveName + ".savefile") && startGame) {
                getSaveValues.LoadingValues();
        }
    }

    
    public void Save() {
        getSaveValues.SavingValues();
        
        string dataPath = Application.persistentDataPath;
        var Serializer = new XmlSerializer(typeof(SaveData));
        var Stream = new FileStream(dataPath + "/" + activeSave.SaveName + ".savefile", FileMode.Create);

        Serializer.Serialize(Stream, activeSave);
        Stream.Close();

        print("saved");
    }

    public void Load() {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.SaveName + ".savefile")) {   
            var Serializer = new XmlSerializer(typeof(SaveData));
            var Stream = new FileStream(dataPath + "/" + activeSave.SaveName + ".savefile", FileMode.Open);

            activeSave = Serializer.Deserialize(Stream) as SaveData;
            Stream.Close();
            
        } else {
            // default settings e.g. render distance, difficulty
            activeSave.health = 100;
        }

    }

    public void DeleteData() {
        string dataPath = Application.persistentDataPath;
        
        if (System.IO.File.Exists(dataPath + "/" + activeSave.SaveName + ".savefile")) {   
            File.Delete(Application.persistentDataPath + "/" + activeSave.SaveName + ".savefile");
            print("data deleted");
        }
    }

    IEnumerator AutoSave() {
        yield return new WaitForSeconds(activeSave.AutoSaveTime);
        Save();

        if (activeSave.autosave) {
            StartCoroutine(AutoSave());
        }
    }

}

[System.Serializable]
public class SaveData
{
    public string SaveName; // access save data

    // all saving var

    public List<GameObject> weapons = new List<GameObject>();
    public float health;
    public int maxWeaponSlot;

    // autosave var

    public int AutoSaveTime;
    public bool autosave;

}