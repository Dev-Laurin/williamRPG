using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHUD : MonoBehaviour {

	public Text hpText; 
	public Text spText; 
	public Slider hpSlider; 
	public Slider spSlider; 

	public void SetHUD(Stats stats, Sprite img){
		hpText.text = stats.hp + "/" + stats.maxHP; 
		spText.text = stats.sp + "/" + stats.maxSP; 
		hpSlider.value = stats.hp; 
		hpSlider.maxValue = stats.maxHP; 
		hpSlider.minValue = 0; 
		spSlider.value = stats.sp; 
		spSlider.maxValue = stats.maxSP; 
		spSlider.minValue = 0; 
		hpSlider.direction = Slider.Direction.RightToLeft;
		spSlider.direction = Slider.Direction.RightToLeft; 
		GetComponent<Image>().sprite = img; 
	}

	public void UpdateHUD(Stats unit, Sprite img = null){
		hpText.text = unit.hp + "/" + unit.maxHP; 
		spText.text = unit.sp + "/" + unit.maxSP; 
		hpSlider.value = unit.hp; 
		spSlider.value = unit.sp; 

		if(img){
			GetComponent<Image>().sprite = img; 
		}
		
	}

	public void SetActive(bool b){
		gameObject.SetActive(b); 
	}
}
