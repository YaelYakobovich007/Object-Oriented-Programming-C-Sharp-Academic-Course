using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.InnerVehicleMembers
{
    internal abstract class Engine
    {
        protected readonly float r_MaxEnergyCapacity;
        protected EnergyTypes.eEnergyType m_EnergyType;
        protected float m_CurrentEnergyAmount;

        public float MaxEnergyCapacity
        {
            get { return r_MaxEnergyCapacity; }
        }

        internal Engine(float i_MaxEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrentEnergyAmount = 0;
        }

        internal abstract void ReplenishEnergy(float i_AmountOfEnergy, EnergyTypes.eEnergyType i_EnergyType);

        internal abstract void InitializeEnergy(float i_AmountOfEnergy);
    }

}
