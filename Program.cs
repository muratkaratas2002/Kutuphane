namespace Kutuphane
{
    internal class Kütüphane
    {
        static void Main(string[] args)
        {
            List<Kitaplar> kitaplistesi = new List<Kitaplar>();
            List<uye> üyelistesi = new List<uye>();
            List<Kitaplar> ödünçalınankitaplar = new List<Kitaplar>();

            Console.WriteLine("yapmak istediginiz işlemi seçiniz");

            while (true)
            {
                Console.WriteLine("1. Yeni Kitap Ekle");
                Console.WriteLine("2. Tüm Kitapları Listele");
                Console.WriteLine("3. Kitap Ara (Ada göre)");
                Console.WriteLine("4. Kitap Sil");
                Console.WriteLine("5. Üye Ekle");
                Console.WriteLine("6. Üyeleri Listele");
                Console.WriteLine("7. Kitap Ödünç Ver");
                Console.WriteLine("8. Ödünç Alınan Kitapları Listele");
                Console.WriteLine("0. Çıkış");
                int secim = Convert.ToInt32(Console.ReadLine());
                if (secim == 1)
                {
                    Kitaplar.KitapEkle(kitaplistesi);
                }
                else if (secim == 2)
                {
                    Kitaplar.KitaplarıListele(kitaplistesi);
                }
                if (secim == 3)
                {
                    Kitaplar.KitapAra(kitaplistesi);
                }
                if (secim == 4)
                {
                    Kitaplar.KitapSil(kitaplistesi);
                }
                if (secim == 5)
                {
                    uye.ÜyeEkle(üyelistesi);
                }
                if (secim == 6)
                {
                    uye.ÜyeleriListele(üyelistesi);
                }
                if (secim == 7)
                {
                    Kitaplar.KitapÖdünçVerme(kitaplistesi, üyelistesi, ödünçalınankitaplar);

                }
                if (secim == 8)
                {
                    Kitaplar.KitapÖdünçListesi(ödünçalınankitaplar);
                }
                if (secim == 0)
                {
                    break;
                }

            }
        }
    }


    internal class Kitaplar
    {
        internal String ısbn;
        internal String ad;
        internal String yazar;
        internal String durum;

        internal static void KitapEkle(List<Kitaplar> kitaplistesi)
        {
            Kitaplar kitap = new Kitaplar();

            Console.WriteLine("kitap ısbn giriniz");
            kitap.ısbn = Console.ReadLine();

            Console.WriteLine("kitap ad giriniz");
            kitap.ad = Console.ReadLine();

            Console.WriteLine("kitap yazar giriniz");
            kitap.yazar = Console.ReadLine();

            // yeni kitaplar direk müsait(yani alınabilir) durumda ekleniyor.
            kitap.durum = "müsait";

            kitaplistesi.Add(kitap);
            return;
        }

        internal static void KitaplarıListele(List<Kitaplar> kitaplistesi)
        {
            foreach (var kitap in kitaplistesi)
            {
                Console.WriteLine(kitap.ad);
                Console.WriteLine(kitap.durum);

            }
        }

        internal static void KitapAra(List<Kitaplar> kitaplistesi)
        {
            Console.WriteLine("aradıgınız kitabın ismini giriniz");
            String aranan = Console.ReadLine();
            foreach (var kitap in kitaplistesi)
            {
                if (kitap.ad == aranan)
                {
                    Console.WriteLine("kitap bulundu ");

                }
            }
            Console.WriteLine("bu kitap bulunamadı");
            char harf = aranan[0];
            Console.WriteLine(" benzer kitaplar:");
            foreach (var ıtem in kitaplistesi)
            {
                if (ıtem.ad[0] == harf)
                {
                    Console.WriteLine(ıtem.ad);
                }
            }

        }


        internal static void KitapSil(List<Kitaplar> kitaplistesi)
        {
            KitaplarıListele(kitaplistesi);
            Console.WriteLine("silmek istedigin ktabın adını giriniz");
            string silinecek = Console.ReadLine().ToUpper();
            foreach (var kitap in kitaplistesi)
            {
                if (kitap.ad.ToUpper() == silinecek)
                {
                    kitaplistesi.Remove(kitap);
                    Console.WriteLine("kitap silindi");
                    return;
                }
            }
        }

        internal static void KitapÖdünçVerme(List<Kitaplar> kitaplistesi, List<uye> üyelistesi, List<Kitaplar> ödünçalınankitaplar)
        {

            Console.WriteLine("öncelikle kütüphaneye üyemisiniz");
            Console.WriteLine("1-evet, 2-hayır");
            int cevap = Convert.ToInt32(Console.ReadLine());
            if (cevap == 1)
            {
                Console.WriteLine("üye numaranızı giriniz");
                String üyenumarası = Console.ReadLine();

                foreach (var üye in üyelistesi)
                {
                    if (üye.üyeno == üyenumarası)
                    {
                        Console.WriteLine("üye dogrulandı");
                        KitaplarıListele(kitaplistesi);
                        Console.WriteLine("istedigin kitabın ismini giriniz");
                        string ödünçalınacakkitap = Console.ReadLine();
                        foreach (var kitap in kitaplistesi)
                        {
                            if (kitap.ad.ToUpper() == ödünçalınacakkitap.ToUpper())
                            {
                                if (kitap.durum == "müsait")
                                {
                                    Console.WriteLine("kitap ödunç alındı");
                                    kitap.durum = "müsait degil ";
                                    ödünçalınankitaplar.Add(kitap);
                                    üye.aldıgıkitaplar.Add(kitap);
                                    return;
                                }
                            }
                        }
                        Console.WriteLine("kitap müsait degil yada listede yok");
                        return;
                    }
                }

                Console.WriteLine("hatalı no");
                return;
            }


            else if (cevap == 2)
            {
                Console.WriteLine("öncelikle kütüphaneye üye olmanız gerekmektedir");
                return;
            }
        }




        internal static void KitapÖdünçListesi(List<Kitaplar> ödünçalınankitaplar)
        {
            foreach (var item in ödünçalınankitaplar)
            {
                Console.WriteLine(item.ad);
                Console.WriteLine(item.durum);
            }
            return;
        }

    }


    internal class uye
    {
        internal String üyeno;
        internal String üyeisim;
        internal List<Kitaplar> aldıgıkitaplar = new List<Kitaplar>();



        internal static void ÜyeEkle(List<uye> üyelistesi)
        {
            uye üye = new uye();

            Console.WriteLine("üye ismi giriniz");
            üye.üyeisim = Console.ReadLine();

            Console.WriteLine("üye no giriniz");
            üye.üyeno = Console.ReadLine();

            üyelistesi.Add(üye);
        }


        internal static void ÜyeleriListele(List<uye> üyelistesi)
        {
            foreach (var üye in üyelistesi)
            {
                Console.WriteLine(üye.üyeisim);
                foreach (var item in üye.aldıgıkitaplar)
                {
                    Console.WriteLine(item.ad);
                }
            }
        }

    }
}
