# PostApp

Test task for BidOne by Alexander Hagen-Thorn

## What it does
The web application shows a simple form to asking for a person's first and last names. When the form is submit, the information is saved as a JSON file in a configurable location.

The application has a typical architecture for a full stack web app, even though some used patterns may be an overkill for such a small project.

## How to run
### From IDE
Starting project from IDE will start both backend and frontend. The frontend opens in the browser. It will be watching for source updates and recompile.

This way requires IDE, ASP.Net 7.0 Web SDK, NodeJS (tested with v18), Angular(tested with v14) to be installed

### From command line with dotnet
`dotnet run` from the project folder fill do the same as above (but you do not need IDE)

### After publishing to a folder
If you publish the app into a folder, it will assemble the backend and frontend together. Running the `PostApp.exe` or `dotnet PostApp.dll` will start the app; it will be accessible by URL `http://localhost:5000`
This way requires .Net 7.0 runtime to be installed.

### In Docker
From the solution folder, give `docker compose up` command. The app will run on `http://localhost/` (port 80), saving data to a docker volume.
This way requires Docker to be installed. This is a recommended way for production environments.

## How to run tests
### Backend tests
Can be run from IDE or by `dotnet test` command from the solution folder

### Frontend tests
Can be run by `ng test` command from PostApp/ClientApp folder.

