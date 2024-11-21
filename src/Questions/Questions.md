## Вопрос 1
    public struct Info
    {
	    public Info(int number)
	    {
		    Number = number;
	    }
	    public int Number;
	  }
    class Program
    {
	    static void Main(string[] args)
	    {
		    var info = new Info(10);
		    var otherInfo = new Info();
		    var resultInfo = info;
		    info.Number = 8;
		    otherInfo = info;
		    Console.WriteLine(resultInfo.Number + otherInfo.Number + info.Number);
	    }
    }

> **Результат?**

> Структуры присваиваются по значению => 10+8+8 = 26

## Вопрос 2

    foreach (var oldDefect in oldDefects)
    {
	    var number = oldDefect.Number;
	    if (newDefects.FirstOrDefault(defect => defect.Number == number) != null)
		    continue;
	    oldDefect.Valid = false;
    }

> **Опишите назначение данной процедуры.**
>
> **Какие изменения повлечет замена FirstOrDefault() на First()?**

> Если есть новый дефект с таким же номером, то старый дефект инвалидируется
>
> Изменение на First() повлечет InvalidOperationException если в newDefects не все старые дефекты

## Вопрос 3

    public class Info
    {
	    public Info(int number)
	    {
		    Number = number;
	    }
	    public int Number;
    }
    class Program
    {
	    static void Main(string[] args)
	    {
		    var info = new Info(10);
		    var otherInfo = new Info(0);
		    var resultInfo = info;
		    info.Number = 8;
		    otherInfo = info;
		    Console.WriteLine(resultInfo.Number + otherInfo.Number + info.Number);
	    }
    }

> **Результат?**

> Классы ссылочный тип => 8 + 8 + 8 = 24

## Вопрос 4

    [StructLayout(LayoutKind.Explicit, Pack = 2)]
    public struct CoordinateItem
    {
	    [FieldOffset(0)] public double Odometer;
	    [FieldOffset(8)] public uint Time;
	    [FieldOffset(12)] public ushort Angle;
    }
    private unsafe void SimpleCopy(CoordinateItem* ccdItem)
    {
	    var odometer =
    }

> **Получить значение Odometer из структуры.**
> 
> **Преобразуйте код для осуществления возврата одометра через параметр процедуры**

> Ответ:

    private unsafe void SimpleCopy(CoordinateItem* ccdItem, out double odometer) 
    { 
        odometer = ccdItem->Odometer; 
    }

## Вопрос 5

    ushort? value = null;
    using (var binaryWriter = new BinaryWriter(file))
    {
	    binaryWriter.Seek(0, SeekOrigin.End);
	    for (var i = 0; i < 1000; i++)
	    {
		    var currentValue = i;
		    value = value ?? currentValue;
		    binaryWriter.Write(value);
	    }
	    binaryWriter.Flush();
    }

> **Ошибки?**
>
> **Результат выполнения функции?**

> Попытка каста int к ushort
>
> Попытка записи nullable типа
>
> Результат: Запись тысячи нулей в конец файла

## Вопрос 6

    public class Truth
    {
	    public void Answer(double question)
	    {
		    Console.WriteLine("true");
	    }
    }
    public class Lie : Truth
    {
	    public void Answer(int question)
	    {
		    Console.WriteLine("false");
	    }
    }
    static void Main(string[] args)
    {
	    var info = 5;
	    var entity = new Lie();
	    double data = info;
	    entity.Answer(info);
	    entity.Answer(data);
    }

> **Результат выполнения?**

>  info имеет тип int => сработает перегрузка с "false"
>
> data имеет тип double => сработает перегрузка с "true"
>
> Вывод:
> 
> "false"
> 
> "true"

## Вопрос 7

    public IEnumerable<string> RunMe()
    {
	    yield return "O\ne";
	    Console.WriteLine(@"\two");
    }
    static void Main(string[] args)
    {
	    foreach (var str in RunMe())
		    Console.Write(str);
    }

> **Результат выполнения?**

> При первом проходе в foreach из итератора вернется '0\ne', при попытке прочитать второй элемент метод продолжится после yield return,  выведет "\two" и завершится
> 
> Вывод:
>
> 'O', перенос строки, 'e', перенос строки, пробел, 'wo'
