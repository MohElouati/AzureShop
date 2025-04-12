# Step 1: Build Angular app
FROM node:18 as build

WORKDIR /app

COPY produit-frontend/package*.json ./
RUN npm install

COPY produit-frontend/ ./
RUN npm run build -- --configuration production

# Step 2: Serve with NGINX
FROM nginx:alpine

COPY --from=build /app/dist/produit-frontend /usr/share/nginx/html

# Optional redirect fix for SPA routing on Render
COPY produit-frontend/public/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
