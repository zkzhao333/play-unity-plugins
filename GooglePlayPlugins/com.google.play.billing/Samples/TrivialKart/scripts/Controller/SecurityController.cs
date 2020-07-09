using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Server;

public class SecurityController
{

  public static ServerResponseModel registerUserDevice() {
    var values = new Dictionary<string, string> {};

    return sendUnityWebRequest(values, ServerURLs.REGISTER_URL);
  }

  public static ServerResponseModel verifyAndSaveUserPurchase(string purchaseToken) {
    var values = new Dictionary<string, string>
    {
      ["purchaseToken"] = purchaseToken
    };

    return sendUnityWebRequest(values, ServerURLs.VERIFY_AND_SAVE_TOKEN_URL);
  }

  public static ServerResponseModel verifyPurchaseWithApi(string packageName, string productId, string purchaseToken) {
    var values = new Dictionary<string, string>
    {
      ["packageName"] = packageName,
      ["productId"] = productId,
      ["purchaseToken"] = purchaseToken
    };
    return sendUnityWebRequest(values, ServerURLs.VERIFY_PURCHASE_WITH_API_URL);
  }
}
