# RedMachine

[Постановка задачи](https://github.com/fornetjob/RedMachine/blob/master/RedMachine/Assets/Editor/Documentation/ProgrammerTest.pdf)

## Разработано с использованием

[Unity 2017.4.22f1 (64-bit)](https://unity3d.com/unity/whats-new/2017.4.22) - ведущая в мире платформа для создания кросплатформенных игр и приложений

## Структура проекта

[Основные фичи](https://github.com/fornetjob/RedMachine/tree/master/RedMachine/Assets/Scripts/Features)
- Board - Игровое поле
- Move - Движение юнитов в указанном направлении, с заданной скоростью
- Bounces - Отталкивание юнитов от стен, друг друга, если одного типа и уменьшение радиуса, если разных типов
- Serialize - Сохранение и загрузка состояния, чтение конфигурации
- UI - Пользовательский интферфейс

[Инициализация контекста](https://github.com/fornetjob/RedMachine/blob/master/RedMachine/Assets/Scripts/GameBehaviour.cs)

[Тесты](https://github.com/fornetjob/RedMachine/tree/master/RedMachine/Assets/Editor)

## Используемые шаблоны

- Entity-Component-System - Отделение логики от данных, быстрая сериализация состояния, тестируемость, пакетная обработка состояний
- Object Pool - Кеширование объектов
- Factory Method - Инкапсуляция кода создания объектов
- Composite - Объединение систем, событий
- Observer - События
- Service locator - Для доступа к сервисам, проблема паттерна смягачается передачей контекста на интерфейсе или конструкторе, инициализацией зависимостей в точке передачи
