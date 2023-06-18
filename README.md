## Какво представлява проектът?
Проектът представлява сайт за рецепти, в който потребителите имат достъп до рецепти от различни категории и кухни.
Проектът поддържа три роли: Admin, Chef и User. 

## Роли
Администраторът има достъп до абсолютно всичко в сайта - може да преглежда, създава, изтрива и обновява всички обекти в сайта, за които е имплементирана такава функционалност. Той притежава и възможността да запазва в базата информация чрез API за случайно избрани рецепти като подава: брой на рецепти; име на категорията на рецептите; име на кухнята, от която да са рецептите. Всяка регистрация в сайта създава акаунт на обикновен потребител - User. Хората без акаунт в сайта имат достъп само до Home и About страниците. Потребителите с роля User могат да разглеждат всички налични рецепти, могат да преглеждат ревютата за всяка рецепта и да пишат такива, могат да преглеждат различните категории рецепти и съответните рецепти от всяка категория, аналогиично могат да разглеждат различните видове кухни и рецептите отнасящи се до всяка от тях. Потребителите с роля Chef имат права като на User потребителите, но могат и да добавят нови рецепти, както и да редактират и изтриват качените от тях рецепти.

## Меню
Менюто съдържа линкове към Home, About, All Recipes, Add New Recipe (Admin, Chef), All Users (Admin), Recipe Categories и Cuisines.

### Home и About
Съдържат основна информация.

### All Recipes
Съдържа списък с всички рецепти, възможност за редактиране и изтриване на всяка от тях, възможност за преглеждане на рецепта в детайли, възможност за преглеждане, добавяне и изтриване на ревюта.

### Add New Recipe
Съдържа форма за добавяне на нова рецепта, както и линк към форма за добавяне на рецепти с API.

### All Users
Съдържа списък с информация за всички потребители, както и възможност за изтриване и променяне на ролята на всеки от тях.

### Recipe Categories
Съдържа списък с всички категории рецепти, възможност за редактиране и изтриване на всяка от тях, възможност за добавяне на нова категория, възможност за преглеждане на всички рецепти от дадена категория.

### Cuisines
Съдържа списък с всички кухни, възможност за редактиране и изтриване на всяка от тях, възможност за добавяне на нова кухня, възможност за преглеждане на всички рецепти от дадена кухня.
