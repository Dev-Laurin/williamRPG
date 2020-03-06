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

	public void SetHUD(BattleUnit unit){
		hpText.text = unit.hp + "/" + unit.maxHP; 
		spText.text = unit.sp + "/" + unit.maxSP; 
		hpSlider.value = unit.hp; 
		hpSlider.maxValue = unit.maxHP; 
		spSlider.value = unit.sp; 
		spSlider.maxValue = unit.maxSP; 
	}
}
