using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ItemButtonManager : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private string itemPrice;
    private Sprite itemImage;
    private GameObject item3DModel;
    private ARInteractionManager interactionManager;
    public string urlBundleModel;
    private RawImage imageBundle;

    public string ItemName{
        set
        {
            itemName = value;
        }
    }
    
    public string ItemDescription
    {
        set
        {
            itemDescription = value;
        }
    }

     public string ItemPrice
    {
        set
        {
            itemPrice = value;
        }
    }

    public Sprite ItemImage
    {
        set
        {
            itemImage = value;
        }
    }
    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
        }
    }
   

    public string URLBundleModel {set => urlBundleModel = value;}

    public RawImage ImageBundle{get=> imageBundle; set => imageBundle = value; }

    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = itemName;
        transform.GetChild(1).GetComponent<Text>().text = itemDescription;
        //transform.GetChild(2).GetComponent<RawImage>().texture = itemImage.texture;
        imageBundle = transform.GetChild(2).GetComponent<RawImage>();
        transform.GetChild(3).GetComponent<Text>().text = itemPrice;

        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(Create3DModel);

        interactionManager = FindObjectOfType<ARInteractionManager>();
    }

    private void Create3DModel()
    {
        //interactionManager.Item3DModel = Instantiate(item3DModel);
        StartCoroutine(DownLoadAssetBundle(urlBundleModel));
    }
    IEnumerator DownLoadAssetBundle (string urlAssetBundle){
        UnityWebRequest serverRequest = UnityWebRequestAssetBundle.GetAssetBundle(urlAssetBundle);
        
        yield return serverRequest.SendWebRequest();

        if (serverRequest.result == UnityWebRequest.Result.Success){
            AssetBundle model3D = DownloadHandlerAssetBundle.GetContent(serverRequest);

            if (model3D != null){
                interactionManager.Item3DModel = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0])as GameObject);
            } else {
                Debug.Log("Not a valid Asset Bundle");
            }
        } else {
            Debug.Log ("Error :p");
        }
    }

}
