using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReneverseManager : MonoBehaviour
{
    public GameObject Email;
    public TextMeshProUGUI Timer;
    public GameObject SignInPanel;
    public GameObject CountdownPanel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    async Task ConnectUser()
    {
        ReneAPICreds _reneAPICreds = ScriptableObject.CreateInstance<ReneAPICreds>();
        _reneAPICreds.APIKey = "e617d1c2-4efe-4203-8a2d-088df7c550ac";
        _reneAPICreds.PrivateKey = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCZK2pGrbCDuWq0/pwFBefifh5CUpgulJwN4EXZ7fUmkcHNCoyiI42U0bWAGtHlFPzESwqzXSZR/wWkYiMmfpDydrfaZksYW6w4zvaacVjpnOaCuS6m0Cm9zRdY1ovX55XQ4YdMuYRZ1LBtr+kLOyoQQAZReJyRl6NbvTIsx5jLbneNTMONbNxM2HDhqLDSZSJXkFsdzkhBtQ1LaJwlC3NQibwuRX2885TxwNUcahDuih9snPGReruacx4ZwMX16sumb3zU7jfRahxYgcF2cRIKmAoTDSnn5BqQkBaYrCzrMUnCfWaKMcadzWOXqgwZrKMCpDvdZa19eej12UQmvNjnAgMBAAECggEACg1jrEYCHSSbxuTxpJGM9K9JP7OmLpYGbd5OFBQXydNSdqkrR9YK5ZbwkS4LBqLredutu2eXUbwc1ZTmCkTPl3h7u91VyI0pQhGXKDvcu99vvQMq3bv9cuoh9TmGmptYy9YmG1hB9UrnooAJsxJ/DrgpxTDXIBj/Gbcff8ZAfJOzFaWWeWmPbUDr2VAgcXV5zoVydj2CfYRPzng3kZukz8S9bJtRNX4i7n51hG8BlcpOaBaD1smvgJFywxkTiJlwFMEHBNTx1/ToSX52QJ3BnU+rUxfOQl3k2X3sTDs0YjmApwcVcSf584sSQeqGLGJ0/TpkmMnyV3htv8TsankgiQKBgQDOSoBNdq33UUPKP5bxMOlIAGokkQMTW7DTI7qhIowh9cc951l/YqKgJinN+qcUzqFoLLn6JxZVXTyZNKs2ZnLf0qCBy7+AUjSjArYTXQqb/BcW4hs1QZG9pPAzdyH7N9JuQMtAqKV9HMYpwtefmhqiiMWIRyKhaEWb2ZYlKSWJiQKBgQC+FAGHGwLdE+Q/FCh2v/Tsgw9GdFIr7WQQd/JIcX6q+PjJm7kV4V0XQV41sEezX70KXPdzl16mfktxxj6rTEvXlEtp/SfeFuE6LDxv8zlLumvXmBvXrVqR26g1bPUAU8586QUVn7TNPNpiPW9P88HABeov9FF3jO4LBRfDCfNi7wKBgGiPdozM9MyAkj23EYja48MtAp/aKJbtSKkcWQJHgoPMEdscok5g7lECRvoya/Gt8j3dPb6/hSBri8WT3pxKPTuZhOWFImGmSSu+ug8Cf9gkZIeiv2u0+mwHaACOB9lPqAdeLCdv08Ggjgioy6YH9Cwh6w1yEOmC8pVWKjZXrsERAoGBAIegqNJpoKp1JhkoXhMVt0MH5V9lYri7Y/ooTDYK3dJLYuIgfnmxXAZa+0kd5puERdReL6dILB5q4ZRmW5NJFpjV1NXk8IyVENK8e8d56rkxZP/qJnvH02deL/EnNM6t/hm8/4bFdXI46K7OnV2UVfyZe9gJ4hOG+NfeI21k7Uj7AoGAM/5Gmz8ev8qupPPl+EwdU/4A/g/kPoQXdDHNY2pJOAF1kuLK3Lhmmq7WCrMiFC0/AFPBvgaQNVUdwHTN/ulxPDENr5m++TBBwSmfhkd6UiTUzOc/H+G7nQdo7eP1K+rTe9xwqOwjgXZNp9kzB9Ocxjq0t346KAtaZa4bQzEonvQ=";
        _reneAPICreds.GameID = "2d9bcf87-1bc8-41df-b6b1-db40cf1ceab4";
        var ReneAPI = API.Init(_reneAPICreds.APIKey, _reneAPICreds.PrivateKey, _reneAPICreds.GameID);
        string input = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(input);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ConnectReneService(API reneApi)
    {
        CountdownPanel.SetActive(true);
        var counter = 30;
        var userConnected = false;
        //Interval how often the code checks that user accepted to log in
        var secondsToDecrement = 1;
        while (counter >= 0 && !userConnected)
        {
            Timer.text = counter.ToString();
            if (reneApi.IsAuthorized())
            {

                CountdownPanel.SetActive(false);
                SignInPanel.SetActive(false);
                //Here can be added any extra logic once the user logged in



                yield return GetUserAssetsAsync(reneApi);
                userConnected = true;
            }

            yield return new WaitForSeconds(secondsToDecrement);
            counter -= secondsToDecrement;
        }
        CountdownPanel.SetActive(false);
    }

    private async Task GetUserAssetsAsync(API reneApi)
    {
        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();
        //By this way you could check in the Unity console your NFT assets
        userAssets?.Items.ForEach
        (asset => Debug.Log
            ($" - Asset Id '{asset.NftId}' Name '{asset.Metadata.Name}"));
        userAssets?.Items.ForEach(asset =>
        {
            string assetName = asset.Metadata.Name;
            string assetImageUrl = asset.Metadata.Image;
            string assetStyle = "";
            asset.Metadata?.Attributes?.ForEach(attribute =>
            {
                //Keep in mind that this TraitType should be preset in your Reneverse Account
                if (attribute.TraitType == "Style")
                {
                    assetStyle = attribute.Value;
                }
            });
            //An example of how you could keep retrieved information
            Asset assetObj = new Asset(assetName, assetImageUrl, assetStyle);
            //one of many ways to add it to the game logic 
            //_assetManager.userAssets.Add(assetObj);
        });
    }

}
public class Asset
{
    public string AssetName { get; set; }
    public string AssetUrl { get; set; }

    public string AssetStyle { get; set; }
    public Asset(string assetName, string assetUrl, string assetStyle)
    {
        AssetName = assetName;
        AssetUrl = assetUrl;
        AssetStyle = assetStyle;
    }
}
