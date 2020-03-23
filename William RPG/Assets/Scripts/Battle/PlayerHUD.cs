using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHUD : MonoBehaviour {

	public Text hpText; 
	public Text spText; 
	public Slider hpSlider; 
	public Slider spSlider; 
	public Image image; 

	public void SetHUD(Unit unit){
		hpText.text = unit.hp + "/" + unit.maxHP; 
		spText.text = unit.sp + "/" + unit.maxSP; 
		hpSlider.value = unit.hp; 
		hpSlider.maxValue = unit.maxHP; 
		hpSlider.minValue = 0; 
		spSlider.value = unit.sp; 
		spSlider.maxValue = unit.maxSP; 
		spSlider.minValue = 0; 
		hpSlider.direction = Slider.Direction.RightToLeft;
		spSlider.direction = Slider.Direction.RightToLeft;  
	}

	public void UpdateHUD(Unit unit){
		hpText.text = unit.hp + "/" + unit.maxHP; 
		spText.text = unit.sp + "/" + unit.maxSP; 
		hpSlider.value = unit.hp; 
		spSlider.value = unit.sp; 
	}

	public void SetActive(bool b){
		gameObject.SetActive(b); 
	}
}
