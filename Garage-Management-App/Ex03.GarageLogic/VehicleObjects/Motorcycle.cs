using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.InnerVehicleMembers;
using static Ex03.GarageLogic.Enums.EnergyTypes;

namespace Ex03.GarageLogic.VehicleObjects
{
    internal sealed class Motorcycle: Vehicle
    {
        private enum eMotorCycleLicenseType
        {
            A = 1,
            A1 = 2,
            AA = 3,
            B = 4,
        }

        private const int k_NumberOfWheels = 2;
        private const float k_MaximumAirPressure = 33;
        private const int k_NumberOfAdditionalFeatures = 2;
        private const float k_MaxBatteryLifetimeInHours = 2.5f;
        private const float k_MaxTankSizeInLiters = 5.5f;
        private readonly List<KeyValuePair<string, Type>> r_AdditionalFeatures;
        private eMotorCycleLicenseType m_MotorCycleLicenseType;
        private int m_EngineVolume;

        internal Motorcycle(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
        {
            r_AdditionalFeatures = new List<KeyValuePair<string, Type>>(k_NumberOfAdditionalFeatures)
                                       {
                                           new KeyValuePair<string, Type>("License type", typeof(eMotorCycleLicenseType)),
                                           new KeyValuePair<string, Type>("Engine volume", typeof(int))
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

        internal override void SetAdditionalFeatures(int i_AttributeIndex, string i_AttributeValue)
        {
            switch(i_AttributeIndex)
            {
                case 0:
                    m_MotorCycleLicenseType = EnumHelper.Parse<eMotorCycleLicenseType>(i_AttributeValue);
                    break;
                case 1:
                    if(!int.TryParse(i_AttributeValue, out int engineVolume))
                    {
                        throw new FormatException("Input must be a number");
                    }
                    else if (engineVolume < 0)
                    {
                        throw new ValueOutOfRangeException(
                            "Engine volume may not be less than zero",
                            "Engine Volume",
                            int.MaxValue,
                            0);
                    }

                    m_EngineVolume = engineVolume;
                    break;
                default:
                    throw new ValueOutOfRangeException(
                        "Invalid attribute index",
                        "SetAdditionalFeatures",
                        k_NumberOfAdditionalFeatures,
                        0);
            }
        }

        internal override void InitializeEngine(eEnergyType i_EnergyType)
        {
            switch(i_EnergyType)
            {
                case eEnergyType.Octan98:
                    m_Engine = new FuelBasedEngine(k_MaxTankSizeInLiters, i_EnergyType);
                    break;
                case eEnergyType.Electricity:
                    m_Engine = new ElecticEngine(k_MaxBatteryLifetimeInHours);
                    break;
                default:
                    throw new ArgumentException("Invalid energy type for motorcycle engine");
            }
        }

        internal override void InitializeWheels()
        {
            m_ListOfWheels = new List<Wheel>();

            for(int i = 0; i < k_NumberOfWheels; i++)
            {
                m_ListOfWheels.Add(new Wheel(k_MaximumAirPressure));
            }
        }

        public override string ToString()
        {
            StringBuilder motorcycleInfoBuilder = new StringBuilder();

            motorcycleInfoBuilder.Append(base.ToString());
            motorcycleInfoBuilder.AppendFormat("A Motorcycle:{2}License type: {0}{2}Engine volume: {1} square meters{2}", 
                m_MotorCycleLicenseType.ToString(), m_EngineVolume.ToString(), Environment.NewLine);

            return motorcycleInfoBuilder.ToString();
        }
    }
}
