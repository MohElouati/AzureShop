# Stage 1: Build the Angular app
FROM node:18 AS build

WORKDIR /app

COPY produit-frontend/package*.json ./
RUN npm install

COPY produit-frontend/ ./
RUN npm run build

# Stage 2: Serve with nginx
FROM nginx:alpine

COPY --from=build /app/dist/produit-frontend/browser /usr/share/nginx/html

# Optional: Netlify-style redirect support
COPY produit-frontend/public/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
