FROM nginx:alpine

COPY ./Build /usr/share/nginx/html/Build
COPY ./TemplateData /usr/share/nginx/html/TemplateData
COPY ./index.html /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
