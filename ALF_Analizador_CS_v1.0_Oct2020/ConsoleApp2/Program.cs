//LIBRERIAS
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConsoleApp1
{
    class ReadFromFile
    {
        static void Main()
        {
            //VARIABLES

            string tempLine;
            string tempPalabra = " ";
            string[] palabrasEnLinea;
            int contadorLineas = 0;
            
            //banderas
            bool esNumerico = false;
            bool encontrada = false;
            bool cadenaAbierta = false;

            //set de palabras reservadas
            string[] palabraReservada = { "main", "int", "if", "else", "then", "for", "each", "while", "print", "write", "read", "end", "mensaje" };
            string[] operadorRelacional = { "<", ">", "<=", ">=", "=", "==", "and", "AND", "or", "OR", "&", "&&", "|", "||" };
            string[] operador = { "+", "-", "/", "%", "*", "++", "--", "+=" };
            var identificador = new List<string>();

           
            //PROGRAMA
                
            try
            {
                //Solicitar path del archivo a leerse
                Console.WriteLine("Ingrese ruta: ");
                String ruta = Console.ReadLine();

                //guarda temporalmente las lineas del archivo en un array de strings
                string[] lines = System.IO.File.ReadAllLines(ruta);

                // Recorrido para desplegar todas las lineas guardadas
                System.Console.WriteLine("Abriendo archivo...");
                System.Console.WriteLine("Achivo contiene " + lines.Length + " lineas en total.");
                System.Console.WriteLine("CONTENIDO: ");
                System.Console.WriteLine(" ");


                foreach (string line in lines)
                {
                    //Elimina espacios al inicio y final de linea
                    tempLine = line.TrimStart();
                    tempLine = tempLine.TrimEnd();
                    Console.WriteLine("Linea No." + contadorLineas);
                    Console.WriteLine(tempLine);
                    contadorLineas++;
                    System.Console.WriteLine(" ");
                    palabrasEnLinea = tempLine.Split(' ');


                    //parte cada linea en palabras
                    foreach (string palabra in palabrasEnLinea)
                    {
                        //Analiza para ver si la palabra se encuentra entre las palabras y operadores reservados
                        //esto se logra recorriendo los arreglos de palabras y operadores reservados
                        //sino esta, verifica si es numero o cadena


                        //si se abre una cadena con ", todo el texto que siga hasta que se encuentre " otra vez es considerado una cadena
                        if(cadenaAbierta == false)
                        {
                            //revisa si la palabra esta en arreglo de palabraReservada
                            foreach (string arraypalabra in palabraReservada)
                            {
                                if (palabra == arraypalabra)
                                {
                                    Console.WriteLine(palabra + " es P RESERVADA encontrada en linea No." + contadorLineas);
                                    encontrada = true;
                                }
                            }

                       
                            //revisa si la palabra esta en la lista de identificadores
                            foreach (string arraypalabra in identificador)
                            {
                                if(palabra == arraypalabra)
                                {
                                    Console.WriteLine(palabra + " es IDENTIFICADOR encontrada en linea No." + contadorLineas);
                                    encontrada = true;
                                }
                                
                            }
                             
                            

                            //revisa si la palabra esta en arreglo de operadorRelacional
                            foreach (string arraypalabra in operadorRelacional)
                            {
                                if (palabra == arraypalabra)
                                {
                                    //si se encontro un simbolo de igual solo y la palabra anterior era no reconocida, entonces la palabra anterior es un identificador
                                    if(palabra == "=" && tempPalabra != " ")
                                    {
                                        identificador.Add(tempPalabra);
                                        Console.WriteLine("CORRECCION: " + tempPalabra + " es IDENTIFICADOR encontrada en linea No." + contadorLineas);
                                        tempPalabra = " ";
                                    }

                                    Console.WriteLine(palabra + " es OP RELACIONAL encontrada en linea No." + contadorLineas);
                                    encontrada = true;
                                }
                            }

                            //revisa si la palabra esta en arreglo de operadores
                            foreach (string arraypalabra in operador)
                            {
                                if (palabra == arraypalabra)
                                {
                                    Console.WriteLine(palabra + " es OPERADOR encontrada en linea No." + contadorLineas);
                                    encontrada = true;
                                }
                            }

                            //si la palabra contiene comillas es cadena
                            if (palabra.Contains('"'))
                            {
                                Console.WriteLine(palabra + " es CADENA encontrada en linea No." + contadorLineas);
                                encontrada = true;
                                cadenaAbierta = true;
                            }

                            //si la palabra se puede convertir en int es numero
                            esNumerico = int.TryParse(palabra, out _);
                            if (esNumerico)
                            {
                                Console.WriteLine(palabra + " es NUMERICA encontrada en linea No." + contadorLineas);
                                encontrada = true;
                            }

                            //si es linea vacio, se omite
                            if (palabra.Length==0)
                            {
                                Console.WriteLine("Linea vacia en linea No." + (contadorLineas-1));
                                encontrada = true;
                            }
                        }
                        else
                        {
                            //se marca como cadena ya que se abrio cadena con "
                            Console.WriteLine(palabra + " es CADENA encontrada en linea No." + contadorLineas);

                            if (palabra.Contains('"'))
                            {
                                cadenaAbierta = false;
                                //si aparece por segunda vez consecutiva ", entonces se cierra cadena
                            }

                            encontrada = true;
                        }
                                 
                        //si la palabra no es encontro, se marca como no encontrada
                        if (!encontrada)
                        {
                            Console.WriteLine(palabra + " es palabra NO RECONOCIDA encontrada en linea No." + contadorLineas);
                            tempPalabra = palabra;
                        }
                        else
                        {
                            //si si se encontro, se resetea el estado
                            encontrada = false;
                        }
                    }

                    //imprimir lineas en blanco
                    System.Console.WriteLine(" ");
                    System.Console.WriteLine(" ");
                }
            }
            catch (Exception e)
            {
                //Si la ruta ingresada o un error de sistema ocurre en el programa, terminara mostrando el error
                System.Console.WriteLine(" ");
                System.Console.WriteLine("Error fatal...");
                System.Console.WriteLine("Error: " + e.Message);
            }

            //Se mantiene abierta la consola hasta que se presione cualquier tecla para cerrar el programa
            //esto nos permite visualizar el programa sin que se cierre le pantalla rapidamente
            System.Console.WriteLine(" ");
            Console.WriteLine("Presione cualquier tecla para salir.");
            System.Console.ReadKey();

        }
    }
}
