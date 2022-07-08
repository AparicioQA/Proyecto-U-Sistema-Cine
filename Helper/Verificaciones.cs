namespace MovieCenter.Helper
{
    public class Verificaciones
    {
        //se le pasa un string y devuelve true si tiene numeros false sino
        public static bool TieneNumeros(string texto)
        {
            bool condicion = false;
            for (int i = 0; i < 10; i++)
            {
                if (texto.Contains(i.ToString()))
                {
                    condicion = true;
                }
            }
            return condicion;
        }


    }
}

