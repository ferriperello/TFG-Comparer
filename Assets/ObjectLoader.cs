using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsImpL;
using UnityEngine.UI;

public class ObjectLoader : MonoBehaviour {

    public GameObject loadedObject;
    public ImportOptions importOptions = new ImportOptions();
    private ObjectImporter objImporter;
    private bool firstLoad = false;
    private bool secondLoad = false;
    private static string filePath;
    private static bool fpdone = false;
    public Text progressText;
    public GameObject loadButton;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (firstLoad)
        {
            if (objImporter.AllImported)
            {
                Camera.main.GetComponent<CameraController>().WhenLoadedCamera();
                ResultsLoader.WhenLoadedResults();
                firstLoad = false;
                secondLoad = true;
            }
        }

        if (secondLoad)
        {
            secondLoad = false;
            /*
            GameObject obj = loadedObject.GetComponentInChildren<GameObject>();
            //StartCoroutine(Wait());
            */
            Renderer rend = loadedObject.GetComponentInChildren<Renderer>();
            Instantiate(loadedObject.gameObject.transform.GetChild(0).gameObject, loadedObject.transform);

            rend.material.shader = Shader.Find("Unlit/Color");
            rend.material.color = Color.red;
            //LoadObject();
            progressText.enabled = false;
            loadButton.SetActive(false);

        }
        if (fpdone)
        {
            LoadObject();
            fpdone = false;
            firstLoad = true;
        }
    }

    public class CustomObjImporter : ObjectImporter
    {
        protected override void OnImportingComplete()
        {
            print("ON IMPORT COMPLETE");
            base.OnImportingComplete();

        }
    }

    public void LoadObject() {
        objImporter = loadedObject.AddComponent<ObjectImporter>();
        importOptions.buildColliders = true;
        objImporter.ImportModelAsync("loadedObject", filePath, loadedObject.transform, importOptions);
        
    }

    public static void SetFilepath(string fp)
    {
        filePath = fp;
        fpdone = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
}


