FROM node:22-slim

WORKDIR /app

COPY package*.json ./

# Install ALL dependencies (needed for react-scripts)
RUN npm install

COPY . .

# Build the app
RUN npm run build

# Install "serve" to serve the build output
RUN npm install -g serve

# Expose port
EXPOSE 3000

# Start the app using serve command
CMD [ "npm", "start" ]
