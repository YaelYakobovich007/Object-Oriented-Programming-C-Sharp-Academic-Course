using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.VehicleObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using static Ex03.GarageLogic.Enums.EnergyTypes;

namespace Ex03.GarageLogic.GarageManagement
{
    public class GarageManager
    {
        private Dictionary<string, VehicleGarageInfo> m_Vehicles = new Dictionary<string, VehicleGarageInfo>();
        private VehicleGarageInfo m_CurrentVehicleInfoToInsert;

        public bool IsVehicleInGarage(string i_LicenceNumber)
        {
            return m_Vehicles.ContainsKey(i_LicenceNumber);
        }

        public void CheckIfVehicleExistsAndSetStatus(string i_LicenceNumber)
        {
            if (IsVehicleInGarage(i_LicenceNumber))
            {
                m_Vehicles[i_LicenceNumber].VehicleStatus = VehicleStatuses.eVehicleStatus.InRepair;
                throw new ArgumentException("The vehicle is already in the garage and has been set to 'In Repair' status.");
            }
        }

        public void CreateNewVehicle(VehiclesType.eVehiclesType i_VehicleType, string i_ModelName, string i_OwnerName, string i_OwnerPhoneNumber, string i_LicenseNumber)
        {
            Vehicle vehicle = VehicleFactory.CreateVehicle(i_VehicleType, i_ModelName, i_LicenseNumber);

            m_Vehicles[i_LicenseNumber] = new VehicleGarageInfo(vehicle, i_OwnerName, i_OwnerPhoneNumber);
            m_CurrentVehicleInfoToInsert = m_Vehicles[i_LicenseNumber];
        }

        public List<string> GetAllLicenseNumbersByStatus(VehicleStatuses.eVehicleStatus i_StatusFilter)
        {
            List<string> result = new List<string>();

            foreach (string licenseNumber in m_Vehicles.Keys)
            {
                if (m_Vehicles[licenseNumber].VehicleStatus == i_StatusFilter)
                {
                    result.Add(licenseNumber);
                }
            }

            return result;
        }

        public List<string> GetAllLicenseNumbers()
        {
            return m_Vehicles.Keys.ToList();
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, VehicleStatuses.eVehicleStatus i_Status) 
        {
            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].VehicleStatus = i_Status; 
            }
            else
            {
                throw new ArgumentException("License number does not exist");
            }
        }

        public void FillAirInWheelsToMax(string i_LicenseNumber)
        {
            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                Vehicle currentVehicle = m_Vehicles[i_LicenseNumber].Vehicle;

                currentVehicle.FillAirPressureToMax();
            }
            else
            {
                throw new ArgumentException("License number does not exist");
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_AmountToCharge)
        {
            const float k_NumberOfMinutesInAnHour = 60f;

            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                Vehicle currentVehicle = m_Vehicles[i_LicenseNumber].Vehicle;

                currentVehicle.ReplenishEnergyToEngine(i_AmountToCharge / k_NumberOfMinutesInAnHour, eEnergyType.Electricity);
            }
            else
            {
                throw new ArgumentException("License number does not exist");
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, eEnergyType i_EnergyType, float i_AmountToReplenish)
        {
            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                Vehicle currentVehicle = m_Vehicles[i_LicenseNumber].Vehicle;

                currentVehicle.ReplenishEnergyToEngine(i_AmountToReplenish, i_EnergyType);
            }
            else
            {
                throw new ArgumentException("License number does not exist");
            }
        }

        public string GetDetails(string i_LicenseNumber)
        {
            string detailsResult;

            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                detailsResult = m_Vehicles[i_LicenseNumber].ToString();
            }
            else
            {
                throw new ArgumentException("License number does not exist");
            }

            return detailsResult;
        }

        public void InsertSpecialAttributeToCurrentByIndex(int i_AttributeIndex, string i_Attribute)
        {
            m_CurrentVehicleInfoToInsert.Vehicle.SetAdditionalFeatures(i_AttributeIndex, i_Attribute);
        }

        public KeyValuePair<string, Type> GetCurrentCarKeyValuePairByIndex(int i_Index)
        {
            return m_CurrentVehicleInfoToInsert.Vehicle.GetAdditionalFeatures()[i_Index];
        }

        public int GetAmountOfSpecialAttribute()
        {
            return m_CurrentVehicleInfoToInsert.Vehicle.GetNumberOfAdditionalFeatures(); 
        }

        public void SetInitialEnergy(float i_AmountToReplenish)
        {
           m_CurrentVehicleInfoToInsert.Vehicle.SetInitialEnergy(i_AmountToReplenish);
        }

        public void SetInitialWheelAirPressureForCurrentVehicle(float i_InitialAirPressure)
        {
            if (m_CurrentVehicleInfoToInsert != null)
            {
                m_CurrentVehicleInfoToInsert.Vehicle.AddAirPressureToWheels(i_InitialAirPressure); 
            }
        }
        public void ClearCurrentVehicle()
        {
            m_CurrentVehicleInfoToInsert = null;
        }
    }
}
