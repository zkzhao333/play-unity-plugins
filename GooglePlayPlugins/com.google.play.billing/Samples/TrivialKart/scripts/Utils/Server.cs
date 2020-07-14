using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Server
{
    private static string userIdentification = SystemInfo.deviceUniqueIdentifier;

    public static ServerResponseModel sendUnityWebRequest(Dictionary<string, string> values, string url)
    {
        //adding user identification to all the requests to the server
        values.Add("userId", userIdentification);
        ServerResponseModel result = new ServerResponseModel();
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> entry in values)
        {
            form.AddField(entry.Key, entry.Value);
        }

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);

        uwr.SendWebRequest();
        while (!uwr.isDone)
        {
        }

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            result = JsonUtility.FromJson<ServerResponseModel>(uwr.downloadHandler.text);
        }

        return result;
    }
}