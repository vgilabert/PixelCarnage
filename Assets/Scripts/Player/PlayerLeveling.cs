using System;
using UnityEngine;

namespace Player
{
    public class PlayerLeveling : MonoBehaviour
    {
        [Serializable]
        public class LevelRange
        {
            public int startLevel;
            public int endLevel;
            public int experienceCapIncrease;
        }
        
        [SerializeField] private int level = 1;  
        public int Level
        {
            get => level;
            set
            {
                level = value;
                OnLevelUp(level);
            }
        }
        
        private int _experience;
        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                OnExperienceChanged(_experience, _experienceCap);
            }
        }
        
        private int _experienceCap;
        public int ExperienceCap
        {
            get => _experienceCap;
            set
            {
                _experienceCap = value;
                OnExperienceChanged(_experience, _experienceCap);
            }
        }
        
        //public List<LevelRange> levelRanges;
        public int baseCap = 10;
        public float capExponent = 1.5f;
        
        // Events
        public static Action<int, int> OnExperienceChanged = delegate { };
        public static Action<int> OnLevelUp = delegate { };
        
        private void Start()
        {
            Level = level;
            Experience = _experience;
            ExperienceCap = baseCap;
            //ExperienceCap = levelRanges[0].experienceCapIncrease;
        }

        public void IncreaseExperience(int amount)
        {
            Experience += amount;
            LevelUpChecker();
        }

        private void LevelUpChecker()
        {
            if (Experience >= ExperienceCap)
            {
                Level++;
                Experience -= ExperienceCap;
                
                // Increase experience cap
                ExperienceCap = (int)CalculateExperienceCap(Level);
                
                /*int experienceCapIncrease = 0;
                foreach (LevelRange range in levelRanges)
                {
                    if (level >= range.startLevel && level <= range.endLevel)
                    {
                        experienceCapIncrease = range.experienceCapIncrease;
                        break;
                    }
                }
                ExperienceCap += experienceCapIncrease;*/
            }
        }
        
        // Describes the formula for calculating the experience cap for each level
        private float CalculateExperienceCap(int level)
        {
            return baseCap * Mathf.Pow(level, capExponent);
        }
    }
}