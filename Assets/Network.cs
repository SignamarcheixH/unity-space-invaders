using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{

	[Serializable]
	public struct Scores {

		public Score[] datas;
	}

	[Serializable]
	public struct Score {
		public int id;
		public String pseudo;
		public int score;
		public String uuid;
	}

	const String URL_REST_API = "https://api.r-c.es/m2dt-unity/asteroids";

	public Text leaderboard;
	public InputField pseudoField;
	public Text pseudoLabel;

	string playerUUID;
	string playerPseudo;


    // Start is called before the first frame update
    void Start()
    {
    	print( "Network : " + Application.internetReachability);
    	print( "Mobile : " + Application.isMobilePlatform);
    	print( "Platform : " + Application.platform);

    	playerUUID = PlayerPrefs.GetString("player-UUID");
    	if(playerUUID == "") {
    		Guid guid = Guid.NewGuid();
    		playerUUID = guid.ToString();
    		PlayerPrefs.SetString("player-UUID", playerUUID);
    	}

    	playerPseudo =  PlayerPrefs.GetString("player-pseudo");
		if(playerPseudo == "") {
			playerPseudo = "Hugues";
    		PlayerPrefs.SetString("player-pseudo", playerPseudo);
    	}
    	LoadScores();
    	pseudoField.onValueChanged.AddListener(delegate { PseudoFieldOnChange(); });    
    }

    public void PseudoFieldOnChange() {
    	playerPseudo = pseudoField.text;
    	PlayerPrefs.SetString("player-pseudo", playerPseudo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendScore(int score) {
    	StartCoroutine(SendScoresToNetwork(score));
    }

    IEnumerator SendScoresToNetwork(int score) {
    	WWWForm form = new WWWForm();
    	form.AddField("pseudo", playerPseudo);
    	form.AddField("uuid", playerUUID);
    	form.AddField("score", score);

    	UnityWebRequest request = UnityWebRequest.Post(URL_REST_API, form);
    	yield return request.SendWebRequest();
    	if(request.isNetworkError || request.isHttpError) {
    		Debug.Log("Error : " + request.error);
    	} else {
    		LoadScores();
    	}

    }

    public void LoadScores() {
    	StartCoroutine(LoadScoresFromNetwork());
    }

    IEnumerator LoadScoresFromNetwork() {
    	//demande de flux réseau
    	UnityWebRequest request = UnityWebRequest.Get(URL_REST_API);
    	//attendre le chargement
    	yield return request.SendWebRequest();
    	//si erreur
    	if(request.isNetworkError || request.isHttpError) {
    		Debug.Log("Error : " + request.error);
    		leaderboard.text = "Network error - no leaderboard";
    	} else {
    		string json = request.downloadHandler.text;
    		Scores root = JsonUtility.FromJson<Scores>(json);
    		for(int i = 0; i < root.datas.Length; i++ ) {
    			Score score = root.datas[i];
    			leaderboard.text += score.score.ToString("000 000")+ "  " + score.pseudo + '\n';
    		}
    	}
    	//sinon
    }
}
