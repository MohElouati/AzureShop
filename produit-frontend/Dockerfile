FROM node:18 AS build

WORKDIR /app


COPY produit-frontend/package*.json ./
RUN npm install

COPY produit-frontend/ ./

RUN npm run build -- --configuration production

FROM nginx:alpine

COPY --from=build /app/dist/produit-frontend /usr/share/nginx/html
COPY produit-frontend/public/_redirects /usr/share/nginx/html/_redirects

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
