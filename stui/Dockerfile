FROM node:17 AS base
EXPOSE 80
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.17.1-alpine
COPY --from=base /app/dist/stui /usr/share/nginx/html
