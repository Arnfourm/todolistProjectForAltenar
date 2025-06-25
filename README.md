# todolistProjectForAltenar
Небольшой пет проект для компании Альтенар в рамках стажировки
Для создания проекта испольвался стек: 
 - База данных: postgres
 - Бекенд: .NET
 - Фронтенд: React + Vite

Для запуска приложения необходимо сделать следующие действия:
1. Скачать данный репозиторий
2. В директории ./todolistAltenar/backend/todolistProject.API/ в файле appsettings.json изменить строку подключения к базе данных.
3. Также необходимо установить инструмент для выполнения миграций следующей командой:
 - dotnet tool install --global dotnet-ef (Любая ОС)
4. Перейти в директорию ./todolistAltenar/ и выполнить команду docker-compose up для поднятия докер образов и конейтенеров
5. Далее необходимо перейти в директорию ./todolistAltenar/backend/todolistProject.API/ и выполнить миграцию, используя команду:
 - dotnet ef database update
6. Для просмотра Swagger документации API необходимо перейти на localhost:5140/swagger/index.html
7. Для просмтра фронтенда необходимо перейти на localhost:5173
