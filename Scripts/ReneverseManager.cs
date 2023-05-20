using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReneverseManager : MonoBehaviour
{
    //Global Stats
    public static bool LoginStatus = false;
    public static Dictionary<string, bool> SkinStats = new Dictionary<string, bool>();
    public static string EmailHandler;

    public GameObject Email;
    public TextMeshProUGUI Timer;

    public GameObject SignInPanel;
    public GameObject CountdownPanel;

    ReneAPICreds _reneAPICreds;
    API ReneAPI;
    NotificationManager NotificationManager;
    // Start is called before the first frame update
    void Start()
    {
        NotificationManager = NotificationManager.notificationManager;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            NotificationManager.Notify("WebGL version lacks login to reneverse, Kindly continue with Guest Mode");
        }

        _reneAPICreds = ScriptableObject.CreateInstance<ReneAPICreds>();
        SkinStats["Cube"] = true;
        if (LoginStatus)
            SignInPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    public void SignUp()
    {
        Application.OpenURL("https://app.reneverse.io/register");
    }

    public async void MintCars(string CarName)
    {
        await Mint(CarName);
    }

    public void GuestMode()
    {
        SignInPanel.SetActive(false);
    }

    async Task ConnectUser()
    {
        _reneAPICreds.APIKey = "0616b7fe-f0fa-43ac-a57d-9d826642834e";
        _reneAPICreds.PrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDzSvN8kto5IVkbl9JM/ZiuvFqTuU6c/tVWBiO7YLnDwdeqpDVtTgjZo3X4G3wsj3AdVxKcAW24Budk/vTMWnfr9IFH5m0zxfH0vhHUVG3uPJ1A1jwUNlLE7FmsivvrqHlbQ/NIOZf1DDpdJaLBUZgjVVJ0vc6Iqm8hNkDdrqAccEfeq2RKBCCUYPi8xvXz4i1RMXHChRr73mIV4r4WJEzkebbz992SVxOgni44/aFGWffPcMLJoXXl6ICMccIJNRpZALbUGiHl10V1MnF9PFppyvLk6MLfA9p9ojiKF4wCeOxkSTa+Epw0bqLymhAOqL2hqcZG98pKaEowEuJoSQAdAgMBAAECggEAOw0vKkZupz075qGkDsHi5E6dYYux1BNabqXQ2HMyw5vyH935hc1SAplVUeJB8oLiQIzY3HrQScGLOo1Tl6JBx0iydGQuj0l1X+UeaL4RbKjTtmAJdxJ0Zo3DekjFur5KrmdAzoAELRtJs0AmT+vhFHpqKCHF1pAfpx0HA4eWHzB8ZaoFnP7aaqFopmebAftqTV7h1dlXkXjfvweMZMk122qzui2Qqxed9MTPcIIRVHTNPp/xWqCqCyrw4Ag0ehGxuvj5PrpCUqm+FeQ8C6C5KSAMYzeaE51Ql62mLucYsZQ9RO39/5h/bcpf9vuAa+hCHF2pkJrzKOnONx69LNvoYQKBgQD+Mc/J8mq5NlhOSuuX7NTdWfzYrdqDec+MDSM0snkuA4DxVnNB/CuFjqanDLZIN6FEtuMQXMwwSCbuqp3imum1iX3rAp6XEXu8naDsG8psllKrl4suVaJnPMQ4tgfJwLXvb5JV4EgTc622IXC7PmLya2+660/aYYbzAVvnL/vyaQKBgQD1BVE55K8PHl7f2JwtamQA8yWcj668vELc7HuyEYRr4FXKUQlqBiT4z/y08BjGmQsadTeUrNIbV1jdVEBeBKt5uLHuP0fdeJtyMDDAZdu/Wa/G4zZzvAluaMqXvhbg77fF+kdNkZzHpFHJeEkPr2ouVoe2gjEXEUaNAB/sLc6BlQKBgAcx477ElM6/QgqdRkPbmT7WsDh120yDYyOEr61rK9Domnq6RrLkb1rtabwquPIcWP036/9nkQQA1tFElQl39wuDY8QGI/UEsqrpD0f/lWAzdQ2UUYUzOVCQwMEWLexA/yVS1CKIIaIjURRpp+Y04toXvmbdCDqXLhmsvSwzCH+ZAoGBAKsm+rU5A/vImDc+5OFohtCPB//T8hhOXVpbKpCZYenE+8hmUPApuJvBFWICsRvQ/guOQ7PsAJwuqJl6Z7gFBQ7yr/+fXoDa5aKe/P74Z8bDTGDeiEPR3risJJBYrTyU1sdJa5NImr5uDt9v0YFOZBpYQVaAnO/jFmgZ5TKiULT9AoGBAMMI51dt77vLbKqHcTAL8JU6ChOdJhLLuAMIZDE7eYn2WFBuQXqA8Ay7rVUYjTW7goDCBQogvJEbPObZACJ+gRnB+9Fp7cvvNr5cgFppRK40aacGrRZF9HtCsxtzVQ5Olp9JunS2+Ic6AyGyqihdWhXL5SxjNceE6QQDPUzQYVt8";
        _reneAPICreds.GameID = "dc58103e-544b-466b-8f6c-5d27d87584b8";
        ReneAPI = API.Init(_reneAPICreds.APIKey, _reneAPICreds.PrivateKey, _reneAPICreds.GameID);
        EmailHandler = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(EmailHandler);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
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


                yield return GetUserAssetsAsync(ReneAPI);

                userConnected = true;
                LoginStatus = true;
                NotificationManager.Notify("Connected to Reneverse");
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
        userAssets?.Items.ForEach(asset =>
        {
            SkinStats[asset.Metadata.Name.ToString()] = true;
        });
    }

    public async Task Mint(string CarName)
    {
        if (LoginStatus == false)
        {
            NotificationManager.Notify("Currently in Guest Mode, Restart to Log In");
            return;
        }
        if (CarName == "Snake" && !SkinStats.ContainsKey("Snake"))
        {
            //Asset Template ID
            string assetTemplateId = "fab7b93f-239a-4651-a119-165d10df38ca";

            //Color Attribute
            AssetsResponse.AssetsData.Asset.AssetMetadata.AssetAttribute Color = new()
            {
                TraitType = "Color",
                Value = "Green"
            };

            //Type Attribute
            AssetsResponse.AssetsData.Asset.AssetMetadata.AssetAttribute Type = new()
            {
                TraitType = "Type",
                Value = "Creature"
            };

            // Adding Toyoyo's Metadata
            var assetMetadata = new AssetsResponse.AssetsData.Asset.AssetMetadata()
            {
                Name = "Snake",
                Description = "A snake skin might be enough to crawl you out to the next level",
                Image = "https://files.reneverse.io/asset_template/metadata-images/fab7b93f-239a-4651-a119-165d10df38ca/92f655b2-0fb1-41c2-b137-261db6199b6b/SnakeRender.png",
                AnimationUrl = null,
                Attributes = new List<AssetsResponse.AssetsData.Asset.AssetMetadata.AssetAttribute>()
                {
                    Color,
                    Type
                },
            };


            //Testnet Booleon
            bool isTestnet = true;

            try
            {
                var Response = await ReneAPI.Game().AssetMint(assetTemplateId, assetMetadata, isTestnet);
                Debug.Log(Response);
                SkinStats["Snake"] = true;
                NotificationManager.Notify("Asset Minting in progress");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                NotificationManager.Notify("There was a problem minting this asset");
            }

        }

        if (CarName == "Robot" && !SkinStats.ContainsKey("Robot"))
        {

            //Asset Template ID
            string assetTemplateId = "fab7b93f-239a-4651-a119-165d10df38ca";

            //Color Attribute
            AssetsResponse.AssetsData.Asset.AssetMetadata.AssetAttribute Color = new()
            {
                TraitType = "Color",
                Value = "Yellow"
            };

            //Type Attribute
            AssetsResponse.AssetsData.Asset.AssetMetadata.AssetAttribute Type = new()
            {
                TraitType = "Type",
                Value = "Machine"
            };

            // Adding Tristar's Metadata
            var assetMetadata = new AssetsResponse.AssetsData.Asset.AssetMetadata()
            {
                Name = "Robot",
                Description = "This Robot skin is playbable in both the games",
                Image = "https://files.reneverse.io/asset_template/metadata-images/fab7b93f-239a-4651-a119-165d10df38ca/b0b4fc7d-dfae-40b2-9aad-a63859197437/RobotRender.png",
                AnimationUrl = null,
                Attributes = new List<AssetsResponse.AssetsData.Asset.AssetMetadata.AssetAttribute>()
                {
                    Color,
                    Type
                },
            };

            //Testnet Booleon
            bool isTestnet = true;

            try
            {
                var Response = await ReneAPI.Game().AssetMint(assetTemplateId, assetMetadata, isTestnet);
                Debug.Log(Response);
                SkinStats["Robot"] = true;
                NotificationManager.Notify("Asset Minting in progress");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                NotificationManager.Notify("There was a problem minting this asset");
            }
        }
    }

    //Car TemplateId : 099492f0-a5e6-4030-a165-c59cafcabdc2
    //Cube TemplateId : fab7b93f-239a-4651-a119-165d10df38ca
}