dotnet test TestFramework.sln /p:CollectCoverage=true  /p:CoverletOutput=./code-coverage/ /p:CoverletOutputFormat=cobertura

reportgenerator "-reports:./tests/TestFramework.UnitTests/code-coverage/coverage.cobertura.xml" "-targetdir:./tests/TestFramework.UnitTests/code-coverage/coveragereport" -reporttypes:Html