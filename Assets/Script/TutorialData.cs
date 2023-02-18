using System;

namespace Model
{
    [Serializable]
    public class TutorialData
    {
        public string sceneName;
        public float curDistance;
        public float playTime;
        public int finishStatus;

        public override string ToString(){
            return UnityEngine.JsonUtility.ToJson (this, true);
        }
    }
}