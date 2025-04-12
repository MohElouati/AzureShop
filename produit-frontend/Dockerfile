# ---- Build stage ----
  FROM node:18 AS build
  WORKDIR /app

  COPY package*.json ./
  RUN npm install

  COPY . .
  RUN npm run build -- --configuration production

  # ---- NGINX stage ----
  FROM nginx:alpine
  COPY --from=build /app/dist/produit-frontend /usr/share/nginx/html

  # Optional: clean routing for SPA
  COPY ./public/_redirects /usr/share/nginx/html/_redirects

  EXPOSE 80
  CMD ["nginx", "-g", "daemon off;"]
