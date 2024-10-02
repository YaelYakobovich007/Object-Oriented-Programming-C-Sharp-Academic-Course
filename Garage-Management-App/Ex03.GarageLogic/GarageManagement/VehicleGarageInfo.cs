using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.VehicleObjects;
using System;
using System.Text;

namespace Ex03.GarageLogic.GarageManagement
{
    internal class VehicleGarageInfo
    {
        private readonly Vehicle r_Vehicle;
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhone;

        internal VehicleStatuses.eVehicleStatus VehicleStatus { get; set; }

        internal VehicleGarageInfo(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            r_Vehicle = i_Vehicle;
            r_OwnerName = i_OwnerName;
            r_OwnerPhone = i_OwnerPhone;
            VehicleStatus = VehicleStatuses.eVehicleStatus.InRepair;
        }

        internal Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfoBuilder = new StringBuilder();

            vehicleInfoBuilder.AppendFormat("Owner's name: {0}{3}Owner's phone: {1}{3}Vehicle's status: {2}{3}"
                , r_OwnerName, r_OwnerPhone, VehicleStatus.ToString(), Environment.NewLine);
            vehicleInfoBuilder.Append(Vehicle.ToString());

            return vehicleInfoBuilder.ToString();
        }
    }
}
