using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.InnerVehicleMembers;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.VehicleObjects
{ 
    internal abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_EnergyLeftPercentage;
        protected List<Wheel> m_ListOfWheels;
        protected Engine m_Engine;

        protected Vehicle(string i_ModelName, string i_LicenseNumber)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergyLeftPercentage = 0;
        }

        internal abstract void InitializeWheels();

        internal void FillAirPressureToMax()
        {
            foreach (Wheel wheel in m_ListOfWheels)
            {
                wheel.AddAirPressureToMax();
            }
        }

        public abstract List<KeyValuePair<string, Type>> GetAdditionalFeatures();

        internal abstract int GetNumberOfAdditionalFeatures();

        internal abstract void SetAdditionalFeatures(int i_AttributeIndex, string i_AttributeValue);

        internal void SetInitialEnergy(float i_AmountToReplenish)
        {
            m_Engine.InitializeEnergy(i_AmountToReplenish);
            m_EnergyLeftPercentage = i_AmountToReplenish / m_Engine.MaxEnergyCapacity;
        }

        public void ReplenishEnergyToEngine(float i_AmountToReplenish, EnergyTypes.eEnergyType i_EnergyType)
        {
            m_Engine.ReplenishEnergy(i_AmountToReplenish, i_EnergyType);
            m_EnergyLeftPercentage = i_AmountToReplenish / m_Engine.MaxEnergyCapacity;
        }

        internal abstract void InitializeEngine(EnergyTypes.eEnergyType i_EnergyType);

        internal void AddAirPressureToWheels(float i_AmountOfAirToAdd)
        {
            foreach (Wheel wheel in m_ListOfWheels)
            {
                wheel.AddAirPressure(i_AmountOfAirToAdd);
            }
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();

            stringToReturn.AppendFormat("Model name: {0} License number: {1} has {2} wheels:{3}", 
                m_ModelName, m_LicenseNumber, m_ListOfWheels.Count, Environment.NewLine);
            int i = 0;

            foreach (Wheel wheel in this.m_ListOfWheels)
            {
                i++;
                stringToReturn.AppendFormat("Wheel no.{0}: {1}", i, wheel.ToString());
            }

            stringToReturn.AppendFormat("Engine info:{1}{0}{2} percent of energy left{1}", 
                m_Engine.ToString(), Environment.NewLine, m_EnergyLeftPercentage);

            return stringToReturn.ToString();
        }
    }
}

