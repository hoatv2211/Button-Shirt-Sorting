# Button to Shirt Sorting - Unity Game Project

## 📌 Project Overview
Button to Shirt Sorting is a **Unity-based casual game** where players drag and drop buttons into their matching shirt slots. The goal is to complete the level by sorting all buttons correctly within a time limit.

This project is structured with **optimized game logic**, a **modular UI system**, and **performance considerations** for mobile devices.
  
## **🎮 APK Gameplay Demo**
📌 Link APK: https://github.com/hoatv2211/Button-Shirt-Sorting/blob/main/Build_APK/Demo.apk

## **🎥 Gameplay Demo**
📌 **Video Link:** https://github.com/hoatv2211/Button-Shirt-Sorting/blob/main/Videos/bandicam%202025-03-10%2016-21-23-824.mp4

## 🎮 Features
### **Core Features**
✅ **Randomized Button Generation** - Spawns 5-10 buttons with different colors per level.  
✅ **Drag & Drop Interaction** - Players can interact with buttons and place them in the correct slots.  
✅ **Win Condition** - The level is completed when all buttons are correctly placed.  
✅ **Lose Condition** - The game ends if the time runs out.  
✅ **Basic UI Feedback** - Displays the number of remaining buttons and win/lose status.  


### **Game Modes**
🎯 **Level Mode** - Players progress through **100 predefined levels** with increasing difficulty. Additional levels can be created using the Level Editor Tool.  
♾️ **Endless Mode** - Play unlimited levels and buttons appear randomly with increasing difficulty

### **Bonus Features**
✨ **Level Progression** - Multiple difficulty levels using ScriptableObjects.  
✨ **Hint System** - Highlights the correct button-slot match if the player takes too long.  
✨ **Ad Monetization Mockup** - A Rewarded Ad button that auto-places a button.  
✨ **Performance Optimization** - Utilizes Object Pooling to improve frame rates and reduce memory usage.  

## 🛠️ Technical Details
- **Unity Version:** 2021 LTS or later <version đang chạy: 2022.3.47f1>
- **Rendering Pipeline:** Universal Render Pipeline (URP) for optimized mobile performance
- **Language:** C#
- **Design Pattern:** Modular architecture with ScriptableObjects for level data
- **Optimizations:** Object pooling, physics optimizations, minimal garbage collection

## 📂 Project Structure
```
/Button-Shirt-Sorting-main
 ├── .gitignore
 ├── Build_APK (Contains build APK files)
 ├── Client-ButtonShirtSorting (Main Unity project)
 │   ├── Assets (Game assets & scripts)
 │   │   ├── _Main (Core game folder)
 │   │   │   ├── Scripts (Game logic & systems)
 │   │   │   │   ├── GameManager.cs (Handles game flow)
 │   │   │   │   ├── AdsManager.cs (Google AdMob integration)
 │   │   │   │   ├── SoundManager.cs (Manages audio effects)
 │   │   │   │   ├── LevelDesign/ (Stores level data with ScriptableObjects)
 │   │   │   │   ├── UI/ (Handles UI elements)
 │   │   │   ├── Prefabs (Reusable game objects like buttons and slots)
 │   │   │   ├── Resources (Stores level configuration data)
 │   │   │   ├── _Scenes (Contains all game scenes)
 │   │   │   ├── Arts & Sounds (Game textures, sprites, and sound assets)
 │   ├── Packages (Unity package dependencies)
 │   ├── ProjectSettings (Unity configuration files)
 ├── README.md (Project documentation)
```

## 🚀 How to Run the Project
### **1️⃣ Clone the Repository**
```sh
git clone https://github.com/hoatv2211/Button-Shirt-Sorting.git
```
### **2️⃣ Open in Unity**
- Ensure you have **Unity 2021 LTS or later** installed.
- Open the **Client-ButtonShirtSorting** folder in Unity.

### **3️⃣ Run the Home Scene & Select Game Mode**
- Navigate to **Assets/_Main/_Scenes**.
- Open and run **Home.unity**.
- Choose between **Level Mode** or **Endless Mode** from the main menu.

### **4️⃣ Build & Export APK (For Mobile Testing)**
- Go to **File → Build Settings**.
- Select **Android** → `Build & Run`.

## **📜 Key Scripts & Their Functions**
| **Script Name**    | **Description** |
|--------------------|----------------|
| `GameplayCtrl.cs`  | Manages game logic and level control. |
| `UIMainGame.cs`  | Controls UI flow and interactions. |
| `AdsManager.cs`   | Integrates Google AdMob for monetization. |
| `SoundManager.cs` | Manages background music and sound effects. |
| `Module.cs`       | Handles reusable game modules. |
| `LevelDesign/`    | Stores level data using ScriptableObjects. |
| `UI/`             | Manages all UI elements including menu, in-game UI, and results. |

## 🛠 Level Editor (ToolLevels Scene)
This project includes a **Level Editor Tool** inside the `ToolLevels` scene.  
With this tool, you can **design custom levels** by manually placing buttons and slots.
![image](https://github.com/user-attachments/assets/6087769a-381d-4e2c-8483-530ac27f7a48)

### **How to Access the Level Editor**
1. **Open Unity** and navigate to **Assets/_Main/_Scenes/**.
2. **Open `ToolLevels.unity`** scene.
3. You will see the **Level Editor UI**, allowing you to place buttons and slots.

### **How to Create a New Level**
1. Select **ToolLevels Scene** and press **Play**.
2. Use the editor interface to **place buttons and shirt slots** manually.
3. Assign colors and positions to match the level design.
- **TMP_Dropdown:** Select an existing level or click "AddNew" to create a new level.
- **Customizable Variables:**
  - `isRandom` - Determines whether the level is fixed or randomly generated.
  - `timeCD` - Sets the time limit for the level.
  - `Remain` - Defines the number of slots/buttons.
  - `Autogen` - Enables automatic object spawning.
  - `Add Point` - Adds a new slot.
  - `Remove Point` - Deletes a slot.
4. Click **Save Level** to store the level data.
5. Click **Play** to test the newly created level.

### **How Levels are Stored**
- Levels are saved as **ScriptableObjects** in the `Assets/Resources/Levels/` directory.
- The game dynamically loads levels from these files.
- To manually edit level data, open the corresponding `.asset` file.

✅ **Using this tool makes it easy to create and test new levels without modifying code!**



## **🤝 Contributing**
- **Fork** the repository and create a new branch for your feature.
- **Submit a pull request** with a clear description of changes.
- If you find any issues, please create an **issue** on GitHub.

## **📜 License**
🚀 **MIT License** - You are free to use and modify this project as needed!

---

🚀 **Enjoy developing and happy coding!** 🎮
