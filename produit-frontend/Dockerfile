# Stage 1: Build Angular App
FROM node:18 AS build

WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy source code
COPY . .

# Build app
RUN npm run build -- --configuration=production

# Stage 2: Serve with NGINX
FROM nginx:alpine

# Copy built app to NGINX HTML folder
COPY --from=build /app/dist/produit-frontend /usr/share/nginx/html

# Optional: enable this if you use client-side routes (e.g., Angular routing)
# COPY public/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
