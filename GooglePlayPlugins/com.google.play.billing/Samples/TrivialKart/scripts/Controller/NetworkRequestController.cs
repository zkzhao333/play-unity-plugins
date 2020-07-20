using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using static Server;

public class NetworkRequestController
{
    private const string REGISTER_URL = "https://us-central1-simpleserver-d2cf5.cloudfunctions.net/register_user";

    private const string VERIFY_AND_SAVE_TOKEN_URL =
        "https://us-central1-simpleserver-d2cf5.cloudfunctions.net/verify_and_save_purchase_token";

    private const string SAVE_GAME_DATA_URL =
        "https://us-central1-simpleserver-d2cf5.cloudfunctions.net/save_game_data";

    private const string GET_GAME_DATA_URL = "https://us-central1-simpleserver-d2cf5.cloudfunctions.net/get_game_data";

    public static ServerResponseModel registerUserDevice()
    {
        var values = new Dictionary<string, string> { };
        return sendUnityWebRequest(values, REGISTER_URL);
    }

    public static ServerResponseModel SaveGameDataOnline()
    {
        var values = new Dictionary<string, string>
        {
            ["gameData"] = JsonUtility.ToJson(GameDataController.GetGameData())
        };

        return sendUnityWebRequest(values, SAVE_GAME_DATA_URL);
    }

    public static void LoadGameOnline()
    {
        ServerResponseModel serverResponse =
            sendUnityWebRequest(new Dictionary<string, string>(), GET_GAME_DATA_URL);
        if (serverResponse.success)
        {
            Debug.Log(serverResponse.result);
            GameDataController.SetGameData(JsonUtility.FromJson<GameData>(serverResponse.result));
        }
        else
        {
            GameDataController.SetGameData(new GameData());
        }
    }

    public static void verifyAndSaveUserPurchase(Product product)
    {
        var values = new Dictionary<string, string>
        {
            ["receipt"] = getReceiptDetails(product.receipt)
        };

        ServerResponseModel serverResponse = sendUnityWebRequest(values, VERIFY_AND_SAVE_TOKEN_URL);
        PurchaseController.ConfirmPendingPurchase(product, serverResponse.success);
    }

    private static string getReceiptDetails(string receipt)
    {
        receipt = String.Join("", receipt.Split( '\\'));
        receipt = receipt.Remove(0,receipt.IndexOf("orderId")-2);
        return receipt.Remove(receipt.IndexOf("signature")-3);
    }
}
