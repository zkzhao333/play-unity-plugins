# Trivial Drive

A sample app for Play Billing Unity Plugin.

## Introduction

The purpose of this sample game is to provide devlopers some intuitions about how to use Google Play Billing Plugin.
To read more, please visit https://developer.android.com/google/play/billing/unity.

This game is a simple kart game that the player can drive the car, purchase gas, coins, new cars and subscriptions in the store, or switch cars and backgrounds in the garage.

## Pre-requisites

- [Unity IAP Documentation](https://docs.unity3d.com/Manual/UnityIAP.html)
- [Play Billing Unity Plug-In Document](https://developer.android.com/google/play/billing/unity)

## Getting Started
To run the sample game and make purchases, you need to follow the following steps.

1. Create a project folder and then create another folder named Assets inside your project folder. 
  After that, clone the repository and put the TrivialKart folder into the Assets folder. 
2. Open the project folder with Unity. Switch the platform to Android if you haven't at Unity IDE menu option **File > BuildSetting**.
3. Following the [instructions](https://docs.unity3d.com/Manual/UnityIAPSettingUp.html) to Enable IAP service. 
You may need to manually import the package at Assets/Plugins/UnityPurchasing/UDP.unitypackage and Assets/Plugins/UnityPurchasing/UnityIAP.unitypackage
(The auto import doesn't work when there are errors in the scripts).
4. Set up the Google Play Game Package Registry for Unity with [this method](https://developer.android.com/games/develop/build-in-unity#option_2_manually_edit_manifestjson).
    Then install Google Play Billing Library at **Window > Package Manager**. Follow [this instruction](https://developer.android.com/google/play/billing/unity#plugin-build-settings) to configure build settings.
    (note: you may need to restart Unity to make Google appear at the menu bar).

   GO TO GOOGLE PLAY DEVELOPER CONSOLE
5. Create an application on the Developer Console, available at https://play.google.com/apps/publish/.
6. Copy the application's public key (a base-64 string). You can find this in the "Services & APIs" section under "Licensing & In-App Billing".

    GO BACK TO UNITY EDITOR
7. Added Google Public Key to Obfuscated encryption keys. Check https://docs.unity3d.com/Manual/UnityIAPValidatingReceipts.html for instructions
(note: If you can't see Unity IAP under Window in menu bar, you may need to reinstall UDP at **Assets/UDPInstaller/Editor/UDP.unitypackage**).
8. Sign the App by creating a new key store at **File > Build Settings > Player Settings > Publishing Settings**. You can follow [this instruction](https://answers.unity.com/questions/326812/signing-android-application.html)
9. Build the game.

    GO BACK TO GOOGLE PLAY DEVELOPER CONSOLE
10. Upload your APK to Googole Play for Beta Testing. 

11. Under Monetize -> Products, create In-app products (note: if you are using Classic Play Console, create MANAGED in-app items under In-app Products) with these IDs and prices:
     
      | Product ID   |  Price|
      | :---:        | :---: |
      | car_jeep     | $2.99 |
      | car_kart     | $4.99 |
      | five_coins   | $0.99 |
      | ten_coins    | $1.99 |
      |twenty_coints | $2.49 |
      |fifty_coins   | $4.99 |

     Fill the other fields. Set them to "Active".

12. Create subscriptions with these IDs and prices: 

     | Product ID   |  Price|
     | :---:        | :---: |
     | silver_subscription   | $1.99 |
     | golden_subscription    | $4.99 |

    Fill the other fields. Set them to "Active".

13. Publish your APK to the Alpha channel. Wait 2-3 hours for Google Play to process the APK. If you don't wait for Google Play to process the APK, you might see errors where Google Play says that "this version of the application is not enabled for in-app billing" or something similar.

14. Added Tester and License testing to your game on Play Console.

15. Also always make sure that the uploaded application has the same version as the one you are testing.

    TEST THE GAME

16. Install the signed APK to a test device, and run the game.

17. Play the game and make purchases with the test account.


## A NOTE ABOUT SECURITY

This smaple app implements signature verifcation and reciept validation with obfuscated account id,
but does not demonstrate how to enforce a tight security model. When releasing a production application to the general public, we highly recommend that you implement the security best practices described in our documentation at:

http://developer.android.com/google/play/billing/billing_best_practices.html

In particular, you should perform a security check on your backend.

## Support
If you've found any errors or bugs in this sample game, please file an issue: https://github.com/google/play-unity-plugins/issues

Patches are encouraged, and may be submitted by forking this project and submitting a pull request through GitHub.

## License
Copyright 2020 Google LLC

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

https://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

## CHANGELOG
2020-8-7: Inital release




##Acknowledgements
Most of the game assets are collected from [Kenney](https://www.kenney.nl/). Thanks Kenney for the amazing assets.