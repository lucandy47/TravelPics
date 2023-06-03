@ECHO OFF
set environment=%1
set imageVersion=%2
set Environment="Development"
TITLE "travelpics-api-%environment%"
ECHO "Image for travelpics-api-%environment%:%imageVersion% will be created and pushed into the pgpcntapps container registry"
ECHO "Creating the image..."
ECHO OFF
docker build -t travelpics-api-%environment%:%imageVersion% --build-arg Environment=%Environment%  --build-arg AppConfigConnectionString="Endpoint=https://travelpicsappconfiguration.azconfig.io;Id=/9U6-l0-s0:oHtG9HIFpsW3rua+iYCD;Secret=KcpyeMOfEhyp1/Uds6MXxzBvWl3Yj2SWPTtzvw7LRw8=" -f ./Dockerfile_api.dockerfile .
ECHO "Logging into the container registry.. "
ECHO OFF
docker login -u travelpicscntapps -p hcKsfCPLqx/kjAZrnEEzqaHpuUAkH3oeEMwJAEHx3T+ACRDMHaIU travelpicscntapps.azurecr.io
docker tag travelpics-api-%environment%:%imageVersion% travelpicscntapps.azurecr.io/travelpics-api-%environment%:%imageVersion%
ECHO "Pushing image to the repository..."
ECHO OFF
docker push travelpicscntapps.azurecr.io/travelpics-api-%environment%:%imageVersion%