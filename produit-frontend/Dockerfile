FROM node:18 AS build

WORKDIR /app

# Correctly copy package files from subfolder
COPY produit-frontend/package*.json ./

RUN npm install

# Copy the full frontend code
COPY produit-frontend/ ./

RUN npm run build -- --configuration production


FROM nginx:alpine

# Copy the built app into NGINX public folder
COPY --from=build /app/dist/produit-frontend /usr/share/nginx/html

# Optional: for routing (if you have a _redirects file)
COPY produit-frontend/public/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
