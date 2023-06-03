@ECHO OFF
set environment=%1
set imageVersion=%2
TITLE "travelpics-notifications-%environment%"
ECHO "Image for travelpics-notifications-%environment%:%imageVersion% will be created and pushed into the pgpcntapps container registry"
ECHO "Creating the image..."
ECHO OFF
docker build -t travelpics-notifications-%environment%:%imageVersion% -f ./Dockerfile_notifications.dockerfile .
ECHO "Logging into the container registry.. "
ECHO OFF
docker login -u MainToken -p hcKsfCPLqx/kjAZrnEEzqaHpuUAkH3oeEMwJAEHx3T+ACRDMHaIU travelpicscntapps.azurecr.io
docker tag travelpics-notifications-%environment%:%imageVersion% travelpicscntapps.azurecr.io/travelpics-notifications-%environment%:%imageVersion%
ECHO "Pushing image to the repository..."
ECHO OFF
docker push travelpicscntapps.azurecr.io/travelpics-notifications-%environment%:%imageVersion%