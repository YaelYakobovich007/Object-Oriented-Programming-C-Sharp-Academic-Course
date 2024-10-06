<h1 align="center">
  <img src="https://i.gifer.com/U0Au.gif" width="30%" alt="logo"/>
  <br/>
  Welcome to the Interfaces and Delegates Project
  <img src="https://media.giphy.com/media/hvRJCLFzcasrR4ia7z/giphy.gif" width="5%" alt="waveEmoji"/>
</h1>

<h2 align="center">
   A hierarchical menu system built using C# with interfaces and delegates for dynamic and flexible menu management.
    <br/><br/> 
</h2>

<br/>

<h2> Table of Contents </h2>
<ul>
  <li><a href="#overview">Overview</a></li>
  <li><a href="#features">Features</a></li>
  <li><a href="#technical-stack">Technical Stack</a></li>
  <li><a href="#class-structure">Class Structure</a></li>
  <li><a href="#menu-management-techniques">Menu Management Techniques</a></li>
  <li><a href="#how-to-use">How to Use</a></li>
  <li><a href="#installation">Installation</a></li>
  <li><a href="#run">Run</a></li>
  <li><a href="#credits">Credits</a></li>
</ul>

<br/>

<h2 id="overview"> 
 :heavy_check_mark: Overview 
</h2>
<p>
   This project demonstrates a hierarchical menu system built using <b>interfaces</b> and <b>delegates</b> in C#. It allows for flexible and dynamic management of nested menus where each menu or submenu can trigger actions based on user selections. The <b>MainMenu</b> class is responsible for displaying the menus, allowing the application to add and organize menus at multiple levels. This system can be applied in various scenarios, such as managing menus in console-based applications, including the garage management system from the previous exercise.
</p>

<br/>

<h2 id="features"> 
  :heavy_check_mark:Features 
</h2>
<ul>
  <li>Dynamic Menu System with nested menus</li>
  <li>Action Delegation for dynamic, non-hardcoded functionality</li>
  <li>Modular design for easy integration</li>
  <li>Integrated error handling for invalid selections</li>
</ul>

<br/>

<h2 id="technical-stack"> 
  :gear:Technical Stack
</h2>
<ul>
  <li><b>Language</b>: C#</li>
  <li><b>Framework</b>: .NET</li>
  <li><b>IDE</b>: Visual Studio</li>
  <li><b>Project Type</b>: Console-based application</li>
</ul>

<br/>
<br/>

<h2 id="class-structure"> 
  :clipboard: Class Structure
</h2>
<ul>
  <li><b>MainMenu.cs</b>: Handles the main entry point for building and displaying the menu structure. It allows the user to create nested menus and assign actions.</li>
  <li><b>MenuItem.cs</b>: The base class for menu items, providing the basic structure for both main and sub-items.</li>
  <li><b>InnerMenu.cs</b>: Represents submenus, which can contain additional `MenuItem` objects. It allows the creation of hierarchical menus.</li>
  <li><b>LeafItem.cs</b>: Represents the final items that do not contain submenus and are used to trigger specific actions through delegates.</li>
  <li><b>IActionSelect.cs</b>: Provides an interface for the selection of actions in the menu system, defining how each menu item should behave.</li>
  <li><b>MenuDelegate.cs</b>: Defines and manages the delegate-based actions, allowing the dynamic assignment of functionality to menu items.</li>
</ul>

<br/>

<h2 id="menu-management-techniques"> 
 🥷 Menu Management Techniques
</h2>

<h3>Using Interfaces</h3>
<p>
   The interface-based method allows for defining a contract that menu items must adhere to. By implementing the `IActionSelect` interface, each menu item can ensure that it has a consistent structure and behavior, allowing for flexible addition of items without worrying about implementation details.
</p>

<p><b>Key Benefits:</b></p>
<ul>
  <li><b>Modularity</b>: The interface ensures that new menu items can be added easily by adhering to the same contract.</li>
  <li><b>Consistency</b>: All menu items, whether they are main menu items or submenus, have the same structure and behavior.</li>
  <li><b>Scalability</b>: It is easy to extend the system with new types of menu items without significant changes to the existing codebase.</li>
</ul>

<h3>Using Delegates</h3>
<p>
   The delegate-based method provides flexibility by allowing dynamic assignment of actions to menu items. Using `Action<T>`, each menu item can trigger a different behavior based on the delegate assigned to it, enabling dynamic functionality without hardcoding specific behaviors.
</p>

<p><b>Key Benefits:</b></p>
<ul>
  <li><b>Flexibility</b>: Actions are not hardcoded into the menu items. Instead, they are assigned dynamically through delegates, allowing easy modification.</li>
  <li><b>Reusability</b>: Actions can be reused across different menu items, enhancing code reuse and reducing duplication.</li>
  <li><b>Loose Coupling</b>: Menu logic is decoupled from specific action logic, allowing easier maintenance and updates.</li>
</ul>

<br/>

<h2 id="how-to-use"> 
  :video_game:How to Use
</h2>

<p>Follow these steps to use the menu system:</p>
<ol>
  <li><b>Start the Program</b>: Run the program, and you will be presented with a list of menu options in the console.</li>
  <li><b>Navigate Menus</b>: Use the numbered options to move through the menus and select actions.</li>
  <li><b>Execute Actions</b>: Selecting a menu item will trigger its associated action, such as displaying text or performing calculations.</li>
  <li><b>Back or Exit</b>: Options are provided at each level to return to the previous menu or exit the program.</li>
</ol>

<br/>

<h2 id="installation"> 
 :wrench:Installation
</h2>
<p>To install and set up the project:</p>
<ol>
  <li>Download the project files from the repository.</li>
  <li>Set up your environment for <b>C#</b> and <b>.NET</b> development (recommended IDE: <b>Visual Studio</b>).</li>
  <li>Ensure all necessary dependencies are installed.</li>
</ol>

<br/>

<h2 id="run"> 
  :arrow_forward:Run
</h2>
<p>To run the project:</p>
<ol>
  <li>Open the project in <b>Visual Studio</b>.</li>
  <li>Run the `Program.cs` file to start the application.</li>
  <li>Follow the on-screen prompts to navigate the menus and trigger actions.</li>
</ol>

<br/>

<h2 id="credits"> 
  :trophy:Credits
</h2>
<p>Created by: Yael Yakobovich</p>
