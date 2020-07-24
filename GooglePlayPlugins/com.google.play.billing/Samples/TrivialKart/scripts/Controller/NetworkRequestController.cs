// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using UnityEngine;
using static Server;

public class NetworkRequestController
{
    private const string REGISTER_URL = "https://us-central1-simpleserver-d2cf5.cloudfunctions.net/register_user";

    private const string VERIFY_AND_SAVE_TOKEN_URL =
        "https://us-central1-simpleserver-d2cf5.cloudfunctions.net/verify_and_save_purchase_token";

    private const string VERIFY_PURCHASE_WITH_API_URL =
        "https://us-central1-simpleserver-d2cf5.cloudfunctions.net/verify_purchase_play_dev_api";

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

    public static ServerResponseModel verifyAndSaveUserPurchase(string purchaseToken)
    {
        var values = new Dictionary<string, string>
        {
            ["purchaseToken"] = purchaseToken
        };

        return sendUnityWebRequest(values, VERIFY_AND_SAVE_TOKEN_URL);
    }

    public static ServerResponseModel verifyPurchaseWithApi(string packageName, string productId, string purchaseToken)
    {
        var values = new Dictionary<string, string>
        {
            ["packageName"] = packageName,
            ["productId"] = productId,
            ["purchaseToken"] = purchaseToken
        };
        return sendUnityWebRequest(values, VERIFY_PURCHASE_WITH_API_URL);
    }
}
