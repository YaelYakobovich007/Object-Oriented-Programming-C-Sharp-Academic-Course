using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.InnerVehicleMembers;

namespace Ex03.GarageLogic.VehicleObjects
{
    internal sealed class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 12;
        private const int k_MaximumAirPressure = 28;
        private const int k_NumberOfAdditionalFeatures = 2;
        private const float k_MaxTankSizeInLiters = 120f;
        private readonly List<KeyValuePair<string, Type>> r_AdditionalFeatures;
        private bool m_IsCarryingHazardousMaterials;
        private float m_CargoVolume;

        internal Truck(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {
            r_AdditionalFeatures = new List<KeyValuePair<string, Type>>(k_NumberOfAdditionalFeatures)
                                       {
                                           new KeyValuePair<string, Type>(
                                               "Transporting hazardous materials",
                                               typeof(bool)),
                                           new KeyValuePair<string, Type>("Cargo volume", typeof(float))
                                       };
        }

        public override List<KeyValuePair<string, Type>> GetAdditionalFeatures()
        {
            return r_AdditionalFeatures;
        }

        internal override int GetNumberOfAdditionalFeatures()
        {
            return k_NumberOfAdditionalFeatures;
        }

        internal override void InitializeEngine(EnergyTypes.eEnergyType i_EnergyType)
        {
            switch (i_EnergyType)
            {
                case EnergyTypes.eEnergyType.Soler:
                    m_Engine = new FuelBasedEngine(k_MaxTankSizeInLiters, i_EnergyType);
                    break;
                default:
                    throw new ArgumentException("Invalid energy type for car engine");
            }
        }

        internal override void InitializeWheels()
        {
            m_ListOfWheels = new List<Wheel>();

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                m_ListOfWheels.Add(new Wheel(k_MaximumAirPressure));
            }
        }

        internal override void SetAdditionalFeatures(int i_AttributeIndex, string i_AttributeValue)
        {
            const string k_YesInput = "Y";
            const string k_NoInput = "N";

            switch (i_AttributeIndex)
            {
                case 0: 
                    if (i_AttributeValue.ToUpper() == k_YesInput)
                    {
                        m_IsCarryingHazardousMaterials = true;
                    }
                    else if (i_AttributeValue.ToUpper() == k_NoInput)
                    {
                        m_IsCarryingHazardousMaterials = false;
                    }
                    else if (!bool.TryParse(i_AttributeValue, out bool isTransportingHazardousMaterials))
                    {
                        throw new ArgumentException(
                            string.Format(
                                "Invalid input for Transporting hazardous materials. It must be a valid boolean or {0}/{1}.",
                                k_YesInput,
                                k_NoInput));
                    }
                    else
                    {
                        m_IsCarryingHazardousMaterials = isTransportingHazardousMaterials;
                    }

                    break;
                case 1:
                    if(!float.TryParse(i_AttributeValue, out float cargoVolume))
                    {
                        throw new FormatException("Input must be a number");
                    }

                    if (cargoVolume < 0)
                    {
                        throw new ValueOutOfRangeException(
                            "Cargo volume must be a non-negative number.",
                            "Truck Cargo Volume",
                            float.MaxValue,
                            0);
                    }

                    m_CargoVolume = cargoVolume;
                    break;
                default:
                    throw new ValueOutOfRangeException(
                        "Invalid attribute index",
                        "SetAdditionalFeatures",
                        k_NumberOfAdditionalFeatures,
                        0); 
            }
        }

        public override string ToString()
        {
            StringBuilder truckInfoBuilder = new StringBuilder();

            truckInfoBuilder.Append(base.ToString());
            truckInfoBuilder.AppendFormat("A Truck:{2}{0}carrying hazardous materials{2}Cargo volume: {1}{2}",
                 m_IsCarryingHazardousMaterials ? "" : "Not ", m_CargoVolume.ToString(), Environment.NewLine);

            return truckInfoBuilder.ToString();
        }
    }
}
