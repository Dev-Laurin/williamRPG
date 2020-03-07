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
		hpText.text = unit.unit.hp + "/" + unit.unit.maxHP; 
		spText.text = unit.unit.sp + "/" + unit.unit.maxSP; 
		hpSlider.value = unit.unit.hp; 
		hpSlider.maxValue = unit.unit.maxHP; 
		spSlider.value = unit.unit.sp; 
		spSlider.maxValue = unit.unit.maxSP; 
	}
}
