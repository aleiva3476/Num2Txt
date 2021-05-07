using System;
using System.Text;

namespace Alp3476
{
    /// <summary>
    /// Transforma un valor numérico en su equivalente en texto.
    /// </summary>
    public static class Num2Txt
    {
        private static string[] textoCentenas = { "uno", "dos", "tres", "cuatro", "quinientos ", "seis", "sete", "ocho", "nove" };
        private static string[] textoDecenas = { "uno ", "veinti", "treinta ", "cuarenta ", "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa " };
        private static string[] textoDiezVeinte = { "diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ", "diecisiete ", "dieciocho ", "diecinueve " };
        private static string[] textoUnidades = { "uno ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };

        /// <summary>
        /// Convierte un número a letras.
        /// </summary>
        /// <param name="numero">Cadena de texto con los dígitos del número a procesar</param>
        /// <returns>Cadena de texto que representa al número</returns>
        public static string ToString(string numero)
        {
            double.TryParse(numero, out double valor);
            return Num2Txt.ToString(valor);
        }

        /// <summary>
        /// Convierte un número a letras.
        /// </summary>
        /// <param name="numero">Valor numérico que vamos a procesar</param>
        /// <returns>Cadena de texto que representa al número</returns>
        public static string ToString(double numero)
        {
            char c1 = '0', c2 = '0', c3 = '0', modo = ' ';
            int numeroTemporal = 0;
            int posicion, longitud;

            if (numero == 0.0)
            {
                return "cero";
            }
            if (numero > 999999999999.99)
            {
                return string.Empty;
            }

            StringBuilder sbTexto = new StringBuilder();
            bool esNegativo = numero < 0;
            if (esNegativo)
            {
                sbTexto.Append("menos ");
                numero = Math.Abs(numero);
            }

            string txtNumero = numero.ToString("0.00").PadLeft(15, '0');

            for (int contador = 1; contador < 6; contador++)
            {
                switch (contador)
                {
                    case 1: modo = 'm'; break;
                    case 2: modo = 'k'; break;
                    case 3: modo = 'm'; break;
                    case 4: modo = 'c'; break;
                    case 5: modo = 'u'; break;
                }

                string temp = string.Empty;

                if (contador < 5)
                {
                    posicion = (contador - 1) * 3;
                    if (posicion + 3 > txtNumero.Length)
                    {
                        longitud = txtNumero.Length - posicion;
                    }
                    else
                    {
                        longitud = 3;
                    }

                    temp = txtNumero.Substring((contador - 1) * 3, longitud);
                    if (longitud < 3)
                    {
                        temp = temp.PadRight(3, '0');
                    }

                    Convert.ToInt32(temp);
                    c1 = temp[0];
                    c2 = temp[1];
                    c3 = temp[2];
                    sbTexto.Append(Centenas(c1, c2, c3));
                    sbTexto.Append(Decenas(c2, c3));
                    sbTexto.Append(Unidades(c1, c2, c3, modo));
                }
                else
                {
                    temp = txtNumero.Substring(13, 2);
                    if (!String.IsNullOrEmpty(temp))
                    {
                        numeroTemporal = Convert.ToInt32(temp);
                        if (temp.Length < 2)
                        {
                            temp = temp.PadRight(2, '0');
                        }

                        c1 = '0';
                        c2 = temp[0];
                        c3 = temp[1];
                        sbTexto.Append(Decenas(c2, c3));
                        sbTexto.Append(Unidades(c1, c2, c3, modo));
                    }
                }

                if (String.IsNullOrEmpty(temp))
                {
                    continue;
                }

                numeroTemporal = Convert.ToInt32(temp);

                if (contador == 2 && (sbTexto.Length != 0 && !esNegativo || esNegativo && sbTexto.Length > 6))
                {
                    sbTexto.Append(c3 == '1' && c2 == '0' && c1 == '0' ? "millón " : "millones ");
                }


                if ((contador == 1 || contador == 3) && numeroTemporal > 0)
                {
                    sbTexto.Append("mil ");
                }

                if (contador == 4 && txtNumero.Length >= 13)
                {
                    if (!String.IsNullOrEmpty(txtNumero.Substring(13)) && Convert.ToInt32(txtNumero.Substring(13)) > 0)
                    {
                        if (txtNumero[9] == '0' && txtNumero[10] == '0' && txtNumero[11] == '1')
                        {
                            sbTexto.Append('o');
                        }
                        else if (sbTexto.Length == 0)
                        {
                            sbTexto.Append("cero ");
                        }

                        sbTexto.Append("con ");
                    }
                }
            }

            return sbTexto.ToString().Trim();
        }

        /// <summary>
        /// Devuelve una cadena con el texto correspondiente a las centenas, dentro de un grupo de 3 dígitos
        /// </summary>
        /// <param name="centenas">Dígito de las centenas</param>
        /// <param name="decenas">Dígito de las decenas</param>
        /// <param name="unidades">Dígito de las unidades</param>
        /// <returns>Cadena con el texto que corresponde al dígito de las centenas</returns>
        private static string Centenas(char centenas, char decenas, char unidades)
        {
            string txt = string.Empty;

            if (centenas == '0')
            {
                return txt;
            }

            if (centenas == '1')
            {
                if (decenas == '0' && unidades == '0')
                {
                    return "cien ";
                }
                else
                {
                    return "ciento ";
                }
            }

            for (char contador = '0'; contador <= '9'; contador++)
            {
                if (centenas == contador)
                {
                    int indice = (int)contador - (int)'1';
                    txt += textoCentenas[indice];
                    break;
                }
            }

            if (centenas != '5')
            {
                txt += "cientos ";
            }

            return txt;
        }

        /// <summary>
        /// Devuelve una cadena con el texto correspondiente a las decenas, dentro de un grupo de 2 dígitos
        /// </summary>
        /// <param name="decenas">Dígito de las decenas</param>
        /// <param name="unidades">Dígito de las unidades</param>
        /// <returns>Cadena con el texto que corresponde al dígito de las decenas</returns>
        private static string Decenas(char decenas, char unidades)
        {
            char contador;
            int indice;
            string txt = string.Empty;

            if (decenas == '0')
            {
                return (txt);
            }

            if (decenas == '1')
            {
                for (contador = '0'; contador <= '9'; contador++)
                {
                    if (unidades == contador)
                    {
                        indice = (int)contador - (int)'0';
                        txt = textoDiezVeinte[indice];
                        break;
                    }
                }

                return txt;
            }

            for (contador = '1'; contador <= '9'; contador++)
            {
                if (contador == decenas)
                {
                    break;
                }
            }

            if (contador > '9')
            {
                indice = 9;
            }
            else
            {
                indice = (int)contador - (int)'1';
            }

            if (unidades == '0')
            {
                if (decenas == '2')
                {
                    txt = "veinte ";
                }
                else
                {
                    txt = textoDecenas[indice];
                }

                return txt;
            }

            if (decenas != '2')
            {
                txt = textoDecenas[indice] + "y ";
            }
            else if (unidades != '0')
            {
                txt = textoDecenas[indice];
            }

            return txt;
        }

        /// <summary>
        /// Devuelve una cadena con el texto correspondiente a las centenas, dentro de un grupo de 3 dígitos
        /// </summary>
        /// <param name="centenas">Dígito de las centenas</param>
        /// <param name="decenas">Dígito de las decenas</param>
        /// <param name="unidades">Dígito de las unidades</param>
        /// <param name="modo">Indica la "posición" del bloque de dígitos dentro del número completo (si es el bloque de los miles, de las unidades, etc)</param>
        /// <returns>Cadena con el texto que corresponde al dígito de las unidades</returns>
        private static string Unidades(char centenas, char decenas, char unidades, char modo)
        {
            if (unidades == '0' || decenas == '1')
            {
                return string.Empty;
            }

            if (unidades == '1')
            {
                if (decenas == '0' && centenas == '0')
                {
                    if (modo == 'm')
                    {
                        return string.Empty;
                    }
                }

                if (modo == 'k')
                {
                    return "un ";
                }
            }

            int indice;
            char contador;

            for (contador = '1'; contador <= '9'; contador++)
            {
                if (contador == unidades)
                {
                    break;
                }
            }

            if (contador > '9')
            {
                indice = 9;
            }
            else
            {
                indice = (int)contador - (int)'1';
            }

            return textoUnidades[indice];
        }
    }
}
