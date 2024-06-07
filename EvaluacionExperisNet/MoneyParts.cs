namespace EvaluacionExperisNet
{
    public class MoneyParts
    {
        public List<List<double>> build(double mount)
        {
            List<double> moneyKinds = new() { 0.05, 0.1, 0.2, 0.5, 1, 2, 5, 10, 20, 50, 100, 200 };

            //Validación para verificar que el monto de dinero sea mayor a 0.05
            if (mount < moneyKinds[0])
            {
                throw new Exception("El monto es menor al de las monedas.");
            }

            List<List<double>> response = new();

            //Lógica de agrupaciones de un tipo de moneda de el monto ingresado
            moneyKinds.ForEach(x =>
            {
                double division = mount / x;

                if (division % 1 == 0)
                {
                    List<double> repeticiones = new();

                    for (int i = 0; i < division; i++)
                    {
                        repeticiones.Add(x);
                    }

                    response.Add(repeticiones);
                }
            });

            //Lógica de agrupaciones de tipos de monedas de el monto ingresado
            for (int i = moneyKinds.Count - 1; i > 1; i--)
            {
                //Validación de monto con el tipo de moneda
                if (moneyKinds[i] >= mount)
                {
                    continue;
                }

                //Recorrer la lista de monedas de mayor a menor y agrupar las combinaciones
                double residuo = mount;

                List<double> moneygroup = new();

                for (int y = i; y >= 0; y--)
                {
                    if (residuo == 0)
                    {
                        break;
                    }

                    if (residuo >= moneyKinds[y])
                    {
                        residuo = Math.Round(residuo - moneyKinds[y], 2);
                        moneygroup.Add(moneyKinds[y]);
                    }

                    if (residuo >= moneyKinds[y])
                    {
                        y++;
                    }
                }

                var validacionIguales = moneygroup.FindIndex(x => x != moneygroup[0]);

                // Si todas las monedas son del mismo tipo no agregar, ya que están agregadas en la primera lógica
                if (validacionIguales != -1)
                {
                    response.Add(moneygroup);
                }
                
            }

            return response;
        }

        public string responseToString(List<List<double>> response)
        {
            List<string> gruposString = new();

            foreach (List<double> list in response)
            {
                string grupoString = "[" + string.Join(",", list) + "]";
                gruposString.Add(grupoString);
            }

            string formatoString = "[" + string.Join(",", gruposString) + "]";

            return formatoString;
        }
    }

    public class TestMoneyParts
    {
        public void run()
        {
            MoneyParts moneyParts = new();

            var responseTest1 = moneyParts.build(0.1);

            Console.WriteLine("Test 1:");
            Console.WriteLine(moneyParts.responseToString(responseTest1));

            var responseTest2 = moneyParts.build(0.5);

            Console.WriteLine("Test 2:");
            Console.WriteLine(moneyParts.responseToString(responseTest2));

            var responseTest3 = moneyParts.build(10.50);

            Console.WriteLine("Test 3:");
            Console.WriteLine(moneyParts.responseToString(responseTest3));
        }
    }
}
