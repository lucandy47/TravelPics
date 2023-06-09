#Create the node stage
FROM node:latest as builder
#SET the working directory
WORKDIR /app

COPY package.json package-lock.json ./
#Run npm install & build the application
RUN npm install -g npm@9.6.7
RUN npm ci --force
#Copy the file from current directory to Workingdirectory
COPY . .

RUN npm run build-dev

#Create the nginx stage fr serving the content
FROM nginx:alpine
# Set the working directory to nginx assets directory
WORKDIR /usr/share/nginx/html

#Remove the default nginx static files
RUN rm -rf ./*

COPY ./config/nginx-custom.conf /etc/nginx/conf.d/default.conf
#Copy the static content from the builder stage
COPY --from=builder /app/dist/travel-pics.ui .
#Container run the nginx with global directive and Daemmon off
