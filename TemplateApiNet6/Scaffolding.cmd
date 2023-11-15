@echo off
REM dotnet new tool-manifest
REM dotnet tool install dotnet-ef
REM dotnet add package Microsoft.EntityFrameworkCore.Design
REM Microsoft.EntityFrameworkCore.Sqlite

REM Preare the needed tools
dotnet tool restore
IF '%errorlevel%' NEQ '0' (
    echo "could not restore the needed tools"
    pause
    goto end
)

REM Try creating an updated version of the models that exist in the database
ren "Database\Models" "ModelsTemp"
dotnet ef dbcontext scaffold name=DatabaseContext Microsoft.EntityFrameworkCore.Sqlite --data-annotations --no-onconfiguring -c DatabaseContext --context-dir Database --output-dir Database/Models --force
IF '%errorlevel%' NEQ '0' (
    ren "Database\ModelsTemp" "Models"
    pause
) ELSE (
    rmdir /s /q "Database\ModelsTemp"
)

:end