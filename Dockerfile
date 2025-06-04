FROM node:22

WORKDIR .

# Copy the app package and package-lock.json file
COPY package*.json ./

# Copy local directories to the current local directory of our docker image (/app)
COPY . .

# Install node packages, install serve, build the app, and remove dependencies at the end
RUN npm install \
    && npm install -g serve \
    && npm run build \
    && rm -fr node_modules

ENV PORT=8089
EXPOSE 8089

# Start the app using serve command
CMD [ "npm", "start" ]
