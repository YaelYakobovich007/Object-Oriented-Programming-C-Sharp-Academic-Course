<h1 align="center">
  <img src="https://i.gifer.com/Aa1d.gif" width="25%" alt="logo"/>
  <br/>
  Welcome to the Garage Management Application
  <img src="https://media.giphy.com/media/hvRJCLFzcasrR4ia7z/giphy.gif" width="5%" alt="waveEmoji"/>
</h1>

<h2 align="center">
   A console-based Garage Management System built using C# and OOP principles. Manage vehicles, handle energy types, and perform diagnostics with ease!
</h2>

<br/>

# Table of Contents
* [Overview](#overview)
* [Features](#features)
* [Technical Stack](#technical-stack)
* [Class Structure](#class-structure)
* [Vehicle Types Supported](#vehicle-types)
* [Installation](#installation)
* [Run](#run)
* [Credits](#credits)

<br/>

# :heavy_check_mark: **Overview** <a name="overview"/>

This project is a **console-based Garage Management Application** written in C#. It supports the management of different types of vehicles, including electric and fuel-based vehicles. The system allows users to register new vehicles, update vehicle status, and perform various diagnostic checks on the vehicles. The application also provides tools for monitoring energy levels, inflating tires, and handling repairs.

<br/>

# :heavy_check_mark: **Features** <a name="features"/>

1. **Vehicle Management**:
   - Register and manage multiple vehicle types in the garage (cars, motorcycles, trucks).
   - Update the vehicle's status (e.g., in repair, ready for delivery, paid).

2. **Energy Management**:
   - Supports **fuel-based** and **electric vehicles**.
   - Recharge electric vehicles or refuel fuel-based vehicles directly from the system.

3. **Diagnostics**:
   - Inflate tires to maximum pressure.
   - Perform diagnostics and energy level checks for all vehicles.
   - Retrieve details of vehicles by their license number.

4. **Error Handling**:
   - Proper validation for input data, ensuring smooth operation and meaningful feedback on incorrect values.

<br/>

# :gear: **Technical Stack** <a name="technical-stack"/>

- **Language**: C#  
- **Framework**: .NET  
- **IDE**: Visual Studio  
- **Project Type**: Console-based application

<br/>

# :clipboard: **Class Structure** <a name="class-structure"/>

1. **GarageManager.cs**: Manages the core logic for handling vehicles, updating their status, and interacting with the garage system.
   
2. **Vehicle.cs**: The base class representing a general vehicle, containing common properties like license number, owner details, and energy status.

3. **Car.cs, Motorcycle.cs, Truck.cs**: Derived classes from `Vehicle` that represent specific types of vehicles with their unique properties.

4. **Engine.cs**: Abstract base class representing an engine (either fuel-based or electric).

5. **FuelBasedEngine.cs & ElectricEngine.cs**: Derived classes from `Engine` that implement specific behaviors for fuel-based and electric vehicles.

6. **Wheel.cs**: Represents the wheels of a vehicle, handling their properties such as current air pressure and manufacturer.

7. **VehicleFactory.cs**: Responsible for creating instances of vehicles based on the user's input.

8. **UI.cs**: Handles the user interface logic for the console, allowing interaction with the user and managing input/output.

<br/>

# :oncoming_automobile: **Vehicle Types Supported** <a name="vehicle-types"/>

1. **Cars**:  
   - Fuel or electric-based.
   - Specific properties include color and number of doors.

2. **Motorcycles**:  
   - Fuel or electric-based.
   - Specific properties include engine capacity and license type.

3. **Trucks**:  
   - Fuel-based.
   - Specific properties include cargo capacity and hazardous materials indicator.

<br/>

# :wrench: **Installation** <a name="installation"/>

1. **Download the project files** from the repository.
2. **Set up your development environment** with C# and .NET support (Visual Studio recommended).
3. Ensure that **all dependencies are installed**, and the project is configured for a console-based application.

---

# ▶️ **Run** <a name="run"/>

1. **Open the project** in Visual Studio.
2. **Run the `Program.cs`** file to start the Garage Management system.
3. Follow the **on-screen prompts** to add, update, or query vehicles in the garage.
4. Manage vehicle status, handle energy levels, and perform diagnostic checks using the console interface.

---

# :trophy: **Credits** <a name="credits"/>
> Created by: Yael Yakobovich

---

