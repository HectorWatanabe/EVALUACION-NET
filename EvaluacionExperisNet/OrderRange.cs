namespace EvaluacionExperisNet
{
    public class OrderRange
    {
        public (List<int>, List<int>) buid(List<int> collectionNumbers)
        {
            //Validación de números enteros positivos
            var foundIndex = collectionNumbers.FindIndex(x => x < 1);

            if (foundIndex != -1)
            {
                throw new Exception("Se encontró un número no positivo entero en la colección números.");
            }

            //Separar los pares e impares
            List<int> pairList = new();
            List<int> oddList = new();

            collectionNumbers.ForEach(x =>
            {
                if (x % 2 == 0)
                {
                    pairList.Add(x);
                }
                else
                {
                    oddList.Add(x);
                }
            });

            //Ordenar las colección
            pairList.Sort();
            oddList.Sort();

            return (pairList, oddList);
        }

        public string listToString(List<int> list)
        {
            string formatoString = "[" + string.Join(",", list) + "]";
            return formatoString;
        }
    }
    

    public class TestOrderRange
    {
        public void runTest()
        {
            List<int> test1 = new() { 2, 1, 4, 5 };
            List<int> test2 = new() { 4, 2, 9, 3, 6 };
            List<int> test3 = new() { 58, 60, 55, 48, 57, 73 };

            OrderRange orderRange = new();

            var resultTest1 = orderRange.buid(test1);

            Console.WriteLine("Caso 1: ");
            Console.WriteLine($"odd list: {orderRange.listToString(resultTest1.Item2)}");
            Console.WriteLine($"pair list: {orderRange.listToString(resultTest1.Item1)}");

            var resultTest2 = orderRange.buid(test2);

            Console.WriteLine("Caso 2: ");
            Console.WriteLine($"odd list: {orderRange.listToString(resultTest2.Item2)}");
            Console.WriteLine($"pair list: {orderRange.listToString(resultTest2.Item1)}");

            var resultTest3 = orderRange.buid(test3);

            Console.WriteLine("Caso 3: ");
            Console.WriteLine($"odd list: {orderRange.listToString(resultTest3.Item2)}");
            Console.WriteLine($"pair list: {orderRange.listToString(resultTest3.Item1)}");

        }
    }
}
