# Use a Node.js base image
FROM node:14

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
# We copy both package.json and package-lock.json to capture all dependencies
COPY package*.json ./

# Install node modules
RUN npm install

# Bundle app source
COPY . .

# The port your application listens on
EXPOSE 3000

# Start the application
CMD [ "node", "main.js" ]
