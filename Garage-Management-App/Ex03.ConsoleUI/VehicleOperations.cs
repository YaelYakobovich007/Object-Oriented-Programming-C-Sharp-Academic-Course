using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.GarageManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal class VehicleOperations
    {
        private const string k_QuitFlag = "Q";
        private const string k_YesResponse = "Y";
        private const string k_NoResponse = "N";
        private readonly GarageManager r_Garage = new GarageManager();

        internal void AddNewVehicleToGarage()
        {
            displayHeader("Add a New Vehicle to the Garage", "Starting Process");
            string licenseNumber = getNonEmptyString("Please enter the license number: ");

            try
            {
                r_Garage.CheckIfVehicleExistsAndSetStatus(licenseNumber);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            string ownerName = getNonEmptyString("Enter owner's name: ");
            string ownerPhone = getValidPhoneNumber();
            string modelName = getNonEmptyString("Enter model name: ");
            VehiclesType.eVehiclesType vehicleType = getValidEnumInput<VehiclesType.eVehiclesType>();

            r_Garage.CreateNewVehicle(vehicleType, modelName, ownerName, ownerPhone, licenseNumber);
            setInitialEnergyForNewVehicle();
            setInitialWheelPressureForNewVehicle();
            insertAllSpecialAttributes();
            Console.WriteLine("Vehicle successfully added to the garage.");
        }

        private void setInitialEnergyForNewVehicle()
        {
            displayHeader("Add a New Vehicle to the Garage", "Setting Initial Energy");
            bool validInput = false;

            while(!validInput)
            {
                try
                {
                    float energyAmount = getValidFloatInput("Enter the amount of energy currently in the vehicle " +
                                                            "(minutes for electric engine, liters for fuel based engine):");
                    r_Garage.SetInitialEnergy(energyAmount);
                    validInput = true;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void setInitialWheelPressureForNewVehicle()
        {
            displayHeader("Add a New Vehicle to the Garage", "Setting Initial Wheel Pressure");
            bool validInput = false;

            while(!validInput)
            {
                try
                {
                    float airPressure = getValidFloatInput("Enter the amount of air pressure currently in all the wheels:");
                    r_Garage.SetInitialWheelAirPressureForCurrentVehicle(airPressure);
                    validInput = true;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private string getNonEmptyString(string i_Message)
        {
            string input;
            do
            {
                Console.WriteLine(i_Message);
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while(string.IsNullOrEmpty(input));

            return input;
        }

        internal void DisplayVehicleData()
        {
            displayHeader("Display Vehicle Data");
            if (getValidLicenseNumber(out string licenseNumber))
            {
                string details = r_Garage.GetDetails(licenseNumber);
                Console.WriteLine(details);
            }
            else
            {
                Console.WriteLine("Operation canceled. Returning to main menu.");
            }
        }

        internal void ChargeElectricVehicle()
        {
            displayHeader("Charge Electric Vehicle");
            
            if(getValidLicenseNumber(out string licenseNumber))
            {
                float electricityAmount = getValidFloatInput("Enter the minutes you wish to charge the vehicle for: ");

                r_Garage.ChargeElectricVehicle(licenseNumber, electricityAmount);
                Console.WriteLine("Vehicle successfully charged.");
            }
            else
            {
                Console.WriteLine("Operation canceled. Returning to main menu.");
            }
        }

        internal void RefuelVehicle()
        {
            displayHeader("Refuel Vehicle");

            if(getValidLicenseNumber(out string licenseNumber))
            {
                EnergyTypes.eEnergyType energyTypeChoice = getValidEnumInput<EnergyTypes.eEnergyType>();
                float fuelAmount = getValidFloatInput("Enter the wanted amount of fuel to add: ");

                r_Garage.RefuelVehicle(licenseNumber, energyTypeChoice, fuelAmount);
                Console.WriteLine("Vehicle successfully refueled.");
            }
            else
            {
                Console.WriteLine("Operation canceled. Returning to main menu.");
            }
        }

        internal void InflateTiresToMaximumPressure()
        {
            displayHeader("Inflate Tires to Maximum Pressure");

            if(getValidLicenseNumber(out string licenseNumber))
            {
                r_Garage.FillAirInWheelsToMax(licenseNumber);
                Console.WriteLine("Tires inflated to maximum pressure.");
            }
            else
            {
                Console.WriteLine("Operation canceled. Returning to main menu.");
            }
        }

        internal void ShowLicenseNumbers()
        {
            displayHeader("View License Numbers in the Garage");
            bool validInput = false;
            List<string> licenseNumbers = null;

            while(!validInput)
            {
                Console.WriteLine(string.Format("Would you like to filter the license numbers by vehicle status? ({0}/{1}): ", k_YesResponse,k_NoResponse));
                string filterChoice = Console.ReadLine().ToUpper();

                if (filterChoice == k_YesResponse)
                {
                    VehicleStatuses.eVehicleStatus vehicleStatus = getValidEnumInput<VehicleStatuses.eVehicleStatus>();
                    licenseNumbers = r_Garage.GetAllLicenseNumbersByStatus(vehicleStatus);
                    validInput = true;
                }
                else if (filterChoice == k_NoResponse)
                {
                    licenseNumbers = r_Garage.GetAllLicenseNumbers();
                    validInput = true;
                }
                else
                {
                    Console.WriteLine(string.Format("Invalid input. Please enter {0} for Yes or {1} for No.", k_YesResponse,k_NoResponse));
                }
            }

            printLicenseNumbersByStatus(licenseNumbers);
        }

        private void printLicenseNumbersByStatus(List<string> i_LicenseNumbers)
        {
            if(i_LicenseNumbers.Count > 0)
            {
                Console.WriteLine("License Numbers:");
                foreach(string license in i_LicenseNumbers)
                {
                    Console.WriteLine(license);
                }
            }
            else
            {
                Console.WriteLine("No vehicles with the specified status exist in the garage.");
            }
        }

        internal void ChangeVehicleStatus()
        {
            displayHeader("Change Vehicle Status");

            if(getValidLicenseNumber(out string licenseNumber))
            {
                VehicleStatuses.eVehicleStatus newStatus = getValidEnumInput<VehicleStatuses.eVehicleStatus>();

                r_Garage.ChangeVehicleStatus(licenseNumber, newStatus);
                Console.WriteLine("Vehicle status successfully updated.");
            }
            else
            {
                Console.WriteLine("Operation canceled. Returning to main menu.");
            }
        }

        private bool getValidLicenseNumber(out string o_LicenseNumber)
        {
            o_LicenseNumber = null;
            bool isValidLicenseNumber = false;

            while (true)
            {
                Console.WriteLine("Please enter the license number (or press 'Q' to return to the main menu): ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input. Please enter a valid license number.");
                }
                else if (input.ToUpper() == k_QuitFlag)
                {
                    break;
                }
                else if (r_Garage.IsVehicleInGarage(input))
                {
                    o_LicenseNumber = input;
                    isValidLicenseNumber = true;
                    break;
                }
                else
                {
                    Console.WriteLine("The vehicle is not in the garage. Please enter a valid license number.");
                }
            }

            return isValidLicenseNumber;
        }

        private void printAttributeRequest(string i_AttributeName, Type i_AttributeType)
        {
            if(i_AttributeType == typeof(bool))
            {
                Console.WriteLine(string.Format("Please enter if your vehicle has {0} ({1}/{2}):", i_AttributeName, k_YesResponse, k_NoResponse));
            }
            else if(i_AttributeType == typeof(int) || i_AttributeType == typeof(float))
            {
                Console.WriteLine(string.Format("Please enter the {0}:", i_AttributeName));
            }
            else if(i_AttributeType.IsEnum)
            {
                string enumTypeName = EnumHelper.FormatEnumName(i_AttributeType.Name);

                Console.WriteLine(string.Format("Please choose the {0} from the following options:", enumTypeName));
                Console.WriteLine(EnumHelper.GetEnumDescriptions(i_AttributeType)); 
            }
            else
            {
                Console.WriteLine(string.Format("Please enter the {0}:", i_AttributeName));
            }
        }

        private void handleSpecialAttribute(int i_SpecialAttributeIndex)
        {
            KeyValuePair<string, Type> currentAttribute = r_Garage.GetCurrentCarKeyValuePairByIndex(i_SpecialAttributeIndex);
            bool validInput = false;

            while(!validInput)
            {
                try
                {
                    printAttributeRequest(currentAttribute.Key, currentAttribute.Value);
                    string input = Console.ReadLine();
                    r_Garage.InsertSpecialAttributeToCurrentByIndex(i_SpecialAttributeIndex, input);
                    validInput = true;
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
        }

        private void insertAllSpecialAttributes()
        {
            displayHeader("Add a New Vehicle to the Garage", "Gathering Additional Information");
            for (int i = 0; i < r_Garage.GetAmountOfSpecialAttribute(); i++)
            {
                handleSpecialAttribute(i);
            }

            r_Garage.ClearCurrentVehicle();
        }

        private string getValidPhoneNumber()
        {
            string phoneNumber;

            do
            {
                Console.WriteLine("Enter owner's phone number (digits only):");
                phoneNumber = Console.ReadLine();
            } while (!isPhoneNumberValid(phoneNumber));

            return phoneNumber;
        }

        private static bool isPhoneNumberValid(string i_PhoneNumber)
        {
            return i_PhoneNumber.Length == 10 && i_PhoneNumber.StartsWith("05") && i_PhoneNumber.Skip(2).All(char.IsDigit);
        }

        private float getValidFloatInput(string i_Message)
        {
            bool validInput = false;
            float input = 0;

            while(!validInput)
            {
                Console.WriteLine(i_Message);
                if(float.TryParse(Console.ReadLine(), out input))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            return input;
        }

        private TEnum getValidEnumInput<TEnum>() where TEnum : Enum
        {
            bool validInput = false;
            TEnum result = default;
            string enumTypeName = EnumHelper.FormatEnumName(typeof(TEnum).Name);

            while(!validInput)
            {
                try
                {
                    Console.WriteLine(string.Format("Please choose the {0} from the following options:", enumTypeName));
                    Console.WriteLine(EnumHelper.GetEnumDescriptions(typeof(TEnum)));
                    result = EnumHelper.Parse<TEnum>(Console.ReadLine());
                    validInput = true;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return result;
        }

        private void displayHeader(string i_Title, string i_Step = "")
        {
            StringBuilder headerBuilder = new StringBuilder();

            headerBuilder.AppendLine(i_Title);
            if(!string.IsNullOrEmpty(i_Step))
            {
                headerBuilder.AppendLine("> " + i_Step);
            }

            int separatorLength = i_Title.Length + (string.IsNullOrEmpty(i_Step) ? 0 : i_Step.Length + 2);

            headerBuilder.AppendLine(new string('=', separatorLength));
            headerBuilder.AppendLine();
            Console.Clear();
            Console.WriteLine(headerBuilder.ToString());
        }
    }
}
