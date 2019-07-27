@echo off

cls
echo.

cd src\MyBudget.Customers.Api

echo RUNINNG Migration...
dotnet ef migrations add Init 

if %ERRORLEVEL% == 0 (
    echo RUNINNG Database Update...
    dotnet ef database update
) else (
    echo Error found: No 'database update' started !
)
echo.

cd ..\..