using System;
using System.Text;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.InnerVehicleMembers
{
    internal class ElecticEngine : Engine
    {
        private const float k_NumberOfMinutesInAnHour = 60f;

        public ElecticEngine(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
        {
            m_EnergyType = EnergyTypes.eEnergyType.Electricity;
        }

        internal override void ReplenishEnergy(float i_AmountOfEnergy, EnergyTypes.eEnergyType i_EnergyType)
        {
            if(i_EnergyType != m_EnergyType)
            {
                throw new ArgumentException("Cannot charge electric vehicles with fuel");
            }

            if(i_AmountOfEnergy > r_MaxEnergyCapacity - m_CurrentEnergyAmount)
            {
                throw new ValueOutOfRangeException("Cannot charge the engine over the limit", "Electric Engine",
                    (r_MaxEnergyCapacity - m_CurrentEnergyAmount) * k_NumberOfMinutesInAnHour, 0);
            }

            if(i_AmountOfEnergy < 0)
            {
                throw new ValueOutOfRangeException("Cannot charge a negative amount", "Electric Engine",
                    (r_MaxEnergyCapacity - m_CurrentEnergyAmount) * k_NumberOfMinutesInAnHour, 0);
            }

            m_CurrentEnergyAmount += i_AmountOfEnergy;
        }

        public override string ToString()
        {
            StringBuilder electricEngineInformation = new StringBuilder();

            electricEngineInformation.AppendFormat("Engine Type: Electric{2} Maximum Battery Lifetime: {0} hours{2}" +
                "Current Battery Lifetime left: {1} hours{2}",
                r_MaxEnergyCapacity, m_CurrentEnergyAmount, Environment.NewLine);

            return electricEngineInformation.ToString();
        }

        internal override void InitializeEnergy(float i_AmountOfEnergy)
        {
            ReplenishEnergy(i_AmountOfEnergy / k_NumberOfMinutesInAnHour, m_EnergyType);
        }
    }
}
