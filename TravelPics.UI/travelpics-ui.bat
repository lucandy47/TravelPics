@ECHO OFF
set environment=%1
set imageVersion=%2
TITLE "travelpics-ui-%environment%"
ECHO "Image for travelpics-ui-%environment%:%imageVersion% will be created and pushed into the travelpicscntapps container registry"
ECHO "Creating the image..."
ECHO OFF
docker build -t travelpics-ui-%environment%:%imageVersion% -f ./Dockerfile --no-cache .
ECHO "Logging into the container registry.. "
ECHO OFF
docker login -u travelpicscntapps -p hcKsfCPLqx/kjAZrnEEzqaHpuUAkH3oeEMwJAEHx3T+ACRDMHaIU travelpicscntapps.azurecr.io
docker tag travelpics-ui-%environment%:%imageVersion% travelpicscntapps.azurecr.io/travelpics-ui-%environment%:%imageVersion%
ECHO "Pushing image to the repository..."
ECHO OFF
docker push travelpicscntapps.azurecr.io/travelpics-ui-%environment%:%imageVersion%
