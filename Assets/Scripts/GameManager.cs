﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public List<GameObject> levels = new List<GameObject>();
	public List<GameObject> level_buttons = new List<GameObject>();
	public List<string> completed_levels = new List<string>();
	[HideInInspector] public int numUnlockedLevels;
	[HideInInspector] public bool loadedLevels = false;
	[HideInInspector] public float minRangeY = 0f;
	[HideInInspector] public float maxRangeY = 0f;
	[HideInInspector] public float minRangeX = 0f;
	[HideInInspector] public float maxRangeX = 0f;
	[HideInInspector] public bool startedLevel = false;


	bool buttonsInit = false;
	// Use this for initialization
	void Awake () {
		if (gm == null)
			gm = this;
		else
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	public void LoadLevelScene()
	{		
		for(int i = 0; i < 3; i++)
		{
			var spotlight = levels[i].transform.GetChild(0);
			spotlight.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (loadedLevels)
			GetLevels();
		if (levels.Count > 0 && !startedLevel)
			LoadLevelScene();
		if (level_buttons.Count > 0 && !startedLevel)
			InitButtons();
		if (!startedLevel)
			ActivateSpotLights();
	}

	void  InitButtons()
	{
		foreach (var button in level_buttons)
		{
			Button b = button.GetComponent<Button>();
			if (button.transform.tag == "lvl_1")
				b.onClick.AddListener(LoadLevel1);
			else if (button.transform.tag == "lvl_2")
				b.onClick.AddListener(LoadLevel2);
			else if (button.transform.tag == "lvl_3")
				b.onClick.AddListener(LoadLevel3);
		}
		buttonsInit = true;
	}

	public void GetLevels()
	{
		levels.Clear();
		GameObject level = GameObject.FindWithTag("Level_Select");
		if (level != null && levels.Count == 0)
		{
			foreach (Transform child in level.transform)
			{
				if (child.tag == "lvl_1" || child.tag == "lvl_2" || child.tag == "lvl_3")
					levels.Add(child.gameObject);
			}
		}
		level_buttons.Clear();
		GameObject level_button = GameObject.FindWithTag("Level_Buttons");
		if (level_button != null && level_buttons.Count == 0)
		{
			foreach(Transform child in level_button.transform.GetChild(0))
			{
				if (child.tag == "lvl_1" || child.tag == "lvl_2" || child.tag == "lvl_3")
					level_buttons.Add(child.gameObject);
			}
		}
	}

	void ActivateSpotLights()
	{
		for(int i = 0; i < numUnlockedLevels; i++)
		{
			if (i < levels.Count)
			{
				var spotlight = levels[i].transform.GetChild(0);
				spotlight.gameObject.SetActive(true);
			}
		}
	}

	public void LoadLevel1()
	{
		Debug.Log("Level 1");
		startedLevel = true;
		minRangeY = 85f;
		maxRangeY = 105f;
		SceneManager.LoadScene("lvl_1");
	}

	public void LoadLevel2()
	{
		Debug.Log(completed_levels.Count);
		if (completed_levels.Count >= 1)
		{
			startedLevel = true;
			minRangeY = 70f;
			maxRangeY = 80f;
			minRangeX = 85;
			maxRangeX = 100f;
			SceneManager.LoadScene("lvl_2");
		}
	}

	public void LoadLevel3()
	{
		if (numUnlockedLevels >= 2)
		{
			Debug.Log("Level 3");
		}
	}
}
