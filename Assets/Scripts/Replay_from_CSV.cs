    using System;
    using System.Collections;
    using System.Globalization;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;
    using System.IO;
    using TMPro; // Import the TextMeshPro namespace

    public class Replay_from_CSV : MonoBehaviour
    {
        // public Vector3 phiThetaPsiDeg;
        public GameObject aircraft;
        // public TextAsset csvFile; 
        public TMP_InputField inputField;
        public TextMeshProUGUI timeTMP;
        public Slider slider;
        public Button playPauseButton;
        public Sprite playSprite;
        public Sprite pauseSprite;
        public Sprite stopSprite;
        private bool pauseActive = true;
        private bool tempPauseActive = false; 
        private float sliderTime_s;
        private float[] time_s;
        private Vector3[] xyz_m;
        private Vector3[] phiThetaPsiDeg;
        private int selected_idx;
        private string[] lines;
        private float min_delta_time;
        private float delta_time;
        void Start()
        {
            // inputField.text = "C:/Users/cyber/Replay_Rocket/Assets/Data/Replay_Deneme.csv";
            inputField.text = "Assets/Data/hava-kara/hava2kara_m1.csv";
            UpdateCSVList();   
        }
        void Update()
        {
            min_delta_time = 10000000f;
            for (int i = 0; i < time_s.Length; i++)
            {
                delta_time = Mathf.Abs(time_s[i] - sliderTime_s);

                if (delta_time<min_delta_time)
                {
                    min_delta_time = delta_time;
                    selected_idx = i;
                }
            }
            aircraft.transform.position = new Vector3(xyz_m[selected_idx].y,xyz_m[selected_idx].z*(-1f),xyz_m[selected_idx].x);
            aircraft.transform.rotation =  Quaternion.Euler(phiThetaPsiDeg[selected_idx].y*(-1f),  phiThetaPsiDeg[selected_idx].z, phiThetaPsiDeg[selected_idx].x*(-1f));


            if (pauseActive)
            {
                sliderTime_s = slider.value;
            }
            else
            {
                slider.value += Time.deltaTime;
                sliderTime_s = slider.value;
            }
            timeTMP.text = "Time [s]: " + sliderTime_s.ToString();

            UpdateButtonMod();
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
        public void UpdateCSVList(){
            if (File.Exists(inputField.text))
            {
                List<string> csvData = new List<string>();

                using (StreamReader reader = new StreamReader(inputField.text))
                {
                    while (!reader.EndOfStream)
                    {
                        string lines = reader.ReadLine();
                        csvData.Add(lines);
                    }
                }

                time_s          = new float[csvData.Count];
                xyz_m           = new Vector3[csvData.Count];
                phiThetaPsiDeg = new Vector3[csvData.Count];
                // CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US"); // Use "en-US" culture for dot as decimal separator

                for (int i = 0; i < csvData.Count; i++)
                {
                    string line = csvData[i].Trim();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] data = line.Split(',');

                    if (data.Length == 7)
                    {
                        if (float.TryParse(data[0], out float t) &&
                            float.TryParse(data[1], out float x) &&
                            float.TryParse(data[2], out float y) &&
                            float.TryParse(data[3], out float z) &&
                            float.TryParse(data[4], out float phi) &&
                            float.TryParse(data[5], out float theta) &&
                            float.TryParse(data[6], out float psi))                    
                        {
                            time_s[i] = t;
                            xyz_m[i].x = x;
                            xyz_m[i].y = y;
                            xyz_m[i].z = z;
                            phiThetaPsiDeg[i].x = phi;
                            phiThetaPsiDeg[i].y = theta;
                            phiThetaPsiDeg[i].z = psi;
                        }
                        else
                        {
                            Debug.LogWarning("Failed to parse values from CSV row " + (i + 1));
                        }
                    }
                    else
                    {
                        Debug.LogWarning("CSV row " + (i + 1) + " does not have 3 columns.");
                    }
                }

                slider.minValue = time_s[0];
                slider.maxValue = time_s[time_s.Length-2];
                sliderTime_s = slider.value;
            }
            else
            {
                Debug.LogError("No CSV file assigned to the 'csvFile' variable in the Inspector.");
            }
        }
    }
