using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro; // Import the TextMeshPro namespace

public class Target : MonoBehaviour
{
        public TMP_InputField inputField;     
        public Time_Manager timeManager;   
        public bool isLoad = false;
        public Color color;
        private float[] time_s;
        private Vector3[] xyz_m;
        private Vector3[] phiThetaPsiDeg;
        private int selected_idx;
        private string[] lines;
        private Renderer radarRenderer;
        private float delta_time;


        void Start(){
            // ChangeColorWithRandom();
            ChangeColor(color); 
        }

        public void UpdateState()
        {
            float min_delta_time = 10000000f;
            for (int i = 0; i < time_s.Length; i++)
            {
                delta_time = Mathf.Abs(time_s[i] - timeManager.currentTime);

                if (delta_time < min_delta_time)
                {
                    min_delta_time = delta_time;
                    selected_idx = i;
                }
            }
            if (selected_idx>=time_s.Length-1){
                selected_idx = time_s.Length-2;
            }
            // transform.position = new Vector3(xyz_m[selected_idx].y,xyz_m[selected_idx].z*(-1f),xyz_m[selected_idx].x);
            Vector3 nextPosition = new Vector3(xyz_m[selected_idx+1].y,xyz_m[selected_idx+1].z*(-1f),xyz_m[selected_idx+1].x);
            Vector3 curPosition = new Vector3(xyz_m[selected_idx].y,xyz_m[selected_idx].z*(-1f),xyz_m[selected_idx].x);

            float deltaTimeForLerp = time_s[selected_idx+1] - time_s[selected_idx];
            float elapsedPercetage = (timeManager.currentTime - time_s[selected_idx])/deltaTimeForLerp;
            transform.position = Vector3.Lerp(curPosition,nextPosition,elapsedPercetage);



            transform.rotation =  Quaternion.Euler(phiThetaPsiDeg[selected_idx].y*(-1f),  phiThetaPsiDeg[selected_idx].z, phiThetaPsiDeg[selected_idx].x*(-1f));
        }
 
        public void GenerateDataFromCSV(){
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
                isLoad = true;
            }
            else
            {
                Debug.LogError("No CSV file assigned to the 'csvFile' variable in the Inspector.");
                isLoad = false;
            }
        }
        public void ChangeColorWithRandom(){
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);
            Color randomColor = new Color(r, g, b);

            ChangeColor(randomColor);
    }
        public void ChangeColor(Color color){
            
            // Trail Rengini Degistirmek Icin
            radarRenderer = gameObject.GetComponent<Renderer>();
            radarRenderer.sharedMaterial = new Material(Shader.Find("Standard"))
            {
                color = color
            };

            // Missile Objecsinin Rengini Degistirmek Icin
            radarRenderer = gameObject.transform.GetChild(0).GetComponent<Renderer>();
            radarRenderer.sharedMaterial = new Material(Shader.Find("Standard"))
            {
                color = color
            };
        }
    }
