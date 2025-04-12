# Stage 1: build the app
FROM node:18 AS build

WORKDIR /app

COPY produit-frontend/package*.json ./
RUN npm install

COPY produit-frontend/ ./
RUN npm run build

# Stage 2: serve with nginx
FROM nginx:alpine

COPY --from=build /app/dist/produit-frontend /usr/share/nginx/html

# Optional: only if using Netlify-style SPA redirects
# COPY produit-frontend/public/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
