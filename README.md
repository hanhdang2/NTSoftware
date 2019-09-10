# NTSoftware

## Steps to Setup the .NET Core Back end app (NTSoftware)

1. **Clone the application**

2. **Change MSSQL username and password as per your MSSQL installation**
  + open `NTSoftware/NTSoftware/appsettings.json` file.
  + change `DefaultConnection` properties as per your MSSQL installation: `Server=localhost;Database=NTSoftware;User Id=sa;Password=YOURPASSWORD;MultipleActiveResultSets=true`
  
4. **Run Migrations using .NET Core CLI**
 
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
5. **Run App**



## Steps to Setup the Angular Front end app (NTSoftware/NTSoftware/ClientApp)
First go to the `ClientApp` folder -

```bash
cd NTSoftware/NTSoftware/ClientApp
```


Then type the following command to install the dependencies and start the application -

```bash
npm install
```
```bash
ng serve --open
```
The front-end server will start on port `4200`.
