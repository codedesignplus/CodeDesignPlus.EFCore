cd ..

# Vars
$path = "tests\CodeDesignPlus.EFCore.Test"
$project = "$path\CodeDesignPlus.EFCore.Test.csproj"
$report = "$path\coverage.opencover.xml"

# Run Sonnar Scanner
dotnet test $project /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

dotnet sonarscanner begin /k:"CodeDesignPlus.EFCore" /d:sonar.host.url=http://localhost:9000 /d:sonar.cs.opencover.reportsPaths="$report" /d:sonar.coverage.exclusions="**Test*.cs"

dotnet build

dotnet sonarscanner end