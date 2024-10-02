using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.InnerVehicleMembers;

namespace Ex03.GarageLogic.VehicleObjects
{
    internal sealed class Car : Vehicle
    {
        private enum eColorType
        {
            Yellow = 1,
            White = 2,
            Red = 3,
            Black = 4,
        }
        
        internal enum eAmountOfDoors
        {
            TwoDoors = 2,
            ThreeDoors = 3,
            FourDoors = 4,
            FiveDoors = 5
        }

        private const int k_NumberOfWheels = 5;
        private const float k_MaximumAirPressure = 31; 
        private const int k_NumberOfAdditionalFeatures = 2;
        private const float k_MaxBatteryLifetimeInHours = 3.5f;
        private const float k_MaxTankSizeInLiters = 45f;
        private readonly List<KeyValuePair<string, Type>> r_AdditionalFeatures;
        private eColorType m_ColorTypeForRegularCar;
        private eAmountOfDoors m_AmountOfDoors;

        internal Car(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {
            r_AdditionalFeatures = new List<KeyValuePair<string, Type>>(k_NumberOfAdditionalFeatures)
                                       {
                                           new KeyValuePair<string, Type>(
                                               "Color",
                                               typeof(eColorType)),
                                           new KeyValuePair<string, Type>(
                                               "Number of doors",
                                               typeof(eAmountOfDoors))
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
                    eColorType color = EnumHelper.Parse<eColorType>(i_AttributeValue);
                    m_ColorTypeForRegularCar = color;
                    break;
                case 1:
                    eAmountOfDoors doors = EnumHelper.Parse<eAmountOfDoors>(i_AttributeValue);
                    m_AmountOfDoors = doors;
                    break;
                default:
                    throw new ValueOutOfRangeException(
                        "Invalid attribute index",
                        "SetAdditionalFeatures",
                        k_NumberOfAdditionalFeatures,
                        0);     
            }
        }

        // $G$ DSN-012 (-4) Code duplication, this logic appear in all vehicles.
        internal override void InitializeEngine(EnergyTypes.eEnergyType i_EnergyType)
        {
            switch(i_EnergyType)
            {
                case EnergyTypes.eEnergyType.Octan95:
                    m_Engine = new FuelBasedEngine(k_MaxTankSizeInLiters, i_EnergyType);
                    break;
                case EnergyTypes.eEnergyType.Electricity:
                    m_Engine = new ElecticEngine(k_MaxBatteryLifetimeInHours);
                    break;
                default:
                    throw new ArgumentException("Invalid energy type for car engine");
            }
        }

        public override string ToString()
        {
            StringBuilder carInfoBuilder = new StringBuilder();

            carInfoBuilder.Append(base.ToString());
            carInfoBuilder.AppendFormat("A car:{2}Color: {0}{2}Doors number: {1}{2}", 
                m_ColorTypeForRegularCar.ToString(), ((int)m_AmountOfDoors).ToString(), Environment.NewLine);

            return carInfoBuilder.ToString();
        }

        // $G$ DSN-012 (-5) Code duplication of InitializeWheels. 
        internal override void InitializeWheels()
        {
            m_ListOfWheels = new List<Wheel>();

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                m_ListOfWheels.Add(new Wheel(k_MaximumAirPressure));
            }
        }
    }
}