@echo off

cls
echo.

cd src\MyBudget.Api.Application

echo RUNINNG Migration...
dotnet ef migrations add Init --startup-project ..\MyBudget.Api\MyBudget.Api.csproj

if %ERRORLEVEL% == 0 (
    echo RUNINNG Database Update...
    dotnet ef database update --startup-project ..\MyBudget.Api\MyBudget.Api.csproj
) else (
    echo Error found: No 'database update' started !
)
echo.

cd ..\..