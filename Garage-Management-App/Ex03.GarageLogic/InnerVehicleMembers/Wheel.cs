using System;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.InnerVehicleMembers
{
    internal class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure = 0;
        private readonly float r_MaximumAirPressure;

        internal Wheel(float i_MaximumAirPressure)
        {
            r_ManufacturerName = "Moshe's Wheels Ltd";
            r_MaximumAirPressure = i_MaximumAirPressure;
        }

        internal void AddAirPressureToMax()
        {
            m_CurrentAirPressure = r_MaximumAirPressure;
        }

        internal void AddAirPressure(float i_AmountOfAirToAdd)
        {
            if(i_AmountOfAirToAdd < 0)
            {

                throw new ValueOutOfRangeException(
                    "Amount of air to add cannot be negative.",
                    "Wheel",
                    r_MaximumAirPressure,
                    0 
                );
            }

            if(m_CurrentAirPressure + i_AmountOfAirToAdd > r_MaximumAirPressure)
            {
                throw new ValueOutOfRangeException(
                    "Cannot add air beyond the maximum air pressure.",
                    "Wheel",
                    r_MaximumAirPressure,
                    0
                );
            }

            m_CurrentAirPressure += i_AmountOfAirToAdd;
        }

        public override string ToString()
        {
            string wheelDetails = string.Format(
                "Manufacturer Name: {0}{1}" +
                "Current Air Pressure: {2}{1}" +
                "Maximum Air Pressure: {3}{1}",
                r_ManufacturerName,
                Environment.NewLine,
                m_CurrentAirPressure,
                r_MaximumAirPressure
            );

            return wheelDetails;
        }
    }
}
