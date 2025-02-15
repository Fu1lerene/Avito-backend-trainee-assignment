# Merch Store API

## Запуск проекта

### 1. Запустить контейнеры
Выполнить команду в корневой директории проекта:
```sh
docker-compose up -d
```
Данная команда собирает образ из Dockerfile и запускает контейнеры PostgreSQL и API

### 2. Проверка работы контейнеров
Можно убедиться, что контейнеры поднялись с помощью команды
```sh
docker ps
```

### 3. Доступ к API
API будет доступен по адресу:
```
http://localhost:8080
```
Чтобы использовать сваггер необходимо перейти по адресу:
```
http://localhost:8080/swagger
```

## 4. Остановка контейнеров
Команда для остановки работы контейнеров:
```sh
docker-compose down
```
