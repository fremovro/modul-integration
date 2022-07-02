# Интеграция программных модулей, реализованных на различных языках программирования

Цель – реализовать приложение, позволяющее решать задачу «о рюкзаке» с помощью
различных алгоритмов. Приложение должно состоять из двух компонент:
1) основная программа, позволяющая вводить и редактировать входные данные, выбирать
алгоритм решения, запускать его и наглядно представлять результаты работы
(реализуется на языке C#);
2) библиотека, в которой находится класс, непосредственно реализующий алгоритм
решения (реализуется на языке F#).
В основной программе происходит подключение библиотеки (статически или динамически).
Для успешного использования библиотеки находящийся в ней класс должен реализовывать
заданный интерфейс.