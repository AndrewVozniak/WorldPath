# Step 1: Build the Angular application
FROM node:18 as build

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json before other files
# Utilize cache to save on future builds with un-changed dependencies
COPY package.json package-lock.json ./

# Install dependencies
RUN npm install

# Copy all files from current directory to working directory in the image
COPY . .

# Build the Angular app in production mode
RUN npm run build

# Step 2: Serve the Angular app with Nginx
FROM nginx:1.19.0-alpine

# Copy the build output from the previous step to Nginx's serve directory
COPY --from=build /app/dist/web /usr/share/nginx/html

# Create a custom Nginx configuration file that sets the server to listen on port 6000
RUN echo "server { listen 8000; location / { root /usr/share/nginx/html; } }" > /etc/nginx/conf.d/default.conf

# Expose port to serve the application
EXPOSE 8000

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
