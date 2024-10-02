using Ex03.GarageLogic.VehicleObjects;
using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Enums.EnergyTypes;
using static Ex03.GarageLogic.Enums.VehiclesType;


namespace Ex03.GarageLogic.GarageManagement
{
    internal static class VehicleFactory
    {
        private static readonly Dictionary<eVehiclesType, eEnergyType> sr_VehicleTypeToEnergyType =
        new Dictionary<eVehiclesType, eEnergyType>()
        {
            { eVehiclesType.ElectricCar, eEnergyType.Electricity },
            { eVehiclesType.ElectricMotorcycle, eEnergyType.Electricity },
            { eVehiclesType.FuelBasedCar, eEnergyType.Octan95 },
            { eVehiclesType.FuelBasedMotorcycle, eEnergyType.Octan98 },
            { eVehiclesType.FuelBasedTruck, eEnergyType.Soler }
        };

        private static readonly Dictionary<eVehiclesType, Func<string, string, Vehicle>> sr_VehicleCreators = new Dictionary<eVehiclesType, Func<string, string, Vehicle>>
        {
            { eVehiclesType.FuelBasedCar, (i_ModelName, i_LicenseNumber) => new Car(i_ModelName, i_LicenseNumber) },
            { eVehiclesType.ElectricCar, (i_ModelName, i_LicenseNumber) => new Car(i_ModelName, i_LicenseNumber) },
            { eVehiclesType.FuelBasedMotorcycle, (i_ModelName, i_LicenseNumber) => new Motorcycle(i_ModelName, i_LicenseNumber) },
            { eVehiclesType.ElectricMotorcycle, (i_ModelName, i_LicenseNumber) => new Motorcycle(i_ModelName, i_LicenseNumber) },
            { eVehiclesType.FuelBasedTruck, (i_ModelName, i_LicenseNumber) => new Truck(i_ModelName, i_LicenseNumber) }
        };

        public static Vehicle CreateVehicle(eVehiclesType i_VehicleType, string i_ModelName, string i_LicenseNumber)
        {
            if(sr_VehicleCreators.TryGetValue(i_VehicleType, out var creator))
            {
                Vehicle vehicleResult = creator(i_ModelName, i_LicenseNumber);
                vehicleResult.InitializeEngine(sr_VehicleTypeToEnergyType[i_VehicleType]);
                vehicleResult.InitializeWheels();

                return vehicleResult;
            }
            else
            {
                throw new ArgumentException("Invalid vehicle type");
            }
        }
    }
}
