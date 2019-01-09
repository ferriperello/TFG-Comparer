using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ResultsLoader : MonoBehaviour {

    private static string filePath;
    private static bool fpdone = false;

    public GameObject loadedObj;
    private static bool loaded = false;

    public GameObject cube;
    public static ArrayList cubes;

    public static bool[] findedTriangles;
    private int[] nts1;
    private int[] nts2;
    public int[] ots;
    public int nVertex;
    public int nTriangles;

    // Update is called once per frame
    void Update () {
        if (fpdone)
        {
            LoadResults();
            fpdone = false;
        }
        if (loaded)
        {
            Initialize();
        }
    }

    public static void WhenLoadedResults()
    {
        loaded = true;
    }

    private void Initialize()
    {
        loaded = false;
       
        //Renderer rend = loadedObj.GetComponentInChildren<Renderer>();
        //GameObject obj = loadedObj.GetComponentInChildren<GameObject>().gameObject;
        //Instantiate(loadedObj, loadedObj.transform);
        //rend.material.shader = Shader.Find("Unlit/Color");
        //rend.material.color = Color.red;

        //amb el objecte ja carregat
        //aixo haurien de ser copies??
        ots = loadedObj.GetComponentInChildren<MeshFilter>().mesh.triangles;
        nVertex = ots.Length;
        nTriangles = nVertex / 3;
        findedTriangles = Enumerable.Repeat(false, nTriangles).ToArray<bool>();
        nts1 = new int[nTriangles * 3];
        nts2 = new int[nTriangles * 3];
        cubes = new ArrayList();
    }

    public static void SetResultsPath(string fp)
    {
        filePath = fp;
        fpdone = true;
    }

    private void LoadResults()
    {
        StreamReader reader = new StreamReader(filePath);

        string line = reader.ReadLine();
        string[] aux = line.Split(' ');

        int trianglesFound = int.Parse(aux[0]);

        line = reader.ReadLine();
        aux = line.Split(',');

        for(int i = 0; i < trianglesFound; i++)
        {
            findedTriangles[int.Parse(aux[i])] = true;
        }

        nts2 = new int[nTriangles * 3];
        int painted = 0;
        int j = 0;
        int k = 0;
        for (int i = 0; i < nTriangles; i++)
        {
            if (findedTriangles[i])
            {
                painted++;
                nts2[j] = ots[i * 3];
                nts2[j + 1] = ots[(i * 3) + 1];
                nts2[j + 2] = ots[(i * 3) + 2];
                j += 3;
            }
            else
            {
                nts1[k] = ots[i * 3];
                nts1[k + 1] = ots[(i * 3) + 1];
                nts1[k + 2] = ots[(i * 3) + 2];
                k += 3;
            }
        }

        MeshFilter[] meshes = loadedObj.GetComponentsInChildren<MeshFilter>();
        meshes[1].mesh.triangles = nts2;
        meshes[0].mesh.triangles = nts1;


        //Carreguem les caixes

        line = reader.ReadLine();
        aux = line.Split(' ');

        int numBoxes = int.Parse(aux[0]);

        for (int i = 0; i < numBoxes; i++)
        {
            line = reader.ReadLine();
            aux = line.Split(',');

            GameObject newCube = Instantiate(cube, new Vector3(0, 0, 0), Quaternion.identity);
            newCube.transform.localScale = new Vector3(float.Parse(aux[4]), float.Parse(aux[5]), float.Parse(aux[6]));
            newCube.transform.position = new Vector3(float.Parse(aux[1]), float.Parse(aux[2]), float.Parse(aux[3]));
        }
    }
}
