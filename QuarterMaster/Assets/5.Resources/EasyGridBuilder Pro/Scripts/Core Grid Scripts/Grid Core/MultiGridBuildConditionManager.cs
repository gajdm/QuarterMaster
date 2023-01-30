using System.Collections.Generic;
using UnityEngine;

namespace SoulGames.EasyGridBuilderPro
{
    public class MultiGridBuildConditionManager : MonoBehaviour
    {
        public static event OnBuildConditionCheckBuildableGridObjectDelegate OnBuildConditionCheckBuildableGridObject;
        public delegate void OnBuildConditionCheckBuildableGridObjectDelegate(BuildableGridObjectTypeSO buildableGridObjectTypeSO);
        public static event OnBuildConditionCheckBuildableFreeObjectDelegate OnBuildConditionCheckBuildableFreeObject;
        public delegate void OnBuildConditionCheckBuildableFreeObjectDelegate(BuildableFreeObjectTypeSO buildableFreeObjectTypeSO);

        public static event OnBuildConditionCompleteBuildableGridObjectDelegate OnBuildConditionCompleteBuildableGridObject;
        public delegate void OnBuildConditionCompleteBuildableGridObjectDelegate(BuildableGridObjectTypeSO buildableGridObjectTypeSO);
        public static event OnBuildConditionCompleteBuildableFreeObjectDelegate OnBuildConditionCompleteBuildableFreeObject;
        public delegate void OnBuildConditionCompleteBuildableFreeObjectDelegate(BuildableFreeObjectTypeSO buildableFreeObjectTypeSO);

        public static bool BuidConditionResponseBuildableGridObject;
        public static bool BuidConditionResponseBuildableFreeObject;

        public static List<BuildableGridObjectTypeSO> BuildableGridObjectTypeSOList;
        public static List<BuildableFreeObjectTypeSO> BuildableFreeObjectTypeSOList;
        [Tooltip("Add all the 'Buildable Grid Object Type SO' that has build conditions enabled")]
        [SerializeField]private List<BuildableGridObjectTypeSO> _buildableGridObjectTypeSOList;
        [Tooltip("Add all the 'Buildable Free Object Type SO' that has build conditions enabled")]
        [SerializeField]private List<BuildableFreeObjectTypeSO> _buildableFreeObjectTypeSOList;

        private List<EasyGridBuilderPro> easyGridBuilderProList;

        private void Start()
        {
            BuildableGridObjectTypeSOList = new List<BuildableGridObjectTypeSO>();
            BuildableFreeObjectTypeSOList = new List<BuildableFreeObjectTypeSO>();
            easyGridBuilderProList = MultiGridManager.Instance.easyGridBuilderProList;

            foreach (EasyGridBuilderPro easyGridBuilderPro in easyGridBuilderProList)
            {
                easyGridBuilderPro.OnBuildConditionCheckCallerBuildableGridObject += OnBuildConditionCheckCallerBuildableGridObject;
                easyGridBuilderPro.OnBuildConditionCompleteCallerBuildableGridObject += OnBuildConditionCompleteCallerBuildableGridObject;
                easyGridBuilderPro.OnBuildConditionCheckCallerBuildableFreeObject += OnBuildConditionCheckCallerBuildableFreeObject;
                easyGridBuilderPro.OnBuildConditionCompleteCallerBuildableFreeObject += OnBuildConditionCompleteCallerBuildableFreeObject;
            }
            
            foreach (BuildableGridObjectTypeSO item in _buildableGridObjectTypeSOList)
            {
                BuildableGridObjectTypeSOList.Add(item);
            }
            foreach (BuildableFreeObjectTypeSO item in _buildableFreeObjectTypeSOList)
            {
                BuildableFreeObjectTypeSOList.Add(item);
            }
        }

        private void OnDisable()
        {
            foreach (EasyGridBuilderPro easyGridBuilderPro in easyGridBuilderProList)
            {
                easyGridBuilderPro.OnBuildConditionCheckCallerBuildableGridObject -= OnBuildConditionCheckCallerBuildableGridObject;
                easyGridBuilderPro.OnBuildConditionCompleteCallerBuildableGridObject -= OnBuildConditionCompleteCallerBuildableGridObject;
                easyGridBuilderPro.OnBuildConditionCheckCallerBuildableFreeObject -= OnBuildConditionCheckCallerBuildableFreeObject;
                easyGridBuilderPro.OnBuildConditionCompleteCallerBuildableFreeObject -= OnBuildConditionCompleteCallerBuildableFreeObject;
            }
        }

        private void OnBuildConditionCheckCallerBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
        {
            OnBuildConditionCheckBuildableGridObject?.Invoke(buildableGridObjectTypeSO);
        }

        private void OnBuildConditionCompleteCallerBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
        {
            OnBuildConditionCompleteBuildableGridObject?.Invoke(buildableGridObjectTypeSO);
        }

        private void OnBuildConditionCheckCallerBuildableFreeObject(BuildableFreeObjectTypeSO buildableFreeObjectTypeSO)
        {
            OnBuildConditionCheckBuildableFreeObject?.Invoke(buildableFreeObjectTypeSO);
        }

        private void OnBuildConditionCompleteCallerBuildableFreeObject(BuildableFreeObjectTypeSO buildableFreeObjectTypeSO)
        {
            OnBuildConditionCompleteBuildableFreeObject?.Invoke(buildableFreeObjectTypeSO);
        }
    }
}
