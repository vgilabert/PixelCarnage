using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLeveling : MonoBehaviour
    {
        public int level = 1;  
        
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
                OnExperienceChanged(_experience, experienceCap);
            }
        }
        
        [SerializeField] private int experienceCap;
        public int ExperienceCap
        {
            get => experienceCap;
            set
            {
                experienceCap = value;
                OnExperienceChanged(_experience, experienceCap);
            }
        }
        
        public List<LevelRange> levelRanges;

        [Serializable]
        public class LevelRange
        {
            public int startLevel;
            public int endLevel;
            public int experienceCapIncrease;
        }
        
        // Events
        public static Action<int, int> OnExperienceChanged = delegate { };
        public static Action<int> OnLevelUp = delegate { };
        
        private void Start()
        {
            Level = level;
            Experience = _experience;
            ExperienceCap = levelRanges[0].experienceCapIncrease;
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
                
                int experienceCapIncrease = 0;
                foreach (LevelRange range in levelRanges)
                {
                    if (level >= range.startLevel && level <= range.endLevel)
                    {
                        experienceCapIncrease = range.experienceCapIncrease;
                        break;
                    }
                }
                ExperienceCap += experienceCapIncrease;
            }
        }
    }
}