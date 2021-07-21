test:
	dotnet test

deploy/local:
	dotnet build --configuration=Release
	rm -rf ~/kasic
	mv kasic/bin/Release/net5.0 kasic/bin/Release/kasic
	mv kasic/bin/Release/kasic ~/ 
