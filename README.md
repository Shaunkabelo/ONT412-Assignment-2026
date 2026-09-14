# Assignment 2 — editable Windows Forms project

## Open and edit in Visual Studio

1. Open **this folder's `CallCenter.sln`** using File > Open > Project/Solution. Do not open the older solution in the parent folder.
2. In Solution Explorer, expand the **CallCenter** project.
3. Right-click **MainForm.cs** and choose **View Designer**, or select it and press **Shift+F7**.
4. Drag controls to move or resize them. Select a control and press **F4** to edit its properties. Use the Toolbox to add controls.
5. Save, then press **F5** to run. Stop debugging before changing the layout.

All 28 visible controls are standard Windows Forms controls, declared and configured inside `MainForm.Designer.cs`. Their location, size, captions, colours and fonts are not reset by runtime layout code. `MainForm.resx` stores form resources. The project explicitly marks `MainForm.cs` as a Form and nests the Designer and resource files beneath it.

The `MainForm.cs` constructor only calls `InitializeComponent()`. `MainForm_Load` loads saved history, binds the role/device lists and starts the timer at runtime. Status text and enabled button states change with the call state. Buttons use named event handlers visible in the Designer's Events properties. Keep business logic in `MainForm.cs` and `Models.cs`; let Visual Studio maintain `MainForm.Designer.cs`.

If View Designer is missing, ensure you opened the solution/project rather than using Open Folder, and that Visual Studio has the **.NET desktop development** workload and support for the installed .NET 9 SDK. Build the solution before reopening the Designer.

## Assignment functionality

- **State:** `Call`, `ICallState`, `OnCallState`, `OnHoldState`, `HungUpState`. Hold blocks speech and pauses talk duration; Resume continues; Hang Up finalizes the duration.
- **Iterator:** `ICallIterator` and `CallLogIterator`. First/Next/Previous/IsDone/CurrentItem navigate searchable call results.
- **Composite:** `CallLogComponent`, `SingleCall`, `CallGroup`, `CallLog`. The log tree groups calls by student, technician and manager role.
- **Proxy:** `ICallService`, `CallProxy`, `RealCallService`. Manager callbacks check the selected role and create a new call linked to the original caller. Only one call can be active at a time.

Enter caller and receiver details, select a role and device, and Make Call. Hold, Speak, Resume and Hang Up demonstrate State behaviour. Search by caller, receiver, call ID or role; View all calls clears the filter. Select a completed call, select Manager, enter the manager's name as caller and click Manager: return selected.

Phone calls/audio are simulated; device selection is metadata and role selection is not authentication. Completed calls persist in `%LOCALAPPDATA%\CallCenterAssignment2\calls.json`, separately from the previous project. Closing normally ends any active call and saves history. Unexpected termination can lose an unfinished call. Load errors disable saving for that session to protect the existing file; save errors are displayed.

## Build, checks and submission

From this folder:

```powershell
dotnet build CallCenter.sln
dotnet run --project Verification/Verification.csproj
dotnet run --project Verification/Verification.csproj -- --designer-check
dotnet run --project Verification/Verification.csproj -- --documents
```

The form check verifies controls exist as named fields before Load and that runtime startup is deferred. It does not automate the Visual Studio Designer itself.

`Submission/Assignment-Submission.docx` contains pattern motivations, the unified diagram and this project's full source. `Submission/Unified-Class-Diagram.puml` is the editable diagram source. After modifying code or layout, regenerate the Word document before adding final group and video details; generation replaces that document.

Follow `Submission/Recording-Guide.md` to record the application, upload the video, paste its viewing URL and have a friend verify access. Those steps still require completion; no video or sharing confirmation is included.
