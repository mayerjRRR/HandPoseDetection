﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PostureMenu : MonoBehaviour {

	public InputField fileName;
	public Dropdown dropDown;
	public Button next;
	TrainingUnit.Posture current;
	List<TrainingUnit> currentList;
    public HandObserver observedHand;
	int index;
	public Transform root, thumb1, thumb2, thumb3, index1, index2, index3, middle1, middle2, middle3, ring1, ring2, ring3, pinky1, pinky2, pinky3;
	// Use this for initialization
	void Start () {
		dropDown.ClearOptions ();
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData> ();
		foreach (string pose in Enum.GetNames(typeof(TrainingUnit.Posture))) {
			list.Add (new Dropdown.OptionData(pose));
		}
		dropDown.AddOptions (list);
		onChangePose ();
	}
	
	// Update is called once per frame
	void Update () {
        HandObserver.AngleBasedHandModel currentHand = currentList[index].hand;
        if (Input.GetButton("Fire2"))
        {
            currentHand = observedHand.hand;
            Debug.Log("Observing");
        }

      //  root.rotation = currentHand.rotation;

        thumb1.localRotation = currentHand.thumb.tmc.mirroredX().mirroredY();
        thumb2.localRotation = Quaternion.Euler(-currentHand.thumb.jointAngles[0], 0, 0);
        thumb3.localRotation = Quaternion.Euler(-currentHand.thumb.jointAngles[1], 0, 0);

        index1.localRotation = currentHand.fingers[0].mcp.mirroredX().mirroredY();
        index2.localRotation = Quaternion.Euler(-currentHand.fingers[0].jointAngles[0], 0, 0);
        index3.localRotation = Quaternion.Euler(-currentHand.fingers[0].jointAngles[1], 0, 0);

        middle1.localRotation = currentHand.fingers[1].mcp.mirroredX().mirroredY();
        middle2.localRotation = Quaternion.Euler(-currentHand.fingers[1].jointAngles[0], 0, 0);
        middle3.localRotation = Quaternion.Euler(-currentHand.fingers[1].jointAngles[1], 0, 0);

        ring1.localRotation = currentHand.fingers[2].mcp.mirroredX().mirroredY();
        ring2.localRotation = Quaternion.Euler(-currentHand.fingers[2].jointAngles[0], 0, 0);
        ring3.localRotation = Quaternion.Euler(-currentHand.fingers[2].jointAngles[1], 0, 0);
         //   Debug.Log(currentHand.fingers[0].ToString());

        pinky1.localRotation = currentHand.fingers[3].mcp.mirroredX().mirroredY();
        pinky2.localRotation = Quaternion.Euler(-currentHand.fingers[3].jointAngles[0], 0, 0);
        pinky3.localRotation = Quaternion.Euler(-currentHand.fingers[3].jointAngles[1], 0, 0);
	}

	public void changeFileName()
	{
		DataHandler.instance.fileName = fileName.text;
		DataHandler.instance.loadData ();
	}
	public void onNext()
	{
		index++;
		index %= currentList.Count;
	}

	public void onBack()
	{
		index--;
		index %= currentList.Count;
	}

	public void onDelete()
	{
		DataHandler.instance.delete (currentList[index]);
		currentList.RemoveAt (index);
		index %= currentList.Count;
	}

	public void onChangePose()
	{
		current = (TrainingUnit.Posture)dropDown.value;
		currentList = DataHandler.instance.getSublist (current);
		index = 0;
		Debug.Log("Got "+currentList.Count+" elements.");
	}

	public void onDeleteAll()
	{
		DataHandler.instance.deleteAll (current);
	}
}
