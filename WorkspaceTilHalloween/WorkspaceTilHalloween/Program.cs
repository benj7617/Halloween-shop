using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceTilHalloween
{
    internal class Program
    {
        public static List<Kurv> kurvliste = new List<Kurv>();
        public static List<Produktinfo> produkter = new List<Produktinfo>();
        public static int valg1 = 0;

        static void Main(string[] args)
        {
            bool isProgramExiting = false, isInputCorrect = false;

            Console.WriteLine("Vekommen til Vestegnens Halloween Shop ");
            Console.WriteLine();

            string path = @".\products.txt";

            try
            {
                FileInfo fi = new FileInfo(path);
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                string line = "";
                int ProductIdentifier = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] text = line.Split(',');
                    produkter.Add(new Produktinfo(ProductIdentifier, text[0], int.Parse(text[1]), int.Parse(text[2]), text[3], text[4]));
                    ProductIdentifier++;
                }
                fs.Close();
                sr.Close();

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Filen kan ikke findes");
            }

            while (!isProgramExiting)
            {
                Console.WriteLine("Du har følgende muligheder: ");
                Console.WriteLine("Tryk 1 for at se varer: ");
                Console.WriteLine("Tryk 2  for at se kurv: ");
                Console.WriteLine("Tryk 0 for at lukke programmet: ");


                int numberInput = -1;
                isInputCorrect = false;

                while (!isInputCorrect)
                {
                    string userInput = Console.ReadLine();

                    try
                    {
                        numberInput = int.Parse(userInput);
                        if (numberInput >= 0 && numberInput <= 2)
                        {
                            isInputCorrect = true;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Indtast venligst et tal for det valg du vil tage");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Indtast venligst et tal mellem 0 og 2");
                    }
                }




                switch (numberInput)
                {
                    case 0:
                        Console.WriteLine("Programmet lukker nu.");
                        isProgramExiting = true;
                        Console.ReadKey();
                        break;
                    case 1:
                        tilføjtilkurv();
                        break;
                    case 2:
                        seKurv();
                        break;
                    default:
                        break;
                }
            }
        }
        private static void tilføjtilkurv()
        {
            Console.Clear();
            Console.WriteLine(" Vælg et produkt:");
            Console.WriteLine();
            for (int i = 0; i < produkter.Count; i++)
            {
                if (produkter[i].numberInStock > 0)
                {
                    Console.WriteLine(produkter[i].productID + ") " + produkter[i].name + ":");
                    Console.WriteLine("Pris: " + produkter[i].price);
                    Console.WriteLine("Antal på lager: " + produkter[i].numberInStock);
                    Console.WriteLine("Beskrivelse: " + produkter[i].description);
                    Console.WriteLine("Kategori: " + produkter[i].category);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(produkter[i].name + " er desværre ikke på lager");
                    Console.WriteLine();
                }
            }

            //bruger vælger hvad de vil have
            try
            {
                valg1 = int.Parse(Console.ReadLine());
                if (produkter[valg1 - 1].numberInStock == 0)
                {
                    Console.WriteLine("Der er ikke flere på lager");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(produkter[valg1 - 1].name + " er lagt i kurven");

                    Kurv kurv1 = new Kurv(produkter[valg1 - 1].name, produkter[valg1 - 1].price, produkter[valg1 - 1].productID);
                    kurvliste.Add(kurv1);

                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Det skal være et tal, prøv igen");
                Console.ReadKey();
                Console.Clear();
                tilføjtilkurv();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("Tallet er ikke på listen");
                Console.ReadKey();
                Console.Clear();
                tilføjtilkurv();
            }
            catch (Exception)
            {
                Console.WriteLine("Send venligst dette til shop@lortemail.dk: TilføjTilKurv-Error");
                Console.ReadLine();
                throw;
            }

        }
        private static void seKurv()
        {
            //int til brug for at udregne samlet pris
            int samletpris = 0;

            //print data om ting, og udregn smalet pris
            Console.Clear();
            if (kurvliste.Count != 0)
            {
                for (int i = 0; i < kurvliste.Count; i++)
                {
                    Console.WriteLine(kurvliste[i].productName + " for " + kurvliste[i].productPrice + " kr.");
                    samletpris = samletpris + kurvliste[i].productPrice;
                }
                Console.WriteLine("Vil du købe varene for " + samletpris + "kr.? [y/n]");
                string confirmationKurv = Console.ReadLine();
                if (confirmationKurv == "y")
                {
                    Opdaterlagerefterbestilling();

                    //Ligegyldig tekst
                    Console.WriteLine("Sender dig til betaling...");
                    Console.WriteLine("...");
                    Console.WriteLine("Tak for dit køb");

                    //tømmer kurven efter køb
                    kurvliste.Clear();

                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Du har ingen varer i kurven");
            }
        }
        private static void Opdaterlagerefterbestilling()
        {
            //sektion til at reducere stock efter hvad der er købt
            for (int i = 0; i < kurvliste.Count; i++)
            {
                Console.WriteLine(kurvliste[i].productID);
                produkter[kurvliste[i].productID - 1].numberInStock -= 1;
            }
        }
    }
}
