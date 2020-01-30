using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace EjercicioFechas
{
    class Program
    {
        static void Main(string[] args)
        {
            Program ob = new Program();
            /*Console.WriteLine("Ingrese la ruta del archivo TXT");
            string cRuta = Console.ReadLine();*/
            string cRuta = @"C:\Users\carlos.hoil\Desktop\ejercicio.txt";
            
            List<string> lstLineas = new List<string>();

            try {
                lstLineas = ob.obtenerArchivo(cRuta);
                lstLineas = ob.calcularEventos(lstLineas);
                ob.imprimirResultados(lstLineas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer" + ex.Message);
            }
            
            Console.ReadLine();
        }

        

        public List<string> obtenerArchivo(string cRuta)
        {
           string [] cTexto= File.ReadAllLines(cRuta);
            return cTexto.ToList(); 
        }

        public string obtenerTiempo(DateTime dtAhora, DateTime dtEvento)
        {
            string cRetorno;
            bool lOcurrira = false;
            int iHours=0, iDias=0, iMinutos=0, iMeses=0;
            int iResiduo;
            long Miliseg;
            TimeSpan span;

            if (dtAhora > dtEvento)
            {
                span = dtAhora.Subtract(dtEvento);
                lOcurrira = false;

                iMinutos = span.Minutes;
            }
            else
            {
                span = dtEvento.Subtract(dtAhora);
                lOcurrira = true;

                iMinutos = span.Minutes;
            }
            
            iResiduo= iMinutos % 43200; //Meses
            if (iResiduo >= 0)
            {
                iMeses = (iMinutos - iResiduo) / 43200;
                iMinutos = 0;
            }

            iResiduo = iMinutos % 1440;//Dias
            if (iResiduo >= 0)
            {
                iDias = (iMinutos - iResiduo) / 1440;
                iMinutos = 0;
            }

            iResiduo = iMinutos % 60;
            if (iResiduo >= 0)
            {
                iHours = (iMinutos - iResiduo);
                iMinutos = 0;
            }
            cRetorno = string.Format("{0} En ",lOcurrira?"Ocurrirá": "Ocurrió");
            cRetorno = cRetorno += (iMeses > 0) ? iMeses+ " Meses": "";
            cRetorno = cRetorno += (iDias > 0) ? iDias + " Dias" : "";
            cRetorno = cRetorno += (iHours > 0) ? iHours + " Hora(s) " : "";
            return cRetorno;          
        }
        
        public List<string> calcularEventos(List<string> _lstPalabras)
        {
            List<string> lstEventos = new List<string>();
            string[] arrTemp;
            string cEvento;
            string cEventoTiempo;
            DateTime dtFechaEvento;
            DateTime dtFechaActual = DateTime.Now;
            foreach (string evento in _lstPalabras)
            {
                arrTemp = evento.Split(',');
                if (arrTemp.Length==2)
                {
                    cEvento = arrTemp[0];
                    DateTime.TryParse(arrTemp[1].Trim(), out dtFechaEvento);
                    cEventoTiempo = obtenerTiempo(dtFechaActual,dtFechaEvento);
                    cEventoTiempo = string.Format("El evento {0} {1}", cEvento, cEventoTiempo);
                    lstEventos.Add(cEventoTiempo);
                }
                else {
                    lstEventos.Add("Error al calcular evento");
                }

            }

            return lstEventos;
        }
      

        public void imprimirResultados(List<string> _lstEventos)
        {
            foreach (string evento in _lstEventos)
            {
                Console.WriteLine(evento);
            }
        }

 

    }
}
