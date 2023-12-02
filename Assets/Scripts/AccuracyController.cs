using System;
using System.Collections.Generic;
using UnityEngine;
using Gtec.Chain.Common.Algorithms;

namespace Gtec.UnityInterface
{

    public class AccuracyController : MonoBehaviour
    {
        public Color NoClassifier;
        public Color VeryGoodClassifier;
        public Color GoodClassifier;
        public Color BadClassifier;

        private enum ClassifierAccuracy { NA, VeryGood, Good, Bad }

        private bool _update;
        private ClassifierAccuracy _classifierAccuracy;
        private SpriteRenderer _sprite;

        void Start()
        {
            _sprite = gameObject.GetComponent<SpriteRenderer>();
            _classifierAccuracy = ClassifierAccuracy.NA;
            _update = true;
            ERPBCIManager.Instance.ClassifierCalculated += OnClassifierAvailableERP;
            ERPBCIManager.Instance.ClassifierCalculationFailed += OnClassifierCalculationFailedERP;
            CVEPBCIManager.Instance.ClassifierCalculated += OnClassifierAvailableCVEP;
            CVEPBCIManager.Instance.ClassifierCalculationFailed += OnClassifierCalculationFailedCVEP;
        }

        private void OnApplicationQuit()
        {
            ERPBCIManager.Instance.ClassifierCalculated -= OnClassifierAvailableERP;
            ERPBCIManager.Instance.ClassifierCalculationFailed -= OnClassifierCalculationFailedERP;
            CVEPBCIManager.Instance.ClassifierCalculated -= OnClassifierAvailableCVEP;
            CVEPBCIManager.Instance.ClassifierCalculationFailed -= OnClassifierCalculationFailedCVEP;
        }

        private void OnClassifierAvailableERP(object sender, EventArgs e)
        {
            double meanAccuracy = 0;
            Dictionary<int, Accuracy> accuracy = ERPBCIManager.Instance.Accuracy();
            if (accuracy.Count < ERPBCIManager.Instance.NumberOfAverages)
                meanAccuracy = accuracy[accuracy.Count - 1].Mean;
            else
                meanAccuracy = accuracy[ERPBCIManager.Instance.NumberOfAverages].Mean;

            if (meanAccuracy >= 90)
                _classifierAccuracy = ClassifierAccuracy.VeryGood;
            else if (meanAccuracy >= 80 && meanAccuracy < 90)
                _classifierAccuracy = ClassifierAccuracy.Good;
            else
                _classifierAccuracy = ClassifierAccuracy.Bad;

            _update = true;
        }

        private void OnClassifierCalculationFailedERP(object sender, Exception e)
        {
            _classifierAccuracy = ClassifierAccuracy.NA;
            _update = true;
        }

        private void OnClassifierAvailableCVEP(object sender, EventArgs e)
        {
            CCAAccuracy accuracy = CVEPBCIManager.Instance.Accuracy();
            if (accuracy.Result == CCAAccuracy.TrainingQuality.VeryGood)
                _classifierAccuracy = ClassifierAccuracy.VeryGood;
            else if (accuracy.Result == CCAAccuracy.TrainingQuality.Good)
                _classifierAccuracy = ClassifierAccuracy.Good;
            else if (accuracy.Result == CCAAccuracy.TrainingQuality.VeryBad || accuracy.Result == CCAAccuracy.TrainingQuality.Bad)
                _classifierAccuracy = ClassifierAccuracy.Bad;
            else
                _classifierAccuracy = ClassifierAccuracy.Bad;

            _update = true;
        }

        private void OnClassifierCalculationFailedCVEP(object sender, Exception e)
        {
            _classifierAccuracy = ClassifierAccuracy.NA;
            _update = true;
        }

        void Update()
        {
            if (_update)
            {
                switch (_classifierAccuracy)
                {
                    case ClassifierAccuracy.NA:
                        {
                            _sprite.color = NoClassifier;
                            break;
                        }
                    case ClassifierAccuracy.VeryGood:
                        {
                            _sprite.color = VeryGoodClassifier;
                            break;
                        }
                    case ClassifierAccuracy.Good:
                        {
                            _sprite.color = GoodClassifier;
                            break;
                        }
                    case ClassifierAccuracy.Bad:
                        {
                            _sprite.color = BadClassifier;
                            break;
                        }
                }
                _update = false;
            }
        }
    }
}