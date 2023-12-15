using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMeshPro namespace

public class Time_Manager : MonoBehaviour
{
    public float currentTime;
    public float playBackSpeed = 1;
    public float[] playBackSpeedArray; 
    private int playBackSpeedCounter = 0;
    public TextMeshProUGUI playBackSpeedDisplay;
    public TextMeshProUGUI timeDisplay;
    public Vector2 minMaxTime;
    private bool tempminMaxTime;
    public Slider timeSlider;
    public Button playPauseButton;
    public Sprite playSprite;
    public Sprite pauseSprite;
    public Sprite stopSprite;
    private bool pauseActive = true;   
    private bool tempPauseActive = false;

    void Update(){
        StepTime();
        UpdateSliderValues();
        UpdateButtonMod();
        UpdateTimeDisplay();
    }
    public void UpdateSliderValues(){
            timeSlider.minValue = minMaxTime.x;
            timeSlider.maxValue = minMaxTime.y;    
    }
    public void UpdateTimeDisplay(){
        timeDisplay.text = "  " + currentTime.ToString("F3") + " [sec]";  
    }
    void UpdateButtonMod()
    {
        if (tempPauseActive != pauseActive)
        {
            if (pauseActive)
            {
                playPauseButton.image.sprite = playSprite;
            }

            else
            {
                playPauseButton.image.sprite = pauseSprite;
            }          
            tempPauseActive  = pauseActive;
        }      
    }
    public void ChangeButtonMod(){
        if (pauseActive){
            pauseActive = false;
        }
        else{
            pauseActive = true;
        }
    }
    public void ChangePlaybackSpeed(){
        playBackSpeedCounter++;
        if (playBackSpeedCounter == playBackSpeedArray.Length)
        {
            playBackSpeedCounter = 0;
        }
        playBackSpeed = playBackSpeedArray[playBackSpeedCounter];
        playBackSpeedDisplay.text = playBackSpeed + "x";
    }
    public void StepTime(){
        if (pauseActive)
        {    
            currentTime = timeSlider.value;
        }
        else
        {
            timeSlider.value += Time.deltaTime*playBackSpeed;
            currentTime = timeSlider.value;
        }    
    }
}
