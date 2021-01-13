using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ServicioBaseDatos : MonoBehaviour
{
	public const string NOMBRE_BD = "BD.db"; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public static string getDatabasePath()
    {
		return "URI = file:" + getDatabasePath(NOMBRE_BD);
    }

	private static string getDatabasePath(string name)
	{
		string filePath = string.Format("{0}/{1}", Application.persistentDataPath, name);
		bool fileExists = File.Exists(filePath);

		switch (Application.platform)
		{
			default:
				{
					// alternatively implement an assumed fallback
					throw new NotSupportedException();
				}

			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.LinuxEditor:
				{
					return string.Format("Assets/StreamingAssets/{0}", name);
				}

			case RuntimePlatform.Android:
				{
					if (fileExists)
					{
						return filePath;
					}

					// this is the path to your StreamingAssets in android
					string path = string.Format("jar:file://{0}!/assets/{1}", Application.dataPath, name);
					var req = UnityWebRequest.Get(path).SendWebRequest();

					// NOTE: may want to add some checks to this
					while (!req.isDone) { }

					File.WriteAllBytes(filePath, req.webRequest.downloadHandler.data);
					break;
				}

			case RuntimePlatform.IPhonePlayer:
				{
					if (fileExists)
					{
						return filePath;
					}

					// this is the path to your StreamingAssets in iOS
					string path = string.Format("/{0}Raw/{1}", Application.dataPath, name);
					File.Copy(path, filePath);
					break;
				}
		}

		return filePath;
	}
}
