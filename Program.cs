//класс Parallel содержит в себе несколько функций для распараллеливания
//работу по созданию потоков он берет на себя
//функция Parallel.For представляет собой параллельный цикл
long sum = 0L;
object o  = new object();
long group_size = 100;
long count = 2000000000;
var po = new ParallelOptions();// переменная из класса ParallelOptions
po.MaxDegreeOfParallelism = 8;// максимальное кол-во потоков
Parallel.For(//объявляется цикл 
    1L,//начальное значение (то, от чего будет изменяться переменная)
        count / group_size + 1L,//верхняя граница, которая причем исключается из диапазона
        po,// передаем кол-во потоков
        i =>//лямбда-выражение, которое определяет тело цикла
        {// это выражение принимает переменную i в качестве параметра (в нашем случае это индекс, который мняется начиная с единицы)
            long from = (i - 1L) * group_size + 1L;
            long to = i * group_size + ((i == count / group_size) ? count % group_size : 0L);
            var s = 0L;
            for (long j = from; j <= to; j++)
            {
                s += j;
            }
            lock (o) { sum += s; }// используем синхронизацию для получения итоговой суммы
        } );
Console.WriteLine(sum);

