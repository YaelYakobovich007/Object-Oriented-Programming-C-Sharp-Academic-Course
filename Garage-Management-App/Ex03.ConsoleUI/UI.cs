using Ex03.GarageLogic.Exceptions;
using System;

namespace Ex03.ConsoleUI
{
    internal class UI
    {
        internal enum eGarageRequestType
        {
            AddNewVehicle = 1,
            ShowLicenseNumbers,
            ChangeVehicleStatus,
            InflateVehicleTiresToMax,
            RefuelVehicle,
            ChargeElectricVehicle,
            DisplayVehicleData,
            Exit,
        }

        private readonly VehicleOperations r_VehicleOperations;
       
        internal UI()
        {
            r_VehicleOperations = new VehicleOperations();
        }

        internal bool RunGarageRequest()
        {
            bool exit = false;
            while(!exit)
            {
                printMenu();
                if(int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(eGarageRequestType), choice))
                {
                    eGarageRequestType requestType = (eGarageRequestType)choice;
                    try
                    {
                        switch(requestType)
                        {
                            case eGarageRequestType.AddNewVehicle:
                                r_VehicleOperations.AddNewVehicleToGarage();
                                break;
                            case eGarageRequestType.ShowLicenseNumbers:
                                r_VehicleOperations.ShowLicenseNumbers();
                                break;
                            case eGarageRequestType.ChangeVehicleStatus:
                                r_VehicleOperations.ChangeVehicleStatus();
                                break;
                            case eGarageRequestType.InflateVehicleTiresToMax:
                                r_VehicleOperations.InflateTiresToMaximumPressure();
                                break;
                            case eGarageRequestType.RefuelVehicle:
                                r_VehicleOperations.RefuelVehicle();
                                break;
                            case eGarageRequestType.ChargeElectricVehicle:
                                r_VehicleOperations.ChargeElectricVehicle();
                                break;
                            case eGarageRequestType.DisplayVehicleData:
                                r_VehicleOperations.DisplayVehicleData();
                                break;
                            case eGarageRequestType.Exit:
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    catch(FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch(ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch(ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                if(!exit) 
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }

            return true;
        }

        private void printMenu()
        {
            Console.Clear();
            string menu = @"
Garage Management System
1. Put a new car in the garage
2. Show the list of license numbers
3. Change the status of a vehicle
4. Inflate vehicle tires to maximum
5. Refuel a fuel-driven vehicle
6. Charge an electric vehicle
7. Display complete vehicle data
8. Exit
Choose an option: ";
            Console.Write(menu);
        }
    }
}
