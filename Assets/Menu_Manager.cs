using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using SimpleFileBrowser;

public class Menu_Manager : MonoBehaviour {

    private static string filePath;

	// Use this for initialization
	void Start () {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Objects", ".obj"));
        FileBrowser.SetDefaultFilter(".obj");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

    }
	
	// Update is called once per frame
	void Update () {
	}

    public void OpenOBJBrowser()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
        
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Objects", ".obj"));
        FileBrowser.SetDefaultFilter(".obj");

        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load OBJ File", "Load");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
        if (FileBrowser.Success)
        {
            filePath = FileBrowser.Result;
            ObjectLoader.SetFilepath(filePath);
        }
        
    }

    public void OpenResultsLoader()
    {
        StartCoroutine(ShowSaveDialogCoroutine());
    }

    IEnumerator ShowSaveDialogCoroutine()
    {

        FileBrowser.SetFilters(true, new FileBrowser.Filter("txt", ".txt"));
        FileBrowser.SetDefaultFilter(".txt");

        // Show a save file dialog and wait for a response from user
        // save file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load Results File", "Load");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
        if (FileBrowser.Success)
        {
            filePath = FileBrowser.Result;
            ResultsLoader.SetResultsPath(filePath);

        }
    }
}

