# Stage 1: Build the Angular app
FROM node:18 AS build

WORKDIR /app

# Copy package files and install dependencies
COPY produit-frontend/package*.json ./
RUN npm install

# Copy all frontend source files
COPY produit-frontend/ ./

# Build the Angular app
RUN npm run build -- --configuration production

# Stage 2: Serve with NGINX
FROM nginx:alpine

# Copy built app to NGINX's default directory
COPY --from=build /app/dist/produit-frontend /usr/share/nginx/html

# Optional: copy _redirects file if you use client-side routing (e.g. Angular routes)
# Uncomment the line below ONLY if the file exists
# COPY produit-frontend/public/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
