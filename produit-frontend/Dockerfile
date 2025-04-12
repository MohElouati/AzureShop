# Stage 1: Build Angular app
FROM node:18 AS build

WORKDIR /app

# Copy and install dependencies
COPY produit-frontend/package*.json ./produit-frontend/
WORKDIR /app/produit-frontend
RUN npm install

# Copy full app
COPY produit-frontend/ .

# Build the app (default prod config)
RUN npm run build

# Stage 2: Serve with nginx
FROM nginx:alpine

# Copy built app from stage 1
COPY --from=build /app/produit-frontend/dist/produit-frontend/browser /usr/share/nginx/html

# Copy _redirects (optional, only if it exists in build output)
# If it's inside browser/ after build
COPY --from=build /app/produit-frontend/dist/produit-frontend/browser/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
