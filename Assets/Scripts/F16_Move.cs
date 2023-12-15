using UnityEngine;

public class F16_Move : MonoBehaviour
{
        public Time_Manager timeManager;   
        public float tangentialVelocity;
        public float angularVelocity;
        public Vector3 initPosition;
        public float startTime; 
        private float[] time_s;
        public Vector3[] xyz_m;
        public Vector3[] phiThetaPsiDeg;
        private int selected_idx;
        public GameObject ghost;
        private float delta_time;

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
            float deltaTimeForLerp = time_s[selected_idx+1] -  time_s[selected_idx];
            float elapsedPercetage = (timeManager.currentTime-time_s[selected_idx])/deltaTimeForLerp;
            transform.position = Vector3.Lerp(curPosition,nextPosition,elapsedPercetage);

            // transform.rotation =  Quaternion.Euler(phiThetaPsiDeg[selected_idx].y*(-1f),  phiThetaPsiDeg[selected_idx].z, phiThetaPsiDeg[selected_idx].x*(-1f));
        }
 
        public void CreateMovement(){
            float deltaTime = 0.001f;
            time_s = Linspace(timeManager.minMaxTime.x,timeManager.minMaxTime.y, deltaTime);
            xyz_m = new Vector3[time_s.Length];
            xyz_m[0] = initPosition;
            for (int i = 1; i < time_s.Length; i++)
            {
                if (time_s[i] >= startTime)
                {
                    UpdateMovement(deltaTime);
                    xyz_m[i] =  initPosition + new Vector3(ghost.transform.localPosition.x*(-1f),ghost.transform.localPosition.y*(-1f),ghost.transform.localPosition.z*(-1f));   
                }
                else
                {
                    xyz_m[i] =  initPosition;
                }

            }
        }

        
        private float[] Linspace(float start, float end, float incriment){
            int ct = Mathf.RoundToInt((end-start)/incriment);
            float[] result = new float[ct];
            result[0] = start;
            for (int i = 1; i < ct; i++)
            {
                result[i] = result[i-1]+incriment;
            }
            return result;
        }
        public void UpdateMovement(float deltaTime){
                ghost.transform.Translate(tangentialVelocity*deltaTime*(-1f),0f,0f,ghost.transform);
                ghost.transform.Rotate(0f,angularVelocity*deltaTime,0f);
        }

}
