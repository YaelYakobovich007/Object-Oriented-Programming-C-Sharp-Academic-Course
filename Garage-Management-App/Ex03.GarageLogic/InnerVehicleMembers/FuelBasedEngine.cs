using System;
using System.Text;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.InnerVehicleMembers
{
    internal class FuelBasedEngine : Engine
    {
        public FuelBasedEngine(float i_MaxEnergyCapacity, EnergyTypes.eEnergyType i_EnergyType) : base(i_MaxEnergyCapacity)
        {
            m_EnergyType = i_EnergyType;
        }

        internal override void ReplenishEnergy(float i_AmountOfEnergy, EnergyTypes.eEnergyType i_EnergyType)
        {
            if(i_EnergyType != m_EnergyType)
            {
                throw new ArgumentException("You've entered a wrong gas type for this vehicle");
            }
            else if(i_AmountOfEnergy > r_MaxEnergyCapacity - m_CurrentEnergyAmount)
            {
                throw new ValueOutOfRangeException("Cannot fill the gas over the limit", "Fuel Engine", r_MaxEnergyCapacity - m_CurrentEnergyAmount, 0);
            }
            else if(i_AmountOfEnergy < 0)
            {
                throw new ValueOutOfRangeException("Cannot fill a negative amount", "Fuel Engine", r_MaxEnergyCapacity - m_CurrentEnergyAmount, 0);
            }

            m_CurrentEnergyAmount += i_AmountOfEnergy;
        }

        public override string ToString()
        {
            StringBuilder fuelEngineInformation = new StringBuilder(); 

            fuelEngineInformation.AppendFormat("Engine type: Fuel, with fuel type {0}{3}" +
                "Maximum tank size: {1} liters{3}Fuel Currently in the tank: {2} liters{3}",
                m_EnergyType.ToString() ,r_MaxEnergyCapacity, m_CurrentEnergyAmount, Environment.NewLine);

            return fuelEngineInformation.ToString();
        }

        internal override void InitializeEnergy(float i_AmountOfEnergy)
        {
            ReplenishEnergy(i_AmountOfEnergy, m_EnergyType);
        }
    }
}
